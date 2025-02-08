using FluentValidation;
using FluentValidation.Results;
using HealthMed.Api.Requests.Validators;
using HealthMed.Application.Responses;

namespace HealthMed.UnitTests.Controllers;

public class AuthControllerLoginMedicoTest
{
    private readonly Mock<IAutenticaUsuarioService> _autenticaUsuarioServiceMock;
    private readonly AuthController _controller;

    public AuthControllerLoginMedicoTest()
    {
        _autenticaUsuarioServiceMock = new Mock<IAutenticaUsuarioService>();
        _controller = new AuthController(_autenticaUsuarioServiceMock.Object);
    }

    [Fact]
    public async Task LoginMedico_ShouldReturnBadRequest_WhenCommandIsNull()
    {
        // Act
        var result = await _controller.LoginMedico(null);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("A request não pode ser vazia.", badRequestResult.Value);
    }

    [Fact]
    public async Task LoginMedico_ShouldReturnBadRequest_WhenValidationFails()
    {
        // Arrange
        var command = new LoginMedicoCommand { Crm = "", Senha = "123" }; 

        var validator = new LoginMedicoCommandValidator();
        var validationResult = await validator.ValidateAsync(command);

        _autenticaUsuarioServiceMock
            .Setup(s => s.Handle(It.IsAny<LoginPacienteCommand>()))
            .ThrowsAsync(new ValidationException(validationResult.Errors));

        // Act
        var result = await _controller.LoginMedico(command);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var errors = Assert.IsType<List<ValidationFailure>>(badRequestResult.Value);
        Assert.NotEmpty(errors);
    }

    [Fact]
    public async Task LoginMedico_ShouldReturnOk_WhenLoginIsSuccessful()
    {
        // Arrange
        var command = new LoginMedicoCommand { Crm = "123456", Senha = "SenhaForte" };

        var expectedResponse = new UsuarioLoginResponse
        {
            AccessToken = "token123",
            ExpiresIn = 3600,
            UsuarioToken = new UsuarioToken
            {
                Id = Guid.NewGuid(),
                Email = "medico@hospital.com",
                Claims = new List<UsuarioClaim>
            {
                new UsuarioClaim { Type = "role", Value = "Medico" }
            }
            }
        };

        _autenticaUsuarioServiceMock
            .Setup(s => s.Handle(It.IsAny<LoginMedicoCommand>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.LoginMedico(command);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var loginResponse = Assert.IsType<UsuarioLoginResponse>(okResult.Value);

        Assert.Equal(expectedResponse.AccessToken, loginResponse.AccessToken);
        Assert.Equal(expectedResponse.ExpiresIn, loginResponse.ExpiresIn);
        Assert.Equal(expectedResponse.UsuarioToken.Email, loginResponse.UsuarioToken.Email);
    }
}
