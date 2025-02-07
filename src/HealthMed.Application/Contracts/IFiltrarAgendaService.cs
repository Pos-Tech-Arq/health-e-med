using HealthMed.Application.Responses;

namespace HealthMed.Application.Contracts;

public interface IFiltrarAgendaService
{
    Task<List<AgendaDisponivelResponse>> FiltrarAgenda(Guid idMedico, DateTime? data = null);
}