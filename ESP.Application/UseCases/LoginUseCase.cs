
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ESP.Application.DTOS;
using ESP.Domain.Interfaces.Repositories;
using ESP.Domain.Interfaces.Security;
using ESP.Infrastructure.Security;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.IdentityModel.Tokens;


namespace ESP.Application.UseCases
{
    public class LoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<LoginDto> _validator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginUseCase(IUserRepository userRepository,IValidator<LoginDto> validator,IPasswordHasher passwordHasher, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _validator = validator;
            _jwtTokenService = jwtTokenService;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> Execute(LoginDto loginDto)
        {
            
           ValidationResult validationResult =await _validator.ValidateAsync(loginDto);
                       
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var user = await _userRepository.GetByEmailAsync(loginDto.mail);

            if (user == null)
                throw new Exception("Invalid username");
          
            var password = _passwordHasher.Verify(loginDto.password, user.Password);

            if (!password)
                throw new Exception("Invalid  password");

            return _jwtTokenService.GenerateToken(user.Mail, user.IsAdmin);
        }

        public string GenerateToken(string mail, bool isAdmin)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("D452qs456453qsdKBHF!;WXD!Hds241F"));

            var claims = new[]
            {
            new Claim(ClaimTypes.Email, mail),
            new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
        };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}