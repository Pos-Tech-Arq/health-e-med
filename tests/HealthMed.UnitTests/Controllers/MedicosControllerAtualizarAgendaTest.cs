using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HealthMed.UnitTests.Controllers;

public class MedicosControllerAtualizarAgendaTest
{
    protected MedicosController _medicosController;
    protected Mock<ICriarAlterarAgendaService> _criarAlterarAgendaService;
    protected Mock<IFiltrarAgendaService> _filtrarAgendaService;

    public MedicosControllerAtualizarAgendaTest()
    {
        _criarAlterarAgendaService = new Mock<ICriarAlterarAgendaService>();
        _filtrarAgendaService = new Mock<IFiltrarAgendaService>();

        _medicosController =
            new MedicosController(_criarAlterarAgendaService.Object, _filtrarAgendaService.Object);
    }

    [Fact]
    public async Task AtualizarAgenda_ValidMedicoId_ReturnsOk()
    {
        // Arrange
        var medicoId = Guid.NewGuid();
        var agendaId = Guid.NewGuid();
        var atualizarAgendaCommand = new AtualizarAgendaCommand(DateTime.Now, TimeSpan.FromHours(9), TimeSpan.FromHours(10), 200);

        _medicosController.ControllerContext.HttpContext = new DefaultHttpContext();
        _medicosController.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("UsuarioId", medicoId.ToString()) 
        }));

        _criarAlterarAgendaService
            .Setup(service => service.Handle(agendaId, atualizarAgendaCommand))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _medicosController.AtualizarAgenda(medicoId, agendaId, atualizarAgendaCommand);

        // Assert
        Assert.IsType<OkResult>(result); 
        _criarAlterarAgendaService.Verify(service => service.Handle(agendaId, atualizarAgendaCommand), Times.Once); // Verifica se o serviço foi chamado uma vez
    }

    [Fact]
    public async Task AtualizarAgenda_InvalidMedicoId_ReturnsForbid()
    {
        // Arrange
        var medicoId = Guid.NewGuid();
        var agendaId = Guid.NewGuid();
        var atualizarAgendaCommand = new AtualizarAgendaCommand(DateTime.Now, TimeSpan.FromHours(9), TimeSpan.FromHours(10), 200);

        _medicosController.ControllerContext.HttpContext = new DefaultHttpContext();
        _medicosController.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("UsuarioId", Guid.NewGuid().ToString()) 
        }));

        // Act
        var result = await _medicosController.AtualizarAgenda(medicoId, agendaId, atualizarAgendaCommand);

        // Assert
        Assert.IsType<ForbidResult>(result); 
        _criarAlterarAgendaService.Verify(service => service.Handle(It.IsAny<Guid>(), It.IsAny<AtualizarAgendaCommand>()), Times.Never); // Verifica se o serviço NÃO foi chamado
    }

}
