using Infra.WebHost.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using Xunit;
using Assert = Xunit.Assert;

namespace OperacionesAPiTest
{
    public class OperacionesModuleTest : IClassFixture<PlatformApiTestFixture>
    {
        readonly PlatformApiTestFixture _platformFixture;
        readonly HttpClient _httpClient;
        public OperacionesModuleTest(PlatformApiTestFixture platformFixture,HttpClient httpClient)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            Environment.SetEnvironmentVariable("ASPNETCORE_HOSTINGSTARTUPASSEMBLIES", "OperacionesApi");
            _platformFixture = platformFixture;
            _httpClient = httpClient;
        }

        [Fact]
        public void PostTest()
        {
            var myContent = JsonConvert.SerializeObject(new {firstValue=23,secondValue=20 });
            //var result =  _httpClient.PostAsync("/operaciones",myContent).Result;
          

            // Construct an HttpContent from a StringContent
            HttpContent _Body = new StringContent(myContent);
            // and add the header to this object instance
            // optional: add a formatter option to it as well
     
            // synchronous request without the need for .ContinueWith() or await
            var response = _httpClient.PostAsync("/operaciones", _Body).Result;
            //Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(response.StatusCode.ToString(), HttpStatusCode.OK.ToString());
        }
    }
}
