using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperacionesApi.Modules
{
    public class HomeModule: NancyModule
    {
        public HomeModule()
        {
            Get("openapi.yaml", (_) => Response.AsFile("openapi.yaml"));
        }
    }
}
