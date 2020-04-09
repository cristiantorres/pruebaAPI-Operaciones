using DapperExtensions.Mapper;

namespace OperacionesApi.Model.Mapping
{
    /// <summary>
    ///  Mapeo de la clase Resultado
    /// </summary>
    public class ResultadoMap : ClassMapper<Resultado>
    {
        public ResultadoMap()
        {
            Table("resultados");
            Map(c => c.Id).Column("id").Key(KeyType.Assigned);
            Map(c => c.Result).Column("resultado");
        }

    }
}
