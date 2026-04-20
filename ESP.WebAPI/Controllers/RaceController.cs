using ESP.Application.DTOS;
using ESP.Application.UseCases;
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
        private readonly IGetRaceByIdUseCase _getRaceByIdUseCase;
        private readonly IDeleteRaceUseCase _deleteRaceUseCase;
        public RaceController(IGetAllRaceUseCase getAllRaceUseCase, ICreateRaceUseCase createRaceUseCase, IGetRaceByIdUseCase getRaceByIdUseCase, IDeleteRaceUseCase deleteRaceUseCase)
        {
            _getAllRaceUseCase = getAllRaceUseCase;
            _createRaceUseCase = createRaceUseCase;
            _getRaceByIdUseCase = getRaceByIdUseCase;
            _deleteRaceUseCase = deleteRaceUseCase;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RaceDto>>> GetAllRace()
        {
            var race = await _getAllRaceUseCase.Execute();
            return Ok(race);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var race = await _getRaceByIdUseCase.ExecuteAsync(id);

            if (race == null)
                return NotFound();

            return Ok(race);

        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<ActionResult<RaceDto>> CreateRace([FromForm] CreateRaceRequest request)
        {
            var race = await _createRaceUseCase.Execute(request);
            return Ok(race);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _deleteRaceUseCase.ExecuteAsync(id);
            return NoContent();
        }
    }
}
