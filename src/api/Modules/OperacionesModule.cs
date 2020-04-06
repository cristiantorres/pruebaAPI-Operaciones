using Andreani.Integracion.Esquemas.Eventos;
using Andreani.Integracion.Eventos.Almacenes;
using FluentValidation.Results;
using Infra.Data;
using Infra.EventBus;
using Infra.Web.Problems;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using OperacionesApi.Configuration;
using OperacionesApi.Managements;
using OperacionesApi.Model;
using OperacionesApi.Modules.Validators;
using Prometheus;
using System;

namespace OperacionesApi.Modules
{
    public class OperacionesModule : NancyModule
    {
        #region variables
        
        private readonly ILogger<OperacionesModule> _logger;
        private readonly IDataAccessRegistry _dataAccessRegistry;
        private readonly IPedidoAsignadoManagement _management;
        private readonly MetricsManager _managerMetrics;

        //private Counter counterOperaciones = Metrics.CreateCounter("my_counter_Call_Operaciones", "Metrica - contador Modulo Operaciones",new CounterConfiguration
        //{
        //    LabelNames = new[] {  "method" }
        //});

        private IDataAccess DataAccess => _dataAccessRegistry.GetDataAccess();
        #endregion

        public OperacionesModule(ILogger<OperacionesModule> logger, IDataAccessRegistry dataAccessRegistry, IPedidoAsignadoManagement management,MetricsManager managerMetric) : base("/operaciones")
        {
           
            _logger = logger;
            _dataAccessRegistry = dataAccessRegistry;
            _management = management;
            _managerMetrics = managerMetric;
             #region endpoints
            Post("/", async (req, res) =>
            {
                  
                //var statusCode = 200;
                // Incremento el contador de llamadas al modulo Operaciones
                _managerMetrics.updateMetricModuloOperaciones("POST");
                //counterOperaciones.Labels("POST").Inc();
                var request = this.BindAndValidate<Operacion>();
                
                if (!ModelValidationResult.IsValid)
                {
                    return new ProblemResponse(ModelValidationResult,
                     title: "Errores de validacion",
                     detail: "Verificar Errors para mas detalle");
                }

                 /*nombre del servidor*/
                string _server = Environment.MachineName.ToLower();
                string urlRespuestaOperacion = $"http://{_server}/resultados/";
                //Id generado para la operacion y la respuesta
                var idRespuesta = Guid.NewGuid().ToString().Substring(0, 5);
                request.Id = idRespuesta;

                /*Se realiza el insert en la DB de la operacion creada*/
                DataAccess.Insert(request);
                _management.publicar(request.FirstValue.ToString(), request.SecondValue.ToString(),request.Id);
                _logger.LogInformation("operacion registrada...");
                return await Negotiate
                       .WithHeader("link respuesta", $"{urlRespuestaOperacion}{idRespuesta}")
                       .WithStatusCode(HttpStatusCode.OK);
            });
            #endregion

        }
    }
} 
