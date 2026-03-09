using Azure;
using ESP.Application.DTOS;
using ESP.Application.UseCases;
using FluentValidation;
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
            try
            {
                var token = await _loginUseCase.Execute(loginDto);
                //cookie fait avec l'ia
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,        
                    Secure = true,          
                    SameSite = SameSiteMode.Strict, 
                    Expires = DateTimeOffset.UtcNow.AddHours(8)
                };

                Response.Cookies.Append("auth_token", token, cookieOptions);

                return Ok(new { message = "Connexion réussie" });
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}

