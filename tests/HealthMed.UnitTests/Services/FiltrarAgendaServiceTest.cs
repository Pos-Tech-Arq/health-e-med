using HealthMed.Application.Services;

namespace HealthMed.UnitTests.Services;

public class FiltrarAgendaServiceTest
{
    private readonly Mock<IConsultaRepository> _consultaRepositoryMock;
    private readonly Mock<IAgendaRepository> _agendaRepositoryMock;
    private readonly CreateConsultaService _service;

    public FiltrarAgendaServiceTest()
    {
        _consultaRepositoryMock = new Mock<IConsultaRepository>();
        _agendaRepositoryMock = new Mock<IAgendaRepository>();
        _service = new CreateConsultaService(_consultaRepositoryMock.Object, _agendaRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateConsulta_ShouldThrowExceptionIfConsultaAlreadyExistsAtTheSameTime()
    {
        // Arrange
        var command = new CreateConsultaCommand
        {
            MedicoId = Guid.NewGuid(),
            Data = DateTime.Today,
            Horario = TimeSpan.FromHours(9),
            PacientId = Guid.NewGuid()
        };

        _consultaRepositoryMock
            .Setup(x => x.ValidaSeExisteConsulta(command.MedicoId, command.Data, command.Horario))
            .ReturnsAsync(true); 

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateConsulta(command));
    }

    [Fact]
    public async Task CreateConsulta_ShouldThrowExceptionIfTimeIsOutsideScheduleRange()
    {
        // Arrange
        var command = new CreateConsultaCommand
        {
            MedicoId = Guid.NewGuid(),
            Data = DateTime.Today,
            Horario = TimeSpan.FromHours(20), 
            PacientId = Guid.NewGuid()
        };

        var agenda = new Agenda(
            command.MedicoId,
            command.Data,
            TimeSpan.FromHours(8),
            TimeSpan.FromHours(17),1
        );

        _agendaRepositoryMock
            .Setup(x => x.Get(command.MedicoId, command.Data))
            .ReturnsAsync(agenda);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateConsulta(command));
    }

    [Fact]
    public async Task CreateConsulta_ShouldCreateConsultaSuccessfully()
    {
        // Arrange
        var command = new CreateConsultaCommand
        {
            MedicoId = Guid.NewGuid(),
            Data = DateTime.Today,
            Horario = TimeSpan.FromHours(9),
            PacientId = Guid.NewGuid()
        };

        var agenda = new Agenda(command.MedicoId,
            command.Data, TimeSpan.FromHours(8), TimeSpan.FromHours(17), 1);

        _agendaRepositoryMock
            .Setup(x => x.Get(command.MedicoId, command.Data))
            .ReturnsAsync(agenda);

        _consultaRepositoryMock
            .Setup(x => x.ValidaSeExisteConsulta(command.MedicoId, command.Data, command.Horario))
            .ReturnsAsync(false);

        // Act
        await _service.CreateConsulta(command);

        // Assert
        _consultaRepositoryMock.Verify(x => x.Create(It.Is<Consulta>(c =>
            c.PacienteId == command.PacientId &&
            c.MedicoId == command.MedicoId &&
            c.Data == command.Data &&
            c.Horario == command.Horario)), Times.Once);
    }
}
