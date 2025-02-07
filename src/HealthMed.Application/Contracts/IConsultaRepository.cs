using HealthMed.Core.Enums;
using HealthMed.Domain.Entities;

namespace HealthMed.Application.Contracts;

public interface IConsultaRepository
{
    Task<Consulta> GetById(Guid consultaId);
    Task Create(Consulta consulta);
    Task Update(Guid consultaId, Consulta consulta);
    Task<List<Consulta>> Get(Guid medicoId, DateTime? data = null);
    Task<bool> ValidaSeExisteConsulta(Guid medicoId, DateTime data, TimeSpan horario);
}