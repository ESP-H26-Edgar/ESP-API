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

            var idRace = int.Parse(metadata["idRace"]);
            var metaNormalized = metadata.ToDictionary(k => k.Key.ToLower(), v => v.Value);

            var prenom = metaNormalized["prenom"];
            var nom = metaNormalized["nom"];
            var adresseMail = metaNormalized["adressemail"];
            var phone = metaNormalized["phone"];
            var sexe = metaNormalized["sexe"];
            var dateNaissance = DateOnly.Parse(metaNormalized["datenaissance"]);

            bool exists = await _inscriptionRepository.AlreadyExistsAsync(
                idRace, nom, prenom, dateNaissance, adresseMail
             );
            if (exists)
            {
                Console.WriteLine("Cette personne est déjà inscrite pour cette course.");
                return;
            }

            var bibNumber = await _inscriptionRepository.GenerateBibNumberAsync(idRace);

            var registration = new Registration
            {
                IdRace = idRace,

                BibNumber = bibNumber,
                Nom = nom,
                Prenom = prenom,
                AdresseMail = adresseMail,
                Sexe = sexe,
                DateNaissance = dateNaissance,
                Phone = phone,
            };

            await _inscriptionRepository.AddAsync(registration);
            await _inscriptionRepository.SaveChangesAsync();
        }
    }
}
