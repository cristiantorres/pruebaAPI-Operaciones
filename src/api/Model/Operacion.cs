using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperacionesApi.Model
{
    public class Operacion
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public int FirstValue { get; set; }
        public int SecondValue { get; set; }

    }
}
