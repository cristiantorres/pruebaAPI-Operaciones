using Andreani.Integracion.Eventos.Almacenes;

using Infra.EventBus.IbmMQ.DependencyInjection;
using Infra.HealthCheck.Core.Health;
using Infra.HealthCheck.IbmMQ;
using Infra.Metrics.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OperacionesApi.Configuration;
using OperacionesApi.Handlers;
using OperacionesApi.Managements;
using Prometheus;
using System;

[assembly: HostingStartup(typeof(OperacionesApi.Startup))]

namespace OperacionesApi
{
    public class Startup : IHostingStartup
    {
 
        public void Configure(IWebHostBuilder builder)
        {
           builder.ConfigureServices((ctx, c) =>
           {
               c.AddDataAccessRegistry();
               c.AddSingleton<IHealthIndicator, IbmQueueManagerHealthIndicator>(s => new IbmQueueManagerHealthIndicator("GA02.AR.T.QM", "192.6.6.39(1416)", "ITG.TO.GA02.TEST"));
               c.AddSingleton<IPedidoAsignadoManagement, PedidoAsignadoManagement>();
               c.AddIbmMQEventBus(ctx.Configuration)
                   .Subscribe<PedidoCreado, PedidoCreadoHandler>();
               c.AddMetrics();
               c.AddSingleton<MetricsManager>();
               //c.AddCors();
               //c.AddTransient<IStartupFilter, StartupFilter>();

           });

            //builder.Configure(app => {
            //    app.UseRequestMiddleware();
            //});

        }

    }
}
