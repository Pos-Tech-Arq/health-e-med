using HealthMed.Domain.Entities;

namespace HealthMed.Application.Contracts;

public interface IConsultaRepository
{
    Task Create(Consulta consulta);
}