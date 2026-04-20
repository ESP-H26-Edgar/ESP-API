using ESP.Domain.Interfaces.Repositories;
using ESP.Domain.Interfaces.Security;
using ESP.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ESP.Application.UseCases
{
    public class InscriptionCourseUseCase
    {
        private readonly IInscriptionRepository _inscriptionRepository;
        private readonly IStripeService _stripeService;
        private readonly ILogger<InscriptionCourseUseCase> _logger;

        public InscriptionCourseUseCase(
            IInscriptionRepository inscriptionRepository,
            IStripeService stripeService,
            ILogger<InscriptionCourseUseCase> logger)
        {
            _inscriptionRepository = inscriptionRepository;
            _stripeService = stripeService;
            _logger = logger;
        }

        public async Task ExecuteAsync(string json, string signature)
        {
            var metadata = await _stripeService.VerifyAndExtractAsync(json, signature);
            if (metadata == null)
            {
                _logger.LogWarning("Stripe metadata was null");
                return;
            }

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
                _logger.LogInformation("Participant already registered: {Nom} {Prenom}", nom, prenom);
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
            var currentCount = await _inscriptionRepository.CountByRaceAsync(idRace);
            var race = await _inscriptionRepository.GetRaceByIdAsync(idRace);

            if (currentCount >= race.NumberPlace)
            {
                _logger.LogWarning("Course full for race {IdRace}", idRace);
                throw new Exception("Course complète");
            }
            await _inscriptionRepository.AddAsync(registration);
            await _inscriptionRepository.SaveChangesAsync();

            _logger.LogInformation("New registration added: {Nom} {Prenom}, Bib #{BibNumber}", nom, prenom, bibNumber);
        }
    }
}