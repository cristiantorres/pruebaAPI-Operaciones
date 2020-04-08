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
        /// Test unitario metodo Post del Modulo Operaciones
        /// </summary>
      //  [Fact]
        public void PostOperaciones()
        {
            var client = _platformFixture.Client;
            var myContent = JsonConvert.SerializeObject(new { firstValue = 23, secondValue = 20, type = "suma" });
            var myContent2 = JsonConvert.SerializeObject(new { firstValue = 23, secondValue = 20, type = "resta" });

        //    // Construct an HttpContent from a StringContent
        //    HttpContent _Body1 = new StringContent(myContent,Encoding.UTF8, "application/json");
        //    HttpContent _Body2 = new StringContent(myContent2);

        //    // synchronous request without the need for .ContinueWith() or await
        //    var response1 = client.PostAsync("/operaciones", _Body1).Result;
        //    var response2 = client.PostAsync("/operaciones", _Body2).Result;
        //    //Assert.Equal(HttpStatusCode.OK, result.StatusCode);


        //    var baseUri = new Uri("http://localhost:5000/");
        //    var route = "/operaciones";
        //    using (var objClint = new HttpClient())
        //    {
        //        objClint.BaseAddress = baseUri;
        //        var respon =  objClint.PostAsync(route, _Body1).Result;
        //    }
        //    Assert.Equal(response1.StatusCode.ToString(), HttpStatusCode.OK.ToString());
        //    Assert.Equal(response2.StatusCode.ToString(), HttpStatusCode.UnprocessableEntity.ToString());
        }
    }
}
