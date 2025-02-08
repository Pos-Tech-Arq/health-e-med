using HealthMed.Application.Contracts;
using HealthMed.Application.Responses;
using HealthMed.Application.Services;
using HealthMed.Core.Enums;
using HealthMed.Core.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace HealthMed.UnitTests.Services;

public class AutenticaUsuarioServiceTest
{
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly Mock<IGerarTokenService> _tokenServiceMock;
    private readonly Mock<SignInManager<Usuario>> _signInManagerMock;
    private readonly Mock<UserManager<Usuario>> _userManagerMock;
    private readonly IAutenticaUsuarioService _autenticaUsuarioService;

    public AutenticaUsuarioServiceTest()
    {
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _tokenServiceMock = new Mock<IGerarTokenService>();
        _userManagerMock = new Mock<UserManager<Usuario>>(
            Mock.Of<IUserStore<Usuario>>(),
            null, null, null, null, null, null, null, null);
        _signInManagerMock = new Mock<SignInManager<Usuario>>(
            _userManagerMock.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<Usuario>>(),
            null, null, null, null);

        _autenticaUsuarioService = new AutenticaUsuarioService(
            _usuarioRepositoryMock.Object,
            _tokenServiceMock.Object,
            _signInManagerMock.Object
        );
    }

    [Fact]
    public async Task Handle_RegistrarUsuarioCommand_ShouldReturnToken()
    {
        // Arrange
        var command = new RegistrarUsuarioCommand
        {
            Nome = "John Doe",
            Email = "john.doe@example.com",
            Senha = "password123",
            Tipo = TipoUsuario.Paciente,
            Cpf = "12345678900",
            Crm = null,
            Especialidade = null
        };

        var usuario = new Usuario(command.Nome, command.Email, command.Tipo, command.Cpf, command.Crm, command.Especialidade);
        var usuarioLoginResponse = new UsuarioLoginResponse
        {
            AccessToken = "mock-token",
            ExpiresIn = 3600,
            UsuarioToken = new UsuarioToken { Id = Guid.NewGuid(), Email = command.Email }
        };

        _usuarioRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Usuario>(), command.Senha)).Returns(Task.CompletedTask);
        _signInManagerMock.Setup(s => s.PasswordSignInAsync(command.Email, command.Senha, false, false))
            .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
        _tokenServiceMock.Setup(t => t.GerarJwt(It.IsAny<Usuario>())).ReturnsAsync(usuarioLoginResponse);

        // Act
        var result = await _autenticaUsuarioService.Handle(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("mock-token", result.AccessToken);
        _usuarioRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Usuario>(), command.Senha), Times.Once);
        _signInManagerMock.Verify(s => s.PasswordSignInAsync(command.Email, command.Senha, false, false), Times.Once);
        _tokenServiceMock.Verify(t => t.GerarJwt(It.IsAny<Usuario>()), Times.Once);
    }
}
