using Carter;
using Carter.Request;
using Carter.Response;
using Infra.Data;
using Microsoft.Extensions.Logging;
 
using OperacionesApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OperacionesApi.Modules
{
    public class ResultadosModule: CarterModule
    {
        #region variables
        private readonly ILogger<ResultadosModule> _logger;
        private readonly IDataAccessRegistry _dataAccessRegistry;
        private IDataAccess DataAccess => _dataAccessRegistry.GetDataAccess();
        #endregion
        public ResultadosModule(ILogger<ResultadosModule> logger, IDataAccessRegistry dataAccessRegistry) : base("/api/resultados")
        {
            _logger = logger;
            _dataAccessRegistry = dataAccessRegistry;

            #region endpoints
            Get("/{id}", async (req, res) =>
            {
                try
                {
                    string idResultado = req.RouteValues.As<String>("id");
                    var resultado = DataAccess.Get<Resultado>(idResultado);
                    if (resultado == null)
                    {   
                      res.StatusCode = 404;
                      await res.AsJson(new {mensaje = "Resultado no encontrado" });
                      return; 
                    }
                    _logger.LogInformation($"Obteniendo resultado de la operacion {idResultado}");
                    await res.AsJson(resultado);
                    return;
                }
                catch(Exception exception)
                {
                    res.StatusCode = 500;  
                    await res.AsJson(new { error = exception.Message });
                    return;                   
                }
            });
            #endregion
        }
    }
}
