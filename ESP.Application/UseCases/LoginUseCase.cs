
using ESP.Application.DTOS;
using ESP.Domain.Interfaces.Repositories;
using ESP.Domain.Interfaces.Security;
using ESP.Infrastructure.Security;
using FluentValidation;
using FluentValidation.Results;


namespace ESP.Application.UseCases
{
    public class LoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<LoginDto> _validator;
        private readonly IPasswordHasher _passwordHasher;

        public LoginUseCase(IUserRepository userRepository,IValidator<LoginDto> validator,IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _validator = validator;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginDto> Execute(LoginDto loginDto)
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

            return loginDto;
        }
    }
}