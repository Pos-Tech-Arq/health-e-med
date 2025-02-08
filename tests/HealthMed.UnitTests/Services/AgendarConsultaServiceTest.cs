using HealthMed.Application.Services;
using MediatR;
using Moq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace HealthMed.UnitTests.Services;

public class AgendarConsultaServiceTest
{

    private readonly Mock<IConsultaRepository> _consultaRepositoryMock;
    private readonly AgendarConsultaService _service;

    public AgendarConsultaServiceTest()
    {
        _consultaRepositoryMock = new Mock<IConsultaRepository>();
        _service = new AgendarConsultaService(_consultaRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateConsulta_WhenValidCommandIsProvided()
    {
        // Arrange
        var command = 
            new AgendarConsultaCommand(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.Date, new TimeSpan(14, 0, 0));

         _consultaRepositoryMock
                .Setup(repo => repo.Create(It.IsAny<Consulta>()))
                .Returns(Task.CompletedTask);

        // Act
        await _service.Handle(command);

        // Assert
        _consultaRepositoryMock.Verify(repo => repo.Create(It.IsAny<Consulta>()), Times.Once);
    }
}
