using ESP.Application.DTOS;
using ESP.Domain.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Application.UseCases
{
    public class CreationPayementUseCase
    //Aide de l'ia pour toute la partie 
    //Cette partie est appeler lors de la soumission du formulaire et ne fais pas l'ajout dans la bd
    //Permet de créer le "payement" 
    {
        private readonly IStripeService _stripeService;

        public CreationPayementUseCase(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }

        public async Task<string> ExecuteAsync(InscriptionDto dto)
        {
            return await _stripeService.CreatePaymentIntentAsync(dto.IdRace,dto.IdUser,dto.Price);
        }
    }
}
