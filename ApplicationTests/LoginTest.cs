using System;
using ESP.Application.DTOS;
using ESP.Application.UseCases;
using ESP.Application.Validators;
using ESP.Domain.Interfaces.Repositories;
using ESP.Domain.Interfaces.Security;
using ESP.Infrastructure;
using FluentValidation;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace ESPTests;

public class LoginTest
{
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<IJwtTokenService> _jwtTokenServiceMock;
    private Mock<IPasswordHasher> _passwordHasherMock;
    private LoginUseCase _loginUseCase;
    
    private IValidator<LoginDto> _loginValidator;
    User user = new User { Mail = "test@mail.com", Password = "1234" };
    


    [SetUp]
    public void Setup()
    {
        _loginValidator = new LoginValidation();
        _userRepositoryMock = new Mock<IUserRepository>();
        _jwtTokenServiceMock = new Mock<IJwtTokenService>();
        _passwordHasherMock = new Mock<IPasswordHasher>();

        _loginUseCase = new LoginUseCase(_userRepositoryMock.Object, _loginValidator ,_passwordHasherMock.Object, _jwtTokenServiceMock.Object
    );
    }

    [Test]
    public async Task Login_ShouldReturnAToken()
    {
       
        var dto = new LoginDto
        {
            mail = "test@test.com",
            password = "password123"
        };

        _userRepositoryMock
        .Setup(x => x.GetByEmailAsync(dto.mail))
        .ReturnsAsync(user);

        _passwordHasherMock
            .Setup(x => x.Verify(dto.password, user.Password))
            .Returns(true);

        _jwtTokenServiceMock
            .Setup(x => x.GenerateToken(user.Mail, user.IsAdmin, user.IdUser))
            .Returns("fakeToken");

        var result = await _loginUseCase.Execute(dto);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo("fakeToken"));

    }
    [Test]
    public void Login_ShouldThrowException_WhenUserNotFound()
    {
        var dto = new LoginDto
        {
            mail = "unknown@test.com",
            password = "123"
        };

        _userRepositoryMock
            .Setup(x => x.GetByEmailAsync(dto.mail))
            .ReturnsAsync((User)null);

        Assert.That(async () =>
            await _loginUseCase.Execute(dto),
            Throws.Exception
);
    }
    [Test]
    public void Login_ShouldThrowException_WhenPasswordInvalid()
    {
       
        var dto = new LoginDto
        {
            mail = "test@test.com",
            password = "wrongpass"
        };

        _userRepositoryMock
            .Setup(x => x.GetByEmailAsync(dto.mail))
            .ReturnsAsync(user);

        _passwordHasherMock
            .Setup(x => x.Verify(dto.password, user.Password))
            .Returns(false);

        Assert.ThrowsAsync<Exception>(
            async () => await _loginUseCase.Execute(dto)
        );
    }

}