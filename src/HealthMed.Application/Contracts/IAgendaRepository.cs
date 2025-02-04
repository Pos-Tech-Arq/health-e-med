using HealthMed.Domain.Entities;

namespace HealthMed.Application.Contracts;

public interface IAgendaRepository
{
    Task Create(Agenda agenda);
    
}