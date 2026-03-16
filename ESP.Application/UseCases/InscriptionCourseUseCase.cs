using ESP.Application.DTOS;
using ESP.Domain.Interfaces.Repositories;
using ESP.Domain.Interfaces.Security;
using ESP.Infrastructure;

namespace ESP.Application.UseCases
{
    public class InscriptionCourseUseCase
    {
        //Aide de l'ia ici
        //Cette partie est appelée après que Stipe revois les données de validations
        //Le retour de Stripe est vérifié et validé. 
        //récupère idUser et idRace, il génére un numéro de dossrd et insert dans la base de données
        private readonly IInscriptionRepository _inscriptionRepository;
        private readonly IStripeService _stripeService;

        public InscriptionCourseUseCase(
               IInscriptionRepository inscriptionRepository,
               IStripeService stripeService)
        {
            _inscriptionRepository = inscriptionRepository;
            _stripeService = stripeService;
        }

        public async Task ExecuteAsync(string json, string signature)
        {
            var metadata = await _stripeService.VerifyAndExtractAsync(json, signature);
            if (metadata == null) return;

            var idUser = int.Parse(metadata["idUser"]);
            var idRace = int.Parse(metadata["idRace"]);


            var exists = await _inscriptionRepository.AlreadyExistsAsync(idUser, idRace);
            if (exists) return;

            var bibNumber = await _inscriptionRepository.GenerateBibNumberAsync(idRace);

            var registration = new Registration
            {
                IdUser = idUser,
                IdRace = idRace,
                BibNumber = bibNumber,
            };

            await _inscriptionRepository.AddAsync(registration);
            await _inscriptionRepository.SaveChangesAsync();
        }
    }
}
