using HealthMed.Application.Commands;

namespace HealthMed.Application.Contracts;

public interface IAgendarConsultaService
{
    Task Handle(AgendarConsultaCommand command);
}