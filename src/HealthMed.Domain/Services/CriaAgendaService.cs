using HealthMed.Domain.Commands;
using HealthMed.Domain.Contracts;

namespace HealthMed.Domain.Services;

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