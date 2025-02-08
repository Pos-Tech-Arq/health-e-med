using HealthMed.Application.Services;

namespace HealthMed.UnitTests.Services;

public class CreateConsultaServiceTest
{
    private readonly Mock<IConsultaRepository> _consultaRepositoryMock;
    private readonly Mock<IAgendaRepository> _agendaRepositoryMock;
    private readonly CreateConsultaService _service;

    public CreateConsultaServiceTest()
    {
        _consultaRepositoryMock = new Mock<IConsultaRepository>();
        _agendaRepositoryMock = new Mock<IAgendaRepository>();
        _service = new CreateConsultaService(_consultaRepositoryMock.Object, _agendaRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateConsulta_Should_Create_Consulta_When_Valid()
    {
        // Arrange
        var command = new CreateConsultaCommand()
        {

            PacientId = Guid.NewGuid(),
            MedicoId = Guid.NewGuid(),
            Data = DateTime.Today,
            Horario = TimeSpan.FromHours(10)
        };

        _consultaRepositoryMock.Setup(repo => repo.ValidaSeExisteConsulta(command.MedicoId, command.Data, command.Horario))
            .ReturnsAsync(false);

        _agendaRepositoryMock.Setup(repo => repo.Get(command.MedicoId, command.Data))
            .ReturnsAsync(new Agenda(command.MedicoId,command.Data,TimeSpan.FromHours(8), TimeSpan.FromHours(18),1));

        // Act
        await _service.CreateConsulta(command);

        // Assert
        _consultaRepositoryMock.Verify(repo => repo.Create(It.IsAny<Consulta>()), Times.Once);
    }

    [Fact]
    public async Task CreateConsulta_Should_Throw_Exception_When_Consulta_Exists()
    {
        // Arrange
        var command = new CreateConsultaCommand()
        {

            PacientId = Guid.NewGuid(),
            MedicoId = Guid.NewGuid(),
            Data = DateTime.Today,
            Horario = TimeSpan.FromHours(10)
        };

        _consultaRepositoryMock.Setup(repo => repo.ValidaSeExisteConsulta(command.MedicoId, command.Data, command.Horario))
            .ReturnsAsync(true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateConsulta(command));
        Assert.Equal("Já existe consulta nesse horario com esse medico.", exception.Message);

        _consultaRepositoryMock.Verify(repo => repo.Create(It.IsAny<Consulta>()), Times.Never);
    }

    [Fact]
    public async Task CreateConsulta_Should_Throw_Exception_When_Horario_Is_Out_Of_Range()
    {
        // Arrange
        var command = new CreateConsultaCommand()
        {

            PacientId = Guid.NewGuid(),
            MedicoId = Guid.NewGuid(),
            Data = DateTime.Today,
            Horario = TimeSpan.FromHours(20)
        };

        _consultaRepositoryMock.Setup(repo => repo.ValidaSeExisteConsulta(command.MedicoId, command.Data, command.Horario))
            .ReturnsAsync(false);

        _agendaRepositoryMock.Setup(repo => repo.Get(command.MedicoId, command.Data))
            .ReturnsAsync(new Agenda(command.MedicoId, command.Data, TimeSpan.FromHours(8), TimeSpan.FromHours(18), 1));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateConsulta(command));
        Assert.Contains("O horario da consulta deve ser entre", exception.Message);

        _consultaRepositoryMock.Verify(repo => repo.Create(It.IsAny<Consulta>()), Times.Never);
    }
}
