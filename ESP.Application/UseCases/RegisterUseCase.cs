using ESP.Application.DTOS;
using ESP.Domain.Interfaces.Repositories;
using ESP.Domain.Interfaces.Security;
using ESP.Infrastructure;
using FluentValidation;

namespace ESP.Application.UseCases
{
    public class RegisterUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IValidator<RegisterDto> _validator;

        public RegisterUseCase(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IValidator<RegisterDto> validator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _validator = validator;
        }

        public async Task Execute(RegisterDto dto)
        {
            var validation = await _validator.ValidateAsync(dto);

            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            var existingUser = await _userRepository.GetByEmailAsync(dto.mail);

            if (existingUser != null)
                throw new Exception("User already exists");

            var user = new User
            {
                LastName = dto.lastName,
                FirstName = dto.firstName,
                Gender = dto.gender,
                BirthDate = dto.birthDate,
                Nationality = dto.nationality,
                ClubTeam = dto.clubTeam,
                Mail = dto.mail,
                Password = _passwordHasher.Hash(dto.password),
                IsAdmin = true,
            };

            await _userRepository.AddAsync(user);
        }
    }
}