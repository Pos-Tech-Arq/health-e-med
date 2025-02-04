using HealthMed.Domain.Entities;

namespace HealthMed.Domain.Contracts;

public interface IAgendaRepository
{
    Task Create(Agenda agenda);
    
}