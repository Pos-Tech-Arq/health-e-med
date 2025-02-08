using HealthMed.Application.Responses;

namespace HealthMed.UnitTests.Controllers;

public class MedicosControllerBuscarAgendasDisponiveisTest
{
    protected MedicosController _medicosController;
    protected Mock<ICriarAlterarAgendaService> _criarAlterarAgendaService;
    protected Mock<IFiltrarAgendaService> _filtrarAgendaService;

    public MedicosControllerBuscarAgendasDisponiveisTest()
    {
        _criarAlterarAgendaService = new Mock<ICriarAlterarAgendaService>();
        _filtrarAgendaService = new Mock<IFiltrarAgendaService>();

        _medicosController =
            new MedicosController(_criarAlterarAgendaService.Object, _filtrarAgendaService.Object);
    }

    [Fact]
    public async Task BuscarAgendasDisponiveis_ValidRequest_ReturnsOkWithData()
    {
        // Arrange
        var medicoId = Guid.NewGuid();
        var data = DateTime.Now;
        var expectedResponse = new List<AgendaDisponivelResponse>
        {
            new AgendaDisponivelResponse(data.ToString("MM/dd/yyyy"))
            {
                ValorConsulta = 200,
                HorariosDisponiveis = new List<TimeSpan> { TimeSpan.FromHours(9), TimeSpan.FromHours(10) }
            }
        };

        _filtrarAgendaService
            .Setup(service => service.FiltrarAgenda(medicoId, data))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _medicosController.BuscarAgendasDisponiveis(medicoId, data);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<List<AgendaDisponivelResponse>>(okResult.Value);
        Assert.Single(response);
        Assert.Equal(expectedResponse[0].Data, response[0].Data);
        Assert.Equal(expectedResponse[0].ValorConsulta, response[0].ValorConsulta);
        Assert.Equal(expectedResponse[0].HorariosDisponiveis, response[0].HorariosDisponiveis);
    }

    [Fact]
    public async Task BuscarAgendasDisponiveis_NoAvailableSchedules_ReturnsEmptyList()
    {
        // Arrange
        var medicoId = Guid.NewGuid();
        var data = DateTime.Now;
        var expectedResponse = new List<AgendaDisponivelResponse>();

        _filtrarAgendaService
            .Setup(service => service.FiltrarAgenda(medicoId, data))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _medicosController.BuscarAgendasDisponiveis(medicoId, data);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<List<AgendaDisponivelResponse>>(okResult.Value);
        Assert.Empty(response);
    }

    [Fact]
    public async Task BuscarAgendasDisponiveis_NullDate_ReturnsOkWithData()
    {
        // Arrange
        var medicoId = Guid.NewGuid();
        DateTime? data = null;
        var expectedResponse = new List<AgendaDisponivelResponse>
        {
            new AgendaDisponivelResponse("10/12/2024")
            {
                ValorConsulta = 150,
                HorariosDisponiveis = new List<TimeSpan> { TimeSpan.FromHours(8), TimeSpan.FromHours(9) }
            }
        };

        _filtrarAgendaService
            .Setup(service => service.FiltrarAgenda(medicoId, null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _medicosController.BuscarAgendasDisponiveis(medicoId, data);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<List<AgendaDisponivelResponse>>(okResult.Value);
        Assert.Single(response);
        Assert.Equal(expectedResponse[0].Data, response[0].Data);
        Assert.Equal(expectedResponse[0].ValorConsulta, response[0].ValorConsulta);
        Assert.Equal(expectedResponse[0].HorariosDisponiveis, response[0].HorariosDisponiveis);
    }
}
