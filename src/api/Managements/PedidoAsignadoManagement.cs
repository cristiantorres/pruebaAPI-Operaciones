using Andreani.Integracion.Esquemas.Eventos;
using Andreani.Integracion.Eventos.Almacenes;
using Infra.EventBus;
using OperacionesApi.Model;
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
      //  public void publicar(string operando1,string operando2,string idOperacion)
       public void publicar(Operacion operacionAPublicar)
        {
            var pedidoAsignado = new ConstruirEvento<PedidoAsignado>()
                         .DesdeLaApp("OPERACIONES-API")
                         .ConDestino("QL.BORRARME.REQ")
                         .Crear();
            /*Datos del evento a publicar */
            pedidoAsignado.numeroDePedido = string.Empty;
            pedidoAsignado.cuentaCorriente = operacionAPublicar.Id;
            pedidoAsignado.cicloDelPedido = "sumar";
            pedidoAsignado.codigoDeContratoInterno = operacionAPublicar.FirstValue.ToString();
            pedidoAsignado.estadoDelPedido = operacionAPublicar.SecondValue.ToString();
            pedidoAsignado.cuando = string.Empty;
            //publicacion del evento
            _eventBus.Publish(pedidoAsignado);
        }


 
    }
}
