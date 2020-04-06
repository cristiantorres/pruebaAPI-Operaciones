using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperacionesApi.Configuration
{
    /// <summary>
    /// Clase que se encarga de validar el path de cada request
    /// para agregarlo al conjunto de metricas
    /// </summary>
    public class RequestValidator
    {
        public static bool validate(string path)
        {
            var conditionRequestNoMetrics = (path != "/metrics");
            var conditionRequestResultados = path.Contains("resultados");
            return conditionRequestNoMetrics && conditionRequestResultados;
        } 
    }
}
