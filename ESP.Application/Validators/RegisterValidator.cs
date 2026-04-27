using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESP.Application.DTOS;
using FluentValidation;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.mail).NotEmpty().EmailAddress();
        RuleFor(x => x.password)
           .NotEmpty()
           .MinimumLength(12)
           .Matches("[A-Z]").WithMessage("Le mot de passe doit contenir au moins une majuscule")
           .Matches("[0-9]").WithMessage("Le mot de passe doit contenir au moins un chiffre")
           .Matches("[^a-zA-Z0-9]").WithMessage("Le mot de passe doit contenir un caractère spécial");

        RuleFor(x => x.firstName).NotEmpty();
        RuleFor(x => x.lastName).NotEmpty();
        RuleFor(x => x.nationality).NotEmpty();
    }
}