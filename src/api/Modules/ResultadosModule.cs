using Infra.Data;
using Microsoft.Extensions.Logging;
using Nancy;
using OperacionesApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperacionesApi.Modules
{
    public class ResultadosModule: NancyModule
    {
        #region variables
        private readonly ILogger<ResultadosModule> _logger;
        private readonly IDataAccessRegistry _dataAccessRegistry;
        private IDataAccess DataAccess => _dataAccessRegistry.GetDataAccess();
        #endregion

        public ResultadosModule(ILogger<ResultadosModule> logger, IDataAccessRegistry dataAccessRegistry) : base("/resultados")
        {
            _logger = logger;
            _dataAccessRegistry = dataAccessRegistry;

            #region endpoints
            Get("/{id}", parameters =>
            {
                string idResultado = parameters.id;
                var resultado = DataAccess.Get<Resultado>(idResultado);
                if(resultado == null)
                {
                    return Negotiate.WithStatusCode(HttpStatusCode.NotFound);
                }
                _logger.LogInformation($"Obteniendo resultado de la operacion {parameters.id}");
                return Negotiate.WithModel(resultado).WithStatusCode(HttpStatusCode.OK);
            });
            #endregion
        }
    }
}
