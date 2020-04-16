 
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
using System.Collections.Generic;

namespace OperacionesApi.Modules
{
    public class OperacionesModule : CarterModule
    {
        #region variables
        private readonly ILogger<OperacionesModule> _logger; 
        private readonly IPedidoAsignadoManagement _management;
        #endregion

        public OperacionesModule(ILogger<OperacionesModule> logger, IPedidoAsignadoManagement management) : base("/api/operaciones")
        {
            _logger = logger;
            _management = management;
            #region endpoints
            Post("/", async (req, res) =>
           {
               try
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

                   _management.Guardar(result.Data);
                   _management.Publicar(result.Data);

                   _logger.LogInformation("operacion registrada...");
                   res.StatusCode = 202;
                   res.Headers["Location"] = $"{_urlRespuestaOperacion}{_idRespuesta}";
                   await res.WriteAsync("operacion agregada");
               }
               catch(Exception exception)
               {
                   res.StatusCode = 500;
                  _logger.LogError($"Falla en:{req.Method} - OperacionesModule",exception  );
               }
           });
            Get("/", async (req, res) =>
            {
                try
                {
                    IList<Operacion> _listaOperaciones = new List<Operacion>();
                   _listaOperaciones = _management.ListarOperaciones();
                    _logger.LogInformation("listando operaciones existentes...");
                    res.StatusCode = 200;
                     await res.AsJson(_listaOperaciones);
                }
                catch (Exception exception)
                {
                    res.StatusCode = 500;
                    _logger.LogError($"Falla en:{req.Method} - OperacionesModule", exception);
                }
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
