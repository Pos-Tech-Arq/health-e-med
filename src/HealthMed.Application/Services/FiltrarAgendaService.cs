using HealthMed.Application.Contracts;
using HealthMed.Application.Responses;

namespace HealthMed.Application.Services;

public class FiltrarAgendaService(IAgendaRepository agendaRepository) : IFiltrarAgendaService
{
    public async Task<List<AgendaDisponivelResponse>> FiltrarAgenda(Guid idMedico, DateTime? data = null)
    {
        var agendaCollection = await agendaRepository.Get(idMedico, data);
        var agendaDisponivelCollectionResponse = new List<AgendaDisponivelResponse>();
        foreach (var agendaAgrupadaPorDia in agendaCollection.GroupBy(a => a.Data))
        {
            var agendaDisponivelResponse = new AgendaDisponivelResponse(agendaAgrupadaPorDia.Key.ToString("MM/dd/yyyy"));
            foreach (var agenda in agendaAgrupadaPorDia)
            {
                agendaDisponivelResponse.Set(agenda.Valor);
                
                var horarioAtual = agenda.HorarioInicio;
                while (horarioAtual < agenda.HorarioFim)
                {
                    agendaDisponivelResponse.Add(horarioAtual);
                    horarioAtual = horarioAtual.Add(new TimeSpan(1, 0, 0));
                }
            }
            
            agendaDisponivelCollectionResponse.Add(agendaDisponivelResponse);
        }

        return agendaDisponivelCollectionResponse;
    }
}