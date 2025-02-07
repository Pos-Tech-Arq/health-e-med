using HealthMed.Application.Commands;

namespace HealthMed.Application.Contracts;

public interface ICreateConsultaService
{
    Task CreateConsulta(CreateConsultaCommand command);
}