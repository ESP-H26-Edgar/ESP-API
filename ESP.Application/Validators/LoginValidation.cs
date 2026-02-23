using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESP.Application.DTOS;
using FluentValidation;

namespace ESP.Application.Validators
{
    public class LoginValidation : AbstractValidator<LoginDto>
    {
        public LoginValidation()
        {
            RuleFor(x => x.mail)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.password)
               .NotEmpty()
               .MaximumLength(200);

        }
    }
}
