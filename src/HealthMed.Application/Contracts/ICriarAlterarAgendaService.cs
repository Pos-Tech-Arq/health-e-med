using HealthMed.Application.Commands;

namespace HealthMed.Application.Contracts;

public interface ICriarAlterarAgendaService
{
    Task Handle(Guid medicoId, CadastroAgendaCommand command);
    Task Handle(Guid agendaId, AtualizarAgendaCommand command);
}