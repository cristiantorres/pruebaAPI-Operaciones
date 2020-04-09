using FluentValidation;
using OperacionesApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperacionesApi.Modules.Validators
{
 
    public class OperacionesValidator : AbstractValidator<Operacion>
    {
        public OperacionesValidator()
        {
            RuleFor(operation => operation.FirstValue).Must(x => x > -999999 && x < 10000000).WithMessage("El campo firstValue no es correcto");
            RuleFor(operation => operation.SecondValue).Must(x => x > -999999 && x < 10000000).WithMessage("El campo secondValue no es correcto");
            RuleFor(operation => operation.Type).Equal("suma").WithMessage("El campo type debe ser 'suma'");
        }

    }
}
