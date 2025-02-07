using HealthMed.Domain.Entities;

namespace HealthMed.Application.Contracts;

public interface IAgendaRepository
{
    Task<Agenda> Get(Guid id);
    Task<List<Agenda>> Get(Guid medicoId, DateTime? data = null);
    Task Create(Agenda agenda);
    Task<Agenda> Get(Guid medicoId, DateTime data);
    Task Update(Agenda agenda);
}