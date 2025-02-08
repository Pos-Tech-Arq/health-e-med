using FluentValidation.Results;
using HealthMed.Api.Requests.Validators;
using HealthMed.Application.Responses;
using HealthMed.Core.Enums;

namespace HealthMed.UnitTests.Controllers;

public class AuthControllerTests
{
    private readonly Mock<IAutenticaUsuarioService> _autenticaUsuarioServiceMock;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _autenticaUsuarioServiceMock = new Mock<IAutenticaUsuarioService>();
        _controller = new AuthController(_autenticaUsuarioServiceMock.Object);
    }

    [Fact]
    public async Task Register_ShouldReturnBadRequest_WhenCommandIsNull()
    {
        // Act
        var result = await _controller.Register(null);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.IsType<BadRequestObjectResult>(badRequestResult);
        Assert.Equal("A request não pode ser vazia.", badRequestResult.Value);
    }

    [Fact]
    public async Task Register_ShouldReturnBadRequest_WhenValidationFails()
    {
        // Arrange
        var command = new RegistrarUsuarioCommand
        {
            Nome = "",
            Email = "email_invalido",
            Senha = "123",
            Tipo = TipoUsuario.Medico,
            Cpf = "123",
            Crm = "",
            Especialidade = ""
        };

        var validator = new RegistrarUsuarioCommandValidator();
        var validationResult = await validator.ValidateAsync(command);

        // Validação falha porque os campos estão incorretos
        Assert.False(validationResult.IsValid);

        // Act
        var result = await _controller.Register(command);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var errors = Assert.IsType<List<ValidationFailure>>(badRequestResult.Value);
        Assert.NotEmpty(errors);
    }


    [Fact]
    public async Task Register_ShouldReturnOk_WhenValidationSuccessful()
    {
        // Arrange
        var command = new RegistrarUsuarioCommand
        {
            Nome = "Alex Nalim",
            Email = "alex@teste.com",
            Senha = "Senha123",
            Tipo = TipoUsuario.Medico,
            Cpf = "12345678901",
            Crm = "123456",
            Especialidade = "Cardiologia"
        };

        var response = new UsuarioLoginResponse
        {
            AccessToken = "fake-access-token",
            ExpiresIn = 3600,
            UsuarioToken = new UsuarioToken
            {
                Id = Guid.NewGuid(),
                Email = "alex@teste.com",
                Claims = new List<UsuarioClaim>
            {
                new UsuarioClaim { Type = "role", Value = "Medico" },
                new UsuarioClaim { Type = "permission", Value = "write" }
            }
            }
        };

        _autenticaUsuarioServiceMock
            .Setup(s => s.Handle(command))
            .ReturnsAsync(response);

        // Act
        var result = await _controller.Register(command);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedResponse = Assert.IsType<UsuarioLoginResponse>(okResult.Value);

        Assert.Equal(response.AccessToken, returnedResponse.AccessToken);
        Assert.Equal(response.ExpiresIn, returnedResponse.ExpiresIn);
        Assert.Equal(response.UsuarioToken.Id, returnedResponse.UsuarioToken.Id);
        Assert.Equal(response.UsuarioToken.Email, returnedResponse.UsuarioToken.Email);
        Assert.Equal(response.UsuarioToken.Claims.Count(), returnedResponse.UsuarioToken.Claims.Count());
        Assert.Contains(returnedResponse.UsuarioToken.Claims, c => c.Type == "role" && c.Value == "Medico");
        Assert.Contains(returnedResponse.UsuarioToken.Claims, c => c.Type == "permission" && c.Value == "write");
    }

    [Fact]
    public async Task Register_ShouldReturnOk_WithUsuarioLoginResponse_WhenUserIsRegisteredSuccessfully()
    {
        // Arrange
        var command = new RegistrarUsuarioCommand
        {
            Nome = "Alex Nalim",
            Email = "alex@teste.com",
            Senha = "Senha123",
            Tipo = TipoUsuario.Medico,
            Cpf = "12345678901",
            Crm = "123456",
            Especialidade = "Cardiologia"
        };

        var expectedResponse = new UsuarioLoginResponse
        {
            AccessToken = "fake-access-token",
            ExpiresIn = 3600,
            UsuarioToken = new UsuarioToken
            {
                Id = Guid.NewGuid(),
                Email = "alex@teste.com",
                Claims = new List<UsuarioClaim>
            {
                new UsuarioClaim { Type = "role", Value = "Medico" },
                new UsuarioClaim { Type = "permission", Value = "write" }
            }
            }
        };

        _autenticaUsuarioServiceMock
            .Setup(s => s.Handle(It.IsAny<RegistrarUsuarioCommand>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.Register(command);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedResponse = Assert.IsType<UsuarioLoginResponse>(okResult.Value);

        Assert.NotNull(returnedResponse);
        Assert.Equal(expectedResponse.AccessToken, returnedResponse.AccessToken);
        Assert.Equal(expectedResponse.ExpiresIn, returnedResponse.ExpiresIn);
        Assert.NotNull(returnedResponse.UsuarioToken);
        Assert.Equal(expectedResponse.UsuarioToken.Email, returnedResponse.UsuarioToken.Email);
        Assert.Equal(expectedResponse.UsuarioToken.Claims.Count(), returnedResponse.UsuarioToken.Claims.Count());

        foreach (var claim in expectedResponse.UsuarioToken.Claims)
        {
            Assert.Contains(returnedResponse.UsuarioToken.Claims, c => c.Type == claim.Type && c.Value == claim.Value);
        }
    }

}
