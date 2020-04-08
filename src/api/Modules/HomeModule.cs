using Carter;
using Carter.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperacionesApi.Modules
{
    public class HomeModule: CarterModule
    {
        public HomeModule()
        {
            Get("openapi.yaml", async (req, res) => await res.AsFile("openapi.yaml"));
        }
    }
}
