using HealthMed.Application.Commands;
using HealthMed.Application.Contracts;
using HealthMed.Domain.Entities;

namespace HealthMed.Application.Services;

public class CreateConsultaService(IConsultaRepository consultaRepository, IAgendaRepository agendaRepository)
    : ICreateConsultaService
{
    public async Task CreateConsulta(CreateConsultaCommand command)
    {
        var existeConsulta =
            await consultaRepository.ValidaSeExisteConsulta(command.MedicoId, command.Data, command.Horario);

        if (existeConsulta)
        {
            throw new ArgumentException("Já existe consulta nesse horario com esse medico.");
        }

        var agenda = await agendaRepository.Get(command.MedicoId, command.Data);

        if (command.Horario < agenda.HorarioInicio || command.Horario > agenda.HorarioFim)
        {
            throw new ArgumentException(
                $"O horario da consulta deve ser entre {agenda.HorarioInicio} e {agenda.HorarioFim}");
        }

        var consulta = new Consulta(command.PacientId, command.MedicoId, command.Data, command.Horario);

        await consultaRepository.Create(consulta);
    }
}