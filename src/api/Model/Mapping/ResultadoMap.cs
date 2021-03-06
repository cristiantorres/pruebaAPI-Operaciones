﻿using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperacionesApi.Model.Mapping
{
    /// <summary>
    ///  Mapeo de la clase Resultado
    /// </summary>
    public class ResultadoMap: ClassMapper<Resultado>
    {
        public ResultadoMap()
        {
            Table("resultados");
            Map(c => c.Id).Column("id").Key(KeyType.Assigned);
            Map(c => c.Result).Column("resultado");
        }
        
    }
}
