using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESP.Application.DTOS;
using FluentValidation;

public class RegisterValidaton : AbstractValidator<RegisterDto>
{
    public RegisterValidaton()
    {
        RuleFor(x => x.mail).NotEmpty().EmailAddress().WithMessage("L'adresse mail est requise");
        RuleFor(x => x.password)
         .Cascade(CascadeMode.Stop)

         .NotEmpty()
         .WithMessage("Le mot de passe est requis")

         .MinimumLength(12)
         .WithMessage("Le mot de passe doit contenir au moins 12 caractères")

         .Matches("[A-Z]")
         .WithMessage("Le mot de passe doit contenir une majuscule")

         .Matches("[0-9]")
         .WithMessage("Le mot de passe doit contenir un chiffre")

         .Matches("[^a-zA-Z0-9]")
         .WithMessage("Le mot de passe doit contenir un caractère spécial");

        RuleFor(x => x.firstName).NotEmpty().WithMessage("Le nom est requis");
        RuleFor(x => x.lastName).NotEmpty().WithMessage("Le prénom est requis");
        RuleFor(x => x.nationality).NotEmpty().WithMessage("La nationnalité est requise");
    }
}