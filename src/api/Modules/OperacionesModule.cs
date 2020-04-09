 
using Carter;
 
using Infra.Data;
using Infra.EventBus;
using Infra.Web.Problems;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Carter.ModelBinding;
using Carter.Request;
using Carter.Response;
//using Nancy.ModelBinding;
 
using OperacionesApi.Configuration;
using OperacionesApi.Managements;
using OperacionesApi.Model;
using Microsoft.AspNetCore.Routing;
using System;


namespace OperacionesApi.Modules
{
    public class OperacionesModule : CarterModule
    {
        #region variables
        private readonly ILogger<OperacionesModule> _logger;
        private readonly IDataAccessRegistry _dataAccessRegistry;
        private readonly IPedidoAsignadoManagement _management;
        private IDataAccess DataAccess => _dataAccessRegistry.GetDataAccess();
        #endregion

        public OperacionesModule(ILogger<OperacionesModule> logger, IDataAccessRegistry dataAccessRegistry, IPedidoAsignadoManagement management) : base("/api/operaciones")
        {
            _logger = logger;
            _dataAccessRegistry = dataAccessRegistry;
            _management = management;
             #region endpoints
             Post("/", async (req, res) =>
            {
                var result = await req.BindAndValidate<Operacion>();
                
                if (!result.ValidationResult.IsValid)
                {
                    await 
                    res.AsProblem(result.ValidationResult,
                           title: "Errores de validacion",
                           detail: "Verificar Errors para mas detalle");
                    return;
                }
                string _urlRespuestaOperacion = "/api/resultados/";
                /*Id generado para la operacion y la respuesta*/
                var _idRespuesta = Guid.NewGuid().ToString().Substring(0, 5);
                result.Data.Id = _idRespuesta;
                /*Se realiza el insert en la DB de la operacion creada*/
                DataAccess.Insert(result.Data);
                _management.publicar(result.Data);

                _logger.LogInformation("operacion registrada...");
                res.StatusCode = 202;
                res.Headers["Location"] = $"{_urlRespuestaOperacion}{_idRespuesta}";
                await res.WriteAsync("operacion agregada"); 
            });
            #endregion

            After = async (ctx) =>
            {
                MetricsManager.updateMetricModuloOperaciones(ctx.Request.Method,ctx.Response.StatusCode.ToString());
                await ctx.Response.WriteAsync("   -- fin con statusCode: "+ctx.Response.StatusCode);
                return;
            };

        }
    }
} 
