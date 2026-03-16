using ESP.Application.UseCases;
using Microsoft.AspNetCore.Mvc;


namespace ESP.WebAPI.DTOS
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscriptionCourseController : ControllerBase
    {
        private readonly CreationPayementUseCase _creationPayementUseCase;
        private readonly InscriptionCourseUseCase _incriptionCourseUseCase;

        public InscriptionCourseController(
            CreationPayementUseCase creationPayementUseCase,
            InscriptionCourseUseCase incriptionCourseUseCase)
        {
            _creationPayementUseCase = creationPayementUseCase;
            _incriptionCourseUseCase = incriptionCourseUseCase;
        }

        [HttpPost("initier-paiement")]
        public async Task<IActionResult> CreateCheckoutSession(
        [FromBody] InscriptionDto dto)
        {
            var clientSecret = await _creationPayementUseCase.ExecuteAsync(dto);
            return Ok(new { clientSecret });
        }
        //aide de chat gpt pour la gestion de Stripe
        [HttpPost("confirmation-paiement")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var signature = Request.Headers["Stripe-Signature"].ToString();

            await _incriptionCourseUseCase.ExecuteAsync(json, signature);
            return Ok();
        }
    }
}
