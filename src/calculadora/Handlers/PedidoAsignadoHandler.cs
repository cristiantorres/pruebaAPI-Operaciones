using Andreani.Integracion.Eventos.Almacenes;
using Calculadora.Managements;
using Infra.EventBus;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calculadora.Handlers
{
    public class PedidoAsignadoHandler : IIntegrationEventHandler<PedidoAsignado>
    {
        #region variables
        private readonly ILogger<PedidoAsignadoHandler> _logger;
        private readonly IPedidoCreadoManagement _management;
        #endregion
        public PedidoAsignadoHandler(ILogger<PedidoAsignadoHandler> logger, IPedidoCreadoManagement management)
        {
            _logger = logger;
            _management = management;
        }
        public Task Handle(PedidoAsignado @event, IDictionary<string, object> properties)
        {
            /*Calculo del resultado a Publicar*/
            var resultadoOperacion = Convert.ToInt32(@event.codigoDeContratoInterno) + Convert.ToInt32(@event.estadoDelPedido);
            _management.publicar(@event.cuentaCorriente, resultadoOperacion);
            _logger.LogInformation($"Se realizo el calculo de la operacion ID: {@event.cuentaCorriente}");
            return Task.FromResult(0);
        }
        public Task<bool> HandleError(string @event, Exception exception)
        {
            _logger.LogError($"Se produjo un error al procesar el evento de negocio: {@event}: detalle del error:{exception.Message}");
            return Task.FromResult(false); // false == commit, true == backout (back to the queue)
        }
    }
}
