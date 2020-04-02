using Andreani.Integracion.Eventos;
using Andreani.Integracion.Eventos.Almacenes;
using Infra.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculadora.Managements
{
    public interface IPedidoCreadoManagement
    {
         void publicar(string evento,int resultadoCalculado);
    }
}
