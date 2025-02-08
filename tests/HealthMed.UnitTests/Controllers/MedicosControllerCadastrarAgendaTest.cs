using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HealthMed.UnitTests.Controllers;

public class MedicosControllerCadastrarAgendaTest
{
    protected MedicosController _medicosController;
    protected Mock<ICriarAlterarAgendaService> _criarAlterarAgendaService;
    protected Mock<IFiltrarAgendaService> _filtrarAgendaService;

    public MedicosControllerCadastrarAgendaTest()
    {
        _criarAlterarAgendaService = new Mock<ICriarAlterarAgendaService>();
        _filtrarAgendaService = new Mock<IFiltrarAgendaService>();

        _medicosController =
            new MedicosController(_criarAlterarAgendaService.Object, _filtrarAgendaService.Object);
    }


    [Fact]
    public async Task CadastrarAgenda_ValidMedicoId_ReturnsOk()
    {
        // Arrange
        var medicoId = Guid.NewGuid();
        var cadastroAgendaCommand = new CadastroAgendaCommand(DateTime.Now, TimeSpan.FromHours(10), TimeSpan.FromHours(11), 150);
        _medicosController.ControllerContext.HttpContext = new DefaultHttpContext();
        _medicosController.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("UsuarioId", medicoId.ToString())
        }));

        // Act
        var result = await _medicosController.CadastrarAgenda(medicoId, cadastroAgendaCommand);

        // Assert
        var actionResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, actionResult.StatusCode);
        _criarAlterarAgendaService.Verify(service => service.Handle(medicoId, cadastroAgendaCommand), Times.Once);
    }

    [Fact]
    public async Task CadastrarAgenda_InvalidMedicoId_ReturnsForbid()
    {
        // Arrange
        var medicoId = Guid.NewGuid();
        var cadastroAgendaCommand = new CadastroAgendaCommand(DateTime.Now, TimeSpan.FromHours(10), TimeSpan.FromHours(11), 150);
        _medicosController.ControllerContext.HttpContext = new DefaultHttpContext();
        _medicosController.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("UsuarioId", Guid.NewGuid().ToString()) 
        }));

        // Act
        var result = await _medicosController.CadastrarAgenda(medicoId, cadastroAgendaCommand);

        // Assert
        Assert.IsType<ForbidResult>(result);
        _criarAlterarAgendaService.Verify(service => service.Handle(It.IsAny<Guid>(), It.IsAny<CadastroAgendaCommand>()), Times.Never); 
    }
}
