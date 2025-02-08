using HealthMed.Application.Contracts;
using HealthMed.Core.Enums;

namespace HealthMed.Application.Services;

public class AtualizarConsultaService(IConsultaRepository consultaRepository) : IAtualizarConsultaService
{
    public async Task Handle(Guid consultaId, StatusConsulta? status = null)
    {
        var consulta = await consultaRepository.GetById(consultaId);
        if (status == null)
        {
            status = StatusConsulta.Cancelado;
        }
        
        consulta.Atualizar(status.Value);
        await consultaRepository.Update(consultaId, consulta);
    }
}