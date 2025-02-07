namespace HealthMed.Application.Responses;

public class AgendaDisponivelResponse
{
    public AgendaDisponivelResponse(string data)
    {
        Data = data;
        HorariosDisponiveis = new List<TimeSpan>();
    }
    public void Add(TimeSpan horarioDisponivel)
    {
        HorariosDisponiveis.Add(horarioDisponivel);
    } 
    
    public void Set(decimal valorConsulta)
    {
        ValorConsulta = valorConsulta;
    }
    
    public string Data { get; set; }
    public decimal ValorConsulta { get; set; }
    public List<TimeSpan> HorariosDisponiveis { get; set; }
}
