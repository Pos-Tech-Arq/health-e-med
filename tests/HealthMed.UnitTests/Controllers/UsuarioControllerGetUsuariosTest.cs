using HealthMed.Api.Requests;
using HealthMed.Api.Responses;
using HealthMed.Core.Enums;


namespace HealthMed.UnitTests.Controllers;

public class UsuarioControllerGetUsuariosTest 
{
    protected UsuarioController _usuarioController;
    protected Mock<IUsuarioRepository> _usuarioRepository;

    public UsuarioControllerGetUsuariosTest()
    {
        _usuarioRepository = new Mock<IUsuarioRepository>();
        _usuarioController = new UsuarioController(_usuarioRepository.Object);
    }
    [Fact]
    public async Task GetUsuarios_ShouldReturnOk_WhenUsuariosExist()
    {
        // Arrange
        var request = new GetUsuariosRequest { TipoUsuario = TipoUsuario.Medico, Especialidade = "Cardiologia" };
        var usuarios = new List<Usuario>
        {
            new Usuario(
                nome: "João Silva",
                email: "joao@email.com",
                tipo: TipoUsuario.Medico,
                cpf: "12345678900",
                crm: "CRM123",
                especialidade: "Cardiologia"
            )
        };

        _usuarioRepository
            .Setup(repo => repo.GetUsuarios(It.IsAny<TipoUsuario>(), It.IsAny<string>()))
            .ReturnsAsync(usuarios);

        // Act
        var result = await _usuarioController.GetUsuarios(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var usuariosResponse = Assert.IsAssignableFrom<IEnumerable<UsuarioReponse>>(okResult.Value);
        Assert.Single(usuariosResponse);
    }

    [Fact]
    public async Task GetUsuarios_ShouldReturnOk_WhenNoUsuariosExist()
    {
        // Arrange
        var request = new GetUsuariosRequest { TipoUsuario = TipoUsuario.Paciente, Especialidade = null };
       
        _usuarioRepository
          .Setup(repo => repo.GetUsuarios(It.IsAny<TipoUsuario>(), It.IsAny<string>()))
                     .ReturnsAsync(new List<Usuario>());

        // Act
        var result = await _usuarioController.GetUsuarios(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var usuariosResponse = Assert.IsAssignableFrom<IEnumerable<UsuarioReponse>>(okResult.Value);
        Assert.Empty(usuariosResponse);
    }

    [Fact]
    public async Task GetUsuarios_ShouldReturnBadRequest_WhenRequestIsNull()
    {
        // Act
        var result = await _usuarioController.GetUsuarios(null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
