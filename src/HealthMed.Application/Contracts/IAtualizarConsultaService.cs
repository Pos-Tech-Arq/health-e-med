using HealthMed.Core.Enums;

namespace HealthMed.Application.Contracts;

public interface IAtualizarConsultaService
{
    Task Handle(Guid consultaId, StatusConsulta? status = null);
}