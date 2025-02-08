using HealthMed.Application.Services;
using MediatR;
using Moq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace HealthMed.UnitTests.Services;

public class AgendarConsultaServiceTest
{

    private readonly Mock<IConsultaRepository> _consultaRepositoryMock;
    private readonly AgendarConsultaService _agendarConsultaService;

    public AgendarConsultaServiceTest()
    {
        _consultaRepositoryMock = new Mock<IConsultaRepository>();
        _agendarConsultaService = new AgendarConsultaService(_consultaRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_DeveChamarCreateDoRepositorio_QuandoDadosValidos()
    {

        var pacienteId = Guid.NewGuid();
        var medicoId = Guid.NewGuid();
        var data = DateTime.Now.Date;
        var horario = TimeSpan.FromHours(10);
        var command = new AgendarConsultaCommand(pacienteId, medicoId, data, horario);
        var contatoMock = new Mock<Consulta>(pacienteId, medicoId, data, horario);

        _consultaRepositoryMock.Setup(r => r.Get(medicoId, data))
            .ReturnsAsync(new List<Consulta>() { contatoMock.Object });

        _consultaRepositoryMock.Setup(r => r.Create(contatoMock.Object))
        .Returns(Task.CompletedTask);

        await _agendarConsultaService.Handle(command);

        _consultaRepositoryMock.Verify(
             r => r.Create(It.Is<Consulta>(consulta=>
                 consulta.PacienteId == command.PacienteId && 
                 consulta.MedicoId == command.MedicoId)), Times.Once);


    }
}
