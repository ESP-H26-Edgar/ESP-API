using Azure;
using ESP.Application.DTOS;
using ESP.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace ESP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase

    {
        private readonly LoginUseCase _loginUseCase;

        public LoginController(LoginUseCase loginUseCase)
        {
            _loginUseCase = loginUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _loginUseCase.Execute(loginDto);

            return Ok(result);


        }

       
    }
}
