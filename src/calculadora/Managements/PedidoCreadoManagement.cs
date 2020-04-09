using Andreani.Integracion.Esquemas.Eventos;
using Andreani.Integracion.Eventos.Almacenes;
using Infra.EventBus;

namespace Calculadora.Managements
{
    public class PedidoCreadoManagement : IPedidoCreadoManagement
    {
        private readonly IEventBus _eventBus;
        public PedidoCreadoManagement(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        /// <summary>
        /// Este metodo se encarga de calcular el resultado y su correspondiente
        /// publicacion del evento PedidoCreado en el EventBus
        /// </summary>
        /// <param name="idResultado"></param>
        /// <param name="resultadoCalculado"></param>
        public void publicar(string idResultado, int resultadoCalculado)
        {
            var pedidoCreado = new ConstruirEvento<PedidoCreado>()
                                      .DesdeLaApp("CALCULADORA")
                                      .ConDestino("QL.BULPYMED.TEST.REQ")
                                      .Crear();
            /*Datos del evento a publicar */
            pedidoCreado.numeroDePedido = resultadoCalculado.ToString();
            pedidoCreado.cuentaCorriente = idResultado;
            pedidoCreado.cicloDelPedido = string.Empty;
            pedidoCreado.codigoDeContratoInterno = string.Empty;
            pedidoCreado.estadoDelPedido = string.Empty;
            pedidoCreado.cuando = string.Empty;
            //publicacion del evento
            _eventBus.Publish(pedidoCreado);
        }

    }
}
