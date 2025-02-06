using HealthMed.Application.Commands;

namespace HealthMed.Application.Contracts;

public interface ICriaAgendaService
{
    Task Handle(CadastroAgendaCommand command);
}