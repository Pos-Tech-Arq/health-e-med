using HealthMed.Application.Services;

namespace HealthMed.UnitTests.Services;

public class CriarAlterarAgendaServiceTest
{
    private readonly Mock<IAgendaRepository> _agendaRepositoryMock;
    private readonly CriarAlterarAgendaService _service;

    public CriarAlterarAgendaServiceTest()
    {
        _agendaRepositoryMock = new Mock<IAgendaRepository>();
        _service = new CriarAlterarAgendaService(_agendaRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_CadastroAgenda_Should_Create_Agenda()
    {
        // Arrange
        var medicoId = Guid.NewGuid();
        var command = new CadastroAgendaCommand(DateTime.Today, TimeSpan.FromHours(8), TimeSpan.FromHours(12), 150.00m);

        // Act
        await _service.Handle(medicoId, command);

        // Assert
        _agendaRepositoryMock.Verify(repo => repo.Create(It.Is<Agenda>(
            a => a.MedicoId == medicoId &&
                 a.Data == command.Data &&
                 a.HorarioInicio == command.HorarioInicio &&
                 a.HorarioFim == command.HorarioFim &&
                 a.Valor == command.ValorConsulta
        )), Times.Once);
    }

    [Fact]
    public async Task Handle_AtualizarAgenda_Should_Update_Agenda()
    {
        // Arrange
        var agendaId = Guid.NewGuid();
        var command = new AtualizarAgendaCommand(DateTime.Today, TimeSpan.FromHours(9), TimeSpan.FromHours(17), 200.00m);

        var agenda = new Agenda(Guid.NewGuid(), DateTime.Today, TimeSpan.FromHours(8), TimeSpan.FromHours(12), 150.00m);

        _agendaRepositoryMock.Setup(repo => repo.Get(agendaId))
            .ReturnsAsync(agenda);

        // Act
        await _service.Handle(agendaId, command);

        // Assert
        Assert.Equal(command.Data, agenda.Data);
        Assert.Equal(command.HorarioInicio, agenda.HorarioInicio);
        Assert.Equal(command.HorarioFim, agenda.HorarioFim);
        Assert.Equal(command.ValorConsulta, agenda.Valor);

        _agendaRepositoryMock.Verify(repo => repo.Update(agenda), Times.Once);
    }

    [Fact]
    public async Task Handle_AtualizarAgenda_Should_Throw_Exception_When_Agenda_Not_Found()
    {
        // Arrange
        var agendaId = Guid.NewGuid();
        var command = new AtualizarAgendaCommand(DateTime.Today, TimeSpan.FromHours(9), TimeSpan.FromHours(17), 200.00m);

        _agendaRepositoryMock.Setup(repo => repo.Get(agendaId))
            .ReturnsAsync((Agenda)null);

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => _service.Handle(agendaId, command));

        _agendaRepositoryMock.Verify(repo => repo.Update(It.IsAny<Agenda>()), Times.Never);
    }
}
