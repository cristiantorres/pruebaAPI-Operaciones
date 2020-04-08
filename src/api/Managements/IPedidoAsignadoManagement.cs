using Andreani.Integracion.Eventos.Almacenes;
using Infra.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperacionesApi.Managements
{
    public interface IPedidoAsignadoManagement
    {
          void publicar(string operando1, string operando2, string idOperacion);
    }
}
