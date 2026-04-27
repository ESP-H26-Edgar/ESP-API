using Azure;
using ESP.Application.DTOS;
using ESP.Application.UseCases;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ESP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase

    {
        private readonly LoginUseCase _loginUseCase;
        private readonly RegisterUseCase _registerUseCase;

        public LoginController(LoginUseCase loginUseCase, RegisterUseCase registerUseCase)
        {
            _loginUseCase = loginUseCase;
            _registerUseCase = registerUseCase;
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var idUserClaim = User.Claims.FirstOrDefault(c => c.Type == "idUser");
            if (idUserClaim == null) return Unauthorized();

            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            return Ok(new
            {
                idUser = int.Parse(idUserClaim.Value),
                role
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _loginUseCase.Execute(loginDto);
                //cookie fait avec l'ia
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,        
                    Secure = true,
                    SameSite = SameSiteMode.None,
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
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                await _registerUseCase.Execute(dto);
                return Ok(new { message = "Utilisateur créé" });
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

