using Infra.WebHost.Test;
using Newtonsoft.Json;
using OperacionesApi.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace XUnitTestProject1
{
    public class OperacionesModuleTest: IClassFixture<PlatformApiTestFixture>
    {

        readonly PlatformApiTestFixture _platformFixture;

        /// <summary>
        /// Constructor de la clase OperacionesModuleTest con la inyeccion de dependencia de la instancia platformFixture
        /// </summary>
        /// <param name="platformFixture"></param>
        public OperacionesModuleTest(PlatformApiTestFixture platformFixture)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            Environment.SetEnvironmentVariable("ASPNETCORE_HOSTINGSTARTUPASSEMBLIES", "OperacionesApi");
            _platformFixture = platformFixture;
        }


        /// <summary>
        /// Test unitario metodo http/Post del Modulo Operaciones
        /// </summary>
        [Fact]
        public void PostOperacionesOK()
        {
            var client = _platformFixture.Client;
            var myContent = JsonConvert.SerializeObject(new { firstValue = 23, secondValue = 20, type = "suma" });
            HttpContent _Body1 = new StringContent(myContent, Encoding.UTF8, "application/json");
 
            var response1 = client.PostAsync("/api/operaciones", _Body1).Result;
            Assert.Equal(HttpStatusCode.Accepted, response1.StatusCode);
         }
        /// <summary>
        /// Test unitario metodo http/Post del Modulo Operaciones que devuelve errores en el  cliente (StatusCode 422)
        /// </summary>
        [Fact]
        public void PostOperacionesWithErrors()
        {
            var client = _platformFixture.Client;
            var contentWithErrors = JsonConvert.SerializeObject(new { firstValue = 23, secondValue = 20, type = "resta" });
            HttpContent _Body = new StringContent(contentWithErrors, Encoding.UTF8, "application/json");

            var response = client.PostAsync("/api/operaciones", _Body).Result;
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }
    }
}
