using Andreani.Integracion.Esquemas.Eventos;
using Andreani.Integracion.Eventos;
using Andreani.Integracion.Eventos.Almacenes;
using Infra.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculadora.Managements
{
    public class PedidoCreadoManagement:IPedidoCreadoManagement
    {
        private readonly IEventBus _eventBus;
        public PedidoCreadoManagement(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void publicar(string idResultado, int resultadoCalculado)
        {
            var pedidoCreado   = new ConstruirEvento<PedidoCreado>()
                                      .DesdeLaApp("CALCULADORA")
                                      .ConDestino("QL.BULPYMED.TEST.REQ")
                                      .Crear();
            /*Datos del evento a publicar */
            pedidoCreado.numeroDePedido = resultadoCalculado.ToString();
            pedidoCreado.cuentaCorriente =  idResultado;
            pedidoCreado.cicloDelPedido = string.Empty;
            pedidoCreado.codigoDeContratoInterno = string.Empty;
            pedidoCreado.estadoDelPedido = string.Empty;
            pedidoCreado.cuando = string.Empty;
            
            //publicacion del evento
            _eventBus.Publish(pedidoCreado);
        }
 
    }
}
