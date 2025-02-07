using HealthMed.Domain.Entities;

namespace HealthMed.Application.Contracts;

public interface IAgendaRepository
{
    Task<Agenda> Get(Guid id);
    Task Create(Agenda agenda);
    Task Update(Agenda agenda);
}