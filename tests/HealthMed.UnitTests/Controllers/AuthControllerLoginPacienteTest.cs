using FluentValidation;
using FluentValidation.Results;
using HealthMed.Api.Requests.Validators;
using HealthMed.Application.Responses;


namespace HealthMed.UnitTests.Controllers;

public class AuthControllerLoginPacienteTest
{
    private readonly Mock<IAutenticaUsuarioService> _autenticaUsuarioServiceMock;
    private readonly AuthController _controller;

    public AuthControllerLoginPacienteTest()
    {
        _autenticaUsuarioServiceMock = new Mock<IAutenticaUsuarioService>();
        _controller = new AuthController(_autenticaUsuarioServiceMock.Object);
    }

    [Fact]
    public async Task LoginPaciente_ShouldReturnBadRequest_WhenCommandIsNull()
    {
        // Act
        var result = await _controller.LoginPaciente(null);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("A request não pode ser vazia.", badRequestResult.Value);
    }

    [Fact]
    public async Task LoginPaciente_ShouldReturnBadRequest_WhenValidationFails()
    {
        // Arrange
        var command = new LoginPacienteCommand { Cpf = "", Senha = "" }; // CPF e senha vazios

        var validator = new LoginPacienteCommandValidator();
        var validationResult = await validator.ValidateAsync(command);

        _autenticaUsuarioServiceMock
            .Setup(s => s.Handle(It.IsAny<LoginPacienteCommand>()))
            .ThrowsAsync(new ValidationException(validationResult.Errors));

        // Act
        var result = await _controller.LoginPaciente(command);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var errors = Assert.IsType<List<ValidationFailure>>(badRequestResult.Value);
        Assert.NotEmpty(errors);
    }

    [Fact]
    public async Task LoginPaciente_ShouldReturnOk_WhenLoginIsSuccessful()
    {
        // Arrange
        var command = new LoginPacienteCommand { Cpf = "12345678901", Senha = "Senha123" };

        var expectedResponse = new UsuarioLoginResponse
        {
            AccessToken = "fake-access-token",
            ExpiresIn = 3600,
            UsuarioToken = new UsuarioToken
            {
                Id = Guid.NewGuid(),
                Email = "paciente@teste.com",
                Claims = new List<UsuarioClaim>
            {
                new UsuarioClaim { Type = "role", Value = "Paciente" },
                new UsuarioClaim { Type = "permission", Value = "read" }
            }
            }
        };

        _autenticaUsuarioServiceMock
            .Setup(s => s.Handle(It.IsAny<LoginPacienteCommand>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.LoginPaciente(command);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedResponse = Assert.IsType<UsuarioLoginResponse>(okResult.Value);

        Assert.NotNull(returnedResponse);
        Assert.Equal(expectedResponse.AccessToken, returnedResponse.AccessToken);
        Assert.Equal(expectedResponse.ExpiresIn, returnedResponse.ExpiresIn);
        Assert.NotNull(returnedResponse.UsuarioToken);
        Assert.Equal(expectedResponse.UsuarioToken.Email, returnedResponse.UsuarioToken.Email);
        Assert.Equal(expectedResponse.UsuarioToken.Claims.Count(), returnedResponse.UsuarioToken.Claims.Count());
    }
}
