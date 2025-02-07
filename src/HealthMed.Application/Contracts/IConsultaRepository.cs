using HealthMed.Domain.Entities;

namespace HealthMed.Application.Contracts;

public interface IConsultaRepository
{
    Task Create(Consulta consulta);
    Task<bool> ValidaSeExisteConsulta(Guid medicoId, DateTime data, TimeSpan horario);
}