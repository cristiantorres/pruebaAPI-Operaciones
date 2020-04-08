using Andreani.Integracion.Esquemas.Eventos;
using Andreani.Integracion.Eventos.Almacenes;
using Infra.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperacionesApi.Managements
{
    public class PedidoAsignadoManagement : IPedidoAsignadoManagement
    {
        private readonly IEventBus _eventBus;

        public PedidoAsignadoManagement(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operando1"></param>
        /// <param name="operando2"></param>
        /// <param name="idOperacion"></param>
        public void publicar(string operando1,string operando2,string idOperacion)
        {
            var pedidoAsignado = new ConstruirEvento<PedidoAsignado>()
                         .DesdeLaApp("OPERACIONES-API")
                         .ConDestino("QL.BORRARME.REQ")
                         .Crear();
            /*Datos del evento a publicar */
            pedidoAsignado.numeroDePedido = string.Empty;
            pedidoAsignado.cuentaCorriente = idOperacion;
            pedidoAsignado.cicloDelPedido = "sumar";
            pedidoAsignado.codigoDeContratoInterno = operando1.ToString();
            pedidoAsignado.estadoDelPedido = operando2.ToString();
            pedidoAsignado.cuando = string.Empty;
            //publicacion del evento
            _eventBus.Publish(pedidoAsignado);
        }


 
    }
}
