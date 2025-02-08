using HealthMed.Application.Services;
using HealthMed.Core.Enums;
using HealthMed.Core.Extensions;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HealthMed.UnitTests.Services;

public class GerarTokenServiceTest
{

    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly Mock<IOptions<AppSettings>> _appSettingsMock;
    private readonly GerarTokenService _gerarTokenService;
    private readonly AppSettings _appSettings;

    public GerarTokenServiceTest()
    {
        _appSettings = new AppSettings
        {
            Secret = "thisisaverylongsecretkeythatis256bitslong12345678901234",  
            ExpiracaoHoras = 1,
            Emissor = "issuer",
            ValidoEm = "audience"
        };

        _appSettingsMock = new Mock<IOptions<AppSettings>>();
        _appSettingsMock.Setup(a => a.Value).Returns(_appSettings);

        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _usuarioRepositoryMock.Setup(u => u.GetClaimsAsync(It.IsAny<Usuario>()))
                              .ReturnsAsync(new List<Claim>
                              {
                                  new Claim("Name", "User Name"),
                                  new Claim("Role", "Admin")
                              });

        _gerarTokenService = new GerarTokenService(_usuarioRepositoryMock.Object, _appSettingsMock.Object);
    }

    [Fact]
    public async Task GerarJwt_ShouldReturn_UsuarioLoginResponse()
    {
        // Arrange
        var usuario = new Usuario("John Doe", "john@example.com", TipoUsuario.Medico, "123456789", null, null);

        // Act
        var result = await _gerarTokenService.GerarJwt(usuario);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(usuario.Email, result.UsuarioToken.Email);
        Assert.Equal(usuario.Id, result.UsuarioToken.Id);
        Assert.True(result.AccessToken.Length > 0);
    }

    [Fact]
    public async Task GerarJwt_ShouldReturn_ValidToken()
    {
        // Arrange
        var usuario = new Usuario("John Doe", "john@example.com", TipoUsuario.Medico, "123456789", null, null);

        // Act
        var result = await _gerarTokenService.GerarJwt(usuario);

        // Assert
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadToken(result.AccessToken) as JwtSecurityToken;

        Assert.NotNull(token);
        Assert.Equal(_appSettings.Emissor, token.Issuer);
        Assert.Equal(_appSettings.ValidoEm, token.Audiences.FirstOrDefault());
    }

}
