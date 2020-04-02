using Infra.WebHost.Test;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OperacionesAPIUnitTest
{
    public class ResultadosModuleTest : IClassFixture<PlatformApiTestFixture>
    {
        readonly PlatformApiTestFixture _platformFixture;
        /// <summary>
        /// Constructor de la clase OperacionesModuleTest con la inyeccion de dependencia de la instancia platformFixture
        /// </summary>
        /// <param name="platformFixture"></param>
        public ResultadosModuleTest(PlatformApiTestFixture platformFixture)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            Environment.SetEnvironmentVariable("ASPNETCORE_HOSTINGSTARTUPASSEMBLIES", "OperacionesApi");
            _platformFixture = platformFixture;
        }
        /// <summary>
        /// Test unitario metodo Get del Modulo Resultados
        /// </summary>
        [Fact]
        public void GetResultadosAsync()
        {
            //TO DO
            //realizar un Post de operaciones y luego probar este metodo con la url que llega en el response
            string idValido = "e19c3";
            string idNoValido = "e100c3";
            var baseUri = new Uri("http://localhost:5000/");
            var route = $"/resultados/{idValido}";
            var route2 = $"/resultados/{idNoValido}";
            var client = _platformFixture.CreateClient();
            var result = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.OK,result.StatusCode);
            //using (var objClint = new HttpClient())
            //{
            //    objClint.BaseAddress = baseUri;
            //    var respon =  objClint.GetAsync(route).Result;
            //    //var respon2 = objClint.GetAsync(route2).ConfigureAwait(true);
            //    Assert.Equal(respon.StatusCode.ToString(), HttpStatusCode.OK.ToString());
            //    //Assert.Equal(respon2, HttpStatusCode.NotFound.ToString());
            //}
        }
    }
}
