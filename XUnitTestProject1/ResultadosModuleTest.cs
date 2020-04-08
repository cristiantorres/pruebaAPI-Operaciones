using Infra.WebHost.Test;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        /// En este caso, este Test unitario solo comprueba si el tipo de valor devuelto es NotFound.
        /// </summary>
        [Fact]
        public void GetResultadosAsyncNotfound()
        {
 
            string idNoValido = "e100c3";
            var route = $"api/resultados/{idNoValido}";
            var client = _platformFixture.Client;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

           /*Caso NotFound*/
            var  result = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        /// <summary>
        /// En este caso, este Test unitario solo comprueba si el tipo de valor devuelto es OK
        /// </summary>
        [Fact]
        public void GetResultadosAsyncOk()
        {
            string idValido = "e19c3"; 
            var route = $"api/resultados/{idValido}";
            var client = _platformFixture.Client;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            /*Caso Ok*/
            var result = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Content);
          //  Assert.Equal(42, contentResult.Content.Id);

        }

        /// <summary>
        /// Test unitario metodo Get del Modulo Resultados para el caso  de un path erroneo
        /// </summary>
        [Fact]
        public void GetResultadosAsyncPathErroneo()
        {
 
             var route = $"api/resultados/";
            var client = _platformFixture.Client;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            /*Caso Ruta erronea */
            var result = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);

        }
    }
}
