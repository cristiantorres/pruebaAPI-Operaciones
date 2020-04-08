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
        /// Test unitario metodo Get del Modulo Resultados
        /// </summary>
        [Fact]
        public void GetResultadosAsync()
        {
            string idValido = "e19c3";
            string idNoValido = "e100c3";
            var baseAdrees =  "http://localhost:5000/api" ;
            var route = $"/resultados/{idValido}";
            var route2 = $"/resultados/{idNoValido}";
            var route3 = $"/resultados/";
            var client = _platformFixture.Client;
            //client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            /*Caso Ok*/
            var result = client.GetAsync($"{baseAdrees}{route}").Result;
            Assert.Equal(HttpStatusCode.OK,result.StatusCode);

           /*Caso NotFound*/
            result = client.GetAsync($"{baseAdrees}{route2}").Result;
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);

            /*Caso Ruta erronea */
            result = client.GetAsync($"{baseAdrees}{route3}").Result;
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
