using Andreani.Integracion.Eventos.Almacenes;
using Calculadora.Handlers;
using Calculadora.Managements;
using Infra.EventBus.IbmMQ.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

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
                    c.AddSingleton<IPedidoCreadoManagement, PedidoCreadoManagement>();
                    c.AddIbmMQEventBus(ctx.Configuration)
                      .Subscribe<PedidoAsignado, PedidoAsignadoHandler>();
                });
        }
  }
}
