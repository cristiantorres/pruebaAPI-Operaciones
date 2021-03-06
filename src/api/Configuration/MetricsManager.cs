﻿using Prometheus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperacionesApi.Configuration
{
    public class MetricsManager
    {
        #region variables
        private static Counter counterModuloOperaciones = Metrics.CreateCounter("my_counter_Call_Operaciones", "Metrica - contador Modulo Operaciones", new CounterConfiguration
        {
            LabelNames = new[] { "method","statusCode" }
        });
        private static Counter counterResultadosCalculados = Metrics.CreateCounter("my_counter_ResultadoCalculado", "Metrica - contador de recepcion de evento Pedido creado(resultado calculado)");
        #endregion

        /// <summary>
        /// Metodo que actualiza el contador para medir las llamadas al   Modulo Operaciones
        /// </summary>
        /// <param name="method"></param>
        /// <param name="statusCode"></param>
        public static void updateMetricModuloOperaciones(string method, string statusCode)
        {
            counterModuloOperaciones.Labels(method,statusCode).Inc();
        }
        /// <summary>
        /// Metodo que actualiza el contador de la metrica  para medir
        /// las respuestas recibidas
        /// </summary>
        public static void updateMetricResultadosCalculados()
        {
            counterResultadosCalculados.Inc();
        }
    }

}
