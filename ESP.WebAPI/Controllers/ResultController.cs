using ESP.Application.UseCases.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ESP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultController : ControllerBase
    {
        private readonly IGetResultsByRaceUseCase _getResultsByRaceUseCase;

        public ResultController(IGetResultsByRaceUseCase getResultsByRaceUseCase)
        {
            _getResultsByRaceUseCase = getResultsByRaceUseCase;
        }

        [HttpGet("race/{idRace}")]
        public async Task<IActionResult> GetByRace(int idRace)
        {
            var results = await _getResultsByRaceUseCase.Execute(idRace);
            return Ok(results);
        }
    }
}