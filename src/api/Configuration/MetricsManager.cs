using Prometheus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperacionesApi.Configuration
{
    public class MetricsManager
    {
        private Counter counterModuloOperaciones = Metrics.CreateCounter("my_counter_Call_Operaciones", "Metrica - contador Modulo Operaciones", new CounterConfiguration
        {
            LabelNames = new[] { "method" }
        });
        private Counter counterResultadosCalculados = Metrics.CreateCounter("my_counter_ResultadoCalculado", "Metrica - contador de recepcion de evento Pedido creado(resultado calculado)");

        /// <summary>
        /// Metodo que actualiza el contador para medir las llamadas al   Modulo Operaciones
        /// </summary>
        /// <param name="method"></param>
        /// <param name="statusCode"></param>
        public void updateMetricModuloOperaciones(string method)
        {
            counterModuloOperaciones.Labels(method).Inc();
        }
        /// <summary>
        /// Metodo que actualiza el contador de la metrica  para medir
        /// las respuestas recibidas
        /// </summary>
        public void updateMetricResultadosCalculados()
        {
            counterResultadosCalculados.Inc();
        }
    }

}
