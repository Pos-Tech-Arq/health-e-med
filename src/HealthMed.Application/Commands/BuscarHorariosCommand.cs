namespace HealthMed.Application.Commands;

public class BuscarHorariosCommand
{
    public BuscarHorariosCommand(DateTime data, Guid? medicoId, decimal? valorConsulta)
    {
        Data = data;
        MedicoId = medicoId;
        ValorConsulta = valorConsulta;
    }

    public DateTime Data { get; set; }
    public Guid? MedicoId { get; set; }
    public decimal? ValorConsulta { get; set; }
}