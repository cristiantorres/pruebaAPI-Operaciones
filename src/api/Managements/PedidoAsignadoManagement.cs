using Andreani.Integracion.Esquemas.Eventos;
using Andreani.Integracion.Eventos.Almacenes;
using Infra.Data;
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
        private readonly IDataAccessRegistry _dataAccessRegistry;
        private IDataAccess DataAccess => _dataAccessRegistry.GetDataAccess();

        public PedidoAsignadoManagement(IEventBus eventBus, IDataAccessRegistry dataAccessRegistry)
        {
            _eventBus = eventBus;
            _dataAccessRegistry = dataAccessRegistry;
        }

        /// <summary>
        /// Guarda en la DB la operacion creada*/
        /// </summary>
        public void Guardar(Operacion operacionAPublicar)
        {
            
            DataAccess.Insert(operacionAPublicar);
        }

            /// <summary>
            /// Construye y publica el evento en la cola de mensajes "QL.BORRARME.REQ"
            /// </summary>
            public void Publicar(Operacion operacionAPublicar)
        {
            var pedidoAsignado = new ConstruirEvento<PedidoAsignado>()
                         .DesdeLaApp("OPERACIONES-API")
                         .ConDestino("QL.BORRARME.REQ")
                         .Crear();
            /*Datos del evento a publicar */
            pedidoAsignado.numeroDePedido = string.Empty;
            pedidoAsignado.cuentaCorriente = operacionAPublicar.Id;
            pedidoAsignado.cicloDelPedido = operacionAPublicar.Type;
            pedidoAsignado.codigoDeContratoInterno = operacionAPublicar.FirstValue.ToString();
            pedidoAsignado.estadoDelPedido = operacionAPublicar.SecondValue.ToString();
            pedidoAsignado.cuando = string.Empty;
            //publicacion del evento
            _eventBus.Publish(pedidoAsignado);
        }


 
    }
}
