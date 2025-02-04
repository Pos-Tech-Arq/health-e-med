using HealthMed.Domain.Commands;

namespace HealthMed.Domain.Contracts;

public interface ICriaAgendaService
{
    Task Handle(CadastroAgendaCommand command);
}