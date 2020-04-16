
using OperacionesApi.Model;
using System;
using System.Collections.Generic;


namespace OperacionesApi.Managements
{
    public interface IPedidoAsignadoManagement
    {
        
        public void Guardar(Operacion operacion);
        public void Publicar(Operacion operacion);
        public IList<Operacion> ListarOperaciones();
    }
}
