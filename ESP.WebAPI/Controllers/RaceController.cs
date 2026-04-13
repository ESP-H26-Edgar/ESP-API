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
        private readonly ICreateRaceUseCase _createRaceUseCase;
        public RaceController(IGetAllRaceUseCase getAllRaceUseCase, ICreateRaceUseCase createRaceUseCase)
        {
            _getAllRaceUseCase = getAllRaceUseCase;
            _createRaceUseCase = createRaceUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RaceDto>>> GetAllRace()
        {
            var race = await _getAllRaceUseCase.Execute();
            return Ok(race);

        }
        [HttpPost("Create")]
        public async Task<ActionResult<RaceDto>> CreateRace([FromForm] CreateRaceRequest request)
        {
            var race = await _createRaceUseCase.Execute(request);
            return Ok(race);
        }

    }
}
