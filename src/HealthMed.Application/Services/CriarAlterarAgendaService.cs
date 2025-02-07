using HealthMed.Application.Commands;
using HealthMed.Application.Contracts;
using HealthMed.Domain.Entities;

namespace HealthMed.Application.Services;

public class CriarAlterarAgendaService(IAgendaRepository agendaRepository) : ICriarAlterarAgendaService
{
    public async Task Handle(Guid medicoId, CadastroAgendaCommand command)
    {
        var agenda = new Agenda(medicoId, command.Data, command.HorarioInicio, command.HorarioFim, command.ValorConsulta);
        await agendaRepository.Create(agenda);
    }

    public async Task Handle(Guid agendaId, AtualizarAgendaCommand command)
    {
        var agenda = await agendaRepository.Get(agendaId);
        agenda.Atualizar(command.Data, command.HorarioInicio, command.HorarioFim, command.ValorConsulta);
        await agendaRepository.Update(agenda);
    }
}