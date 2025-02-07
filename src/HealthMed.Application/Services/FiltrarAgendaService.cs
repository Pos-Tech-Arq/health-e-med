using HealthMed.Application.Contracts;
using HealthMed.Application.Responses;

namespace HealthMed.Application.Services;

public class FiltrarAgendaService(IAgendaRepository agendaRepository, IConsultaRepository consultaRepository)
    : IFiltrarAgendaService
{
    public async Task<List<AgendaDisponivelResponse>> FiltrarAgenda(Guid idMedico, DateTime? data = null)
    {
        var agendaCollection = await agendaRepository.Get(idMedico, data);
        var consultasExistentes = await consultaRepository.Get(idMedico, data);

        return agendaCollection
            .GroupBy(a => a.Data)
            .Select(agendaAgrupadaPorDia =>
            {
                var agendaDisponivelResponse = new AgendaDisponivelResponse(
                    agendaAgrupadaPorDia.Key.ToString("MM/dd/yyyy"));

                foreach (var agenda in agendaAgrupadaPorDia)
                {
                    var horarios = GerarHorarios(agenda.HorarioInicio, agenda.HorarioFim)
                        .Where(horario => !consultasExistentes.Any(c =>
                            c.Data == agenda.Data && c.Horario == horario));

                    agendaDisponivelResponse.Set(agenda.Valor);
                    foreach (var horario in horarios)
                    {
                        agendaDisponivelResponse.Add(horario);
                    }
                }

                return agendaDisponivelResponse;
            })
            .ToList();
    }

    private static IEnumerable<TimeSpan> GerarHorarios(TimeSpan inicio, TimeSpan fim)
    {
        for (var horario = inicio; horario < fim; horario = horario.Add(TimeSpan.FromHours(1)))
        {
            yield return horario;
        }
    }
}