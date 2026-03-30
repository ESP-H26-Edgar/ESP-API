using ESP.Application.DTOS;
using ESP.Application.UseCases.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RaceController : ControllerBase
    {
        private IGetAllRaceUseCase _getAllRaceUseCase;

        public RaceController(IGetAllRaceUseCase getAllRaceUseCase)
        {
            _getAllRaceUseCase = getAllRaceUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RaceDto>>> GetAllRace()
        {
            var race = await _getAllRaceUseCase.Execute();
            return Ok(race);
        }
    }
}
