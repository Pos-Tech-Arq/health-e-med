global using HealthMed.Api.Controllers;
global using HealthMed.Application.Commands;
global using HealthMed.Application.Contracts;

namespace HealthMed.UnitTests.Controllers;

public class ConsultasControllerCreateConsultaTest
{
    protected ConsultasController _consultasController;
    protected Mock<ICreateConsultaService> _createConsultaService;
    protected Mock<IAgendaRepository> _agendaRepository;

    public ConsultasControllerCreateConsultaTest()
    {
        _createConsultaService = new Mock<ICreateConsultaService>();
        _agendaRepository = new Mock<IAgendaRepository>();
        _consultasController = new ConsultasController(_createConsultaService.Object);
    }

    [Fact]
    public async Task CreateConsulta_ShouldReturnOk_WhenCommandIsValid()
    {
        var command = new CreateConsultaCommand
        {
            PacientId = Guid.NewGuid(),
            MedicoId = Guid.NewGuid(),
            Data = DateTime.UtcNow.Date, 
            Horario = new TimeSpan(14, 30, 0) 
        };

        _createConsultaService
            .Setup(service => service.CreateConsulta(It.IsAny<CreateConsultaCommand>()))
            .Returns(Task.CompletedTask); 

        // Act
        var result = await _consultasController.Create(command);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task CreateConsulta_ShouldThrowArgumentException_WhenConsultaAlreadyExists()
    {
        var command = new CreateConsultaCommand
        {
            PacientId = Guid.NewGuid(),
            MedicoId = Guid.NewGuid(),
            Data = DateTime.UtcNow.Date,
            Horario = new TimeSpan(14, 30, 0)
        };

        _createConsultaService
            .Setup(service => service.CreateConsulta(It.IsAny<CreateConsultaCommand>()))
            .Callback<CreateConsultaCommand>(cmd =>
            {
                if (cmd.MedicoId == command.MedicoId && cmd.Data == command.Data && cmd.Horario == command.Horario)
                {
                    throw new ArgumentException("Já existe consulta nesse horario com esse medico.");
                }
            });

        
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _consultasController.Create(command));

        Assert.Equal("Já existe consulta nesse horario com esse medico.", exception.Message);
    }

    [Fact]
    public async Task CreateConsulta_ShouldThrowArgumentException_WhenHorarioIsOutsideAgenda()
    {
        var command = new CreateConsultaCommand
        {
            PacientId = Guid.NewGuid(),
            MedicoId = Guid.NewGuid(),
            Data = DateTime.UtcNow.Date,
            Horario = new TimeSpan(19, 0, 0)
        };

        var agenda = new Agenda(
            command.MedicoId,
            command.Data,
            new TimeSpan(8, 0, 0),  
            new TimeSpan(17, 0, 0), 
            150.00m); 

        _agendaRepository.Setup(repo => repo.Get(It.IsAny <Guid>(), It.IsAny <DateTime>())).ReturnsAsync(agenda);

        _createConsultaService
           .Setup(service => service.CreateConsulta(It.IsAny<CreateConsultaCommand>()))
           .Callback<CreateConsultaCommand>(cmd =>
           {
               if (command.Horario < agenda.HorarioInicio || command.Horario > agenda.HorarioFim)
               {
                   throw new ArgumentException(
                    $"O horario da consulta deve ser entre {agenda.HorarioInicio} e {agenda.HorarioFim}");
               }
           });


        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _consultasController.Create(command));

        Assert.Equal($"O horario da consulta deve ser entre {agenda.HorarioInicio} e {agenda.HorarioFim}", exception.Message);
    }
}