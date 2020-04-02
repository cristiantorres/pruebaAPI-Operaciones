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
            RuleFor(operation => operation.Type).Equal("suma") ;
        }
    }
}
