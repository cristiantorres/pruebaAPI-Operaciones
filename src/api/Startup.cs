using Andreani.Integracion.Eventos.Almacenes;
using Infra.Data.DependencyInjection;
using Infra.EventBus.IbmMQ.DependencyInjection;
using Infra.HealthCheck.Core.Health;
using Infra.HealthCheck.IbmMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OperacionesApi.Handlers;
using OperacionesApi.Managements;

[assembly: HostingStartup(typeof(OperacionesApi.Startup))]

namespace OperacionesApi
{
  public class Startup : IHostingStartup
  {
        // This method gets called by the runtime. Use this method to add services to the container.
 
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((ctx, c) =>
            {
                c.AddDataAccessRegistry();
                c.AddSingleton<IHealthIndicator, IbmQueueManagerHealthIndicator>(s => new IbmQueueManagerHealthIndicator("GA02.AR.T.QM", "192.6.6.39(1416)", "ITG.TO.GA02.TEST"));
                c.AddSingleton<IPedidoAsignadoManagement, PedidoAsignadoManagement>();
                c.AddIbmMQEventBus(ctx.Configuration)
                    .Subscribe<PedidoCreado, PedidoCreadoHandler>();
            });
         }

    }
}
