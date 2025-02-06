using HealthMed.Application.Commands;
using HealthMed.Application.Contracts;

namespace HealthMed.Application.Services;

public class CriaAgendaService : ICriaAgendaService
{
    private readonly IAgendaRepository _agendaRepository;
    
    public CriaAgendaService(IAgendaRepository agendaRepository)
    {
        _agendaRepository = agendaRepository;
    }

    public Task Handle(CadastroAgendaCommand command)
    {
        throw new NotImplementedException();
    }
}