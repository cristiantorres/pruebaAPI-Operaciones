using Andreani.Integracion.Eventos.Almacenes;
using Infra.Data.DependencyInjection;
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
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

[assembly: HostingStartup(typeof(OperacionesApi.Startup))]

namespace OperacionesApi
{
    public class Startup : IHostingStartup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        //public void Configure(IApplicationBuilder app)
        //{
        //    app.Use(async (context, next) =>
        //    {
        //        // Do work that doesn't write to the Response.
        //        await next.Invoke();
        //        // Do logging or other work that doesn't write to the Response.
        //    });

        //    app.Run(async context =>
        //    {
        //        await context.Response.WriteAsync("Hello from 2nd delegate.");
        //    });
        //}
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
             //  c.AddTransient<IStartupFilter, StartupFilter>();

           });

            //builder.Configure(app => {
            //    app.UseRequestMiddleware();
            //});

        }

    }
}
