using Andreani.Integracion.Eventos.Almacenes;
using Infra.EventBus;
using OperacionesApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperacionesApi.Managements
{
    public interface IPedidoAsignadoManagement
    {
        void Guardar(Operacion operacion);
        void Publicar(Operacion operacion);
    }
}
