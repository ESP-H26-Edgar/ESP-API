using ESP.Application.DTOS;
using ESP.Application.UseCases.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ESP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RaceTypeController : ControllerBase
    {
        private readonly IGetAllRaceTypeUseCase _getAllRaceTypeUseCase;
        public RaceTypeController(IGetAllRaceTypeUseCase getAllRaceTypeUseCase)
        {
            _getAllRaceTypeUseCase = getAllRaceTypeUseCase;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RaceTypeDto>>> GetAll()
        {
            var types = await _getAllRaceTypeUseCase.Execute();
            return Ok(types);
        }
    }
}