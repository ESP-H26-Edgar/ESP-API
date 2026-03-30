using Stripe;
using Microsoft.Extensions.Configuration;
using ESP.Domain.Interfaces.Security;

namespace ESP.Infrastructure.Security;

public class StripeService : IStripeService
{
    private readonly string _webhookSecret;

    //Cette partie à été faite avec Claude. 

    public StripeService(IConfiguration config)
    {
        StripeConfiguration.ApiKey = config["Stripe:SecretKey"]!;
        _webhookSecret = config["Stripe:WebhookSecret"]!;
    }

    //appelle l'api Stripe et créer un payement
    //Stock idUser + idRace
    //retourne un clientSecret au front
    public async Task<string> CreatePaymentIntentAsync(int idRace, decimal price, string prenom, string nom, string adreseMail, string phone, string sexe, DateOnly dateNaissance )
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(price * 100),
            Currency = "cad",
            Metadata = new Dictionary<string, string>
            {
                { "idRace", idRace.ToString() },
                { "Prenom", prenom },
                { "Nom", nom },
                { "AdresseMail", adreseMail },
                {"Phone", phone },
                { "Sexe", sexe },
                { "DateNaissance", dateNaissance.ToString("yyyy-MM-dd") }
            }
        };

        var service = new PaymentIntentService();
        var intent = await service.CreateAsync(options);
        return intent.ClientSecret;
    }
    //Vérifie sur le payement est bien validé, si c'est le cas il retourne idUser et idRace
    public Task<Dictionary<string, string>?> VerifyAndExtractAsync(
        string json, string signature)
    {
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(json, signature, _webhookSecret);

            if (stripeEvent.Type != EventTypes.PaymentIntentSucceeded)
                return Task.FromResult<Dictionary<string, string>?>(null);

            var intent = stripeEvent.Data.Object as PaymentIntent;
            var meta = new Dictionary<string, string>(intent!.Metadata)
            {
                ["paymentIntentId"] = intent.Id
            };

            return Task.FromResult<Dictionary<string, string>?>(meta);
        }
        catch (StripeException)
        {
            return Task.FromResult<Dictionary<string, string>?>(null);
        }
    }
}