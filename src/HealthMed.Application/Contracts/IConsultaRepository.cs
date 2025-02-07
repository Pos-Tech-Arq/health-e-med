using HealthMed.Domain.Entities;

namespace HealthMed.Application.Contracts;

public interface IConsultaRepository
{
    Task Create(Consulta consulta);
    Task<List<Consulta>> Get(Guid medicoId, DateTime? data = null);
    Task<bool> ValidaSeExisteConsulta(Guid medicoId, DateTime data, TimeSpan horario);
}