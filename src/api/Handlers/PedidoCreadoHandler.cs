﻿using Andreani.Integracion.Eventos.Almacenes;
using Infra.Data;
using Infra.EventBus;
using Microsoft.Extensions.Logging;
using OperacionesApi.Model;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperacionesApi.Handlers
{
    
    //Modificar nombre de la clase  PedidoCreadoHandler
    public class PedidoCreadoHandler : IIntegrationEventHandler<PedidoCreado>
    {
        private readonly ILogger<PedidoCreadoHandler> _logger;
        private readonly IDataAccessRegistry _dataAccessRegistry;
        private IDataAccess DataAccess => _dataAccessRegistry.GetDataAccess();
        private Counter counterResultados = Metrics.CreateCounter("my_counterResultadoCalculado", "Metrica - contador de recepcion de evento Pedido creado(resultado calculado)");

        public PedidoCreadoHandler(ILogger<PedidoCreadoHandler> logger, IDataAccessRegistry dataAccessRegistry)
        {
            _logger = logger;
            _dataAccessRegistry = dataAccessRegistry;
        }
        public Task Handle(PedidoCreado @event, IDictionary<string, object> properties)
        {
            //Incremento del contador
            counterResultados.Inc();

            //Leer el resultado de la operacion y luego  guarda en la DB
            Resultado resultado = new Resultado {Id = @event.cuentaCorriente,
                                                 Result= Convert.ToInt32(@event.numeroDePedido)
                                                };
            DataAccess.Insert<Resultado>(resultado);
            _logger.LogInformation($"El resultado de la operacion {resultado.Id} se ha registrado...");
            return Task.FromResult(0);
        }

        public Task<bool> HandleError(string @event, Exception exception)
        {
            _logger.LogError($"Error al procesar {@event} : {exception.Message}");
            return Task.FromResult(false); // false == commit, true == backout (back to the queue)
         }
    }
}

 