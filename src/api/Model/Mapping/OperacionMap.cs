using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperacionesApi.Model.Mapping
{   
    
    /// <summary>
     ///  Mapeo de la clase Operacion
     /// </summary>
    public class OperacionMap: ClassMapper<Operacion>  
    {
        public OperacionMap() {
            Table("operaciones");
            Map(c => c.Id).Column("id").Key(KeyType.Assigned);
            Map(c => c.FirstValue).Column("firstValue");
            Map(c => c.SecondValue).Column("secondValue");
            Map(c => c.Type).Column("type");
        }
    }
}
