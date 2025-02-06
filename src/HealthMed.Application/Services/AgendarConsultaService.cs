using HealthMed.Application.Commands;
using HealthMed.Application.Contracts;
using HealthMed.Domain.Entities;

namespace HealthMed.Application.Services;

public class AgendarConsultaService : IAgendarConsultaService
{
    private IConsultaRepository _consultaRepository;

    public AgendarConsultaService(IConsultaRepository consultaRepository)
    {
        _consultaRepository = consultaRepository;
    }

    public async Task Handle(AgendarConsultaCommand command)
    {
        var consulta = new Consulta(command.MedicoId, command.PacienteId, command.Data, command.Horario);
        await _consultaRepository.Create(consulta);
    }
}