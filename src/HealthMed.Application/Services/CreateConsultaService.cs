using HealthMed.Application.Commands;
using HealthMed.Application.Contracts;
using HealthMed.Domain.Entities;

namespace HealthMed.Application.Services;

public class CreateConsultaService : ICreateConsultaService
{
    private readonly IConsultaRepository _consultaRepository;
    private readonly IAgendaRepository _agendaRepository;

    public CreateConsultaService(IConsultaRepository consultaRepository)
    {
        _consultaRepository = consultaRepository;
    }

    public async Task CreateConsulta(CreateConsultaCommand command)
    {
        var existeConsulta =
            await _consultaRepository.ValidaSeExisteConsulta(command.MedicoId, command.Data, command.Horario);

        if (existeConsulta)
        {
            throw new ArgumentException("Já existe consulta com o medico");
        }

        var agenda = await _agendaRepository.Get(command.MedicoId, command.Data);

        if (command.Horario < agenda.HorarioInicio || command.Horario > agenda.HorarioFim)
        {
            throw new ArgumentException(
                $"O horario da consulta deve ser entre {agenda.HorarioInicio} e {agenda.HorarioFim}");
        }

        var consulta = new Consulta(command.PacientId, command.MedicoId, command.Data, command.Horario);

        await _consultaRepository.Create(consulta);
    }
}