namespace HealthMed.Application.Commands;

public class CadastroAgendaCommand
{
    public CadastroAgendaCommand(Guid medicoId, DateTime data, TimeSpan horarioInicio, TimeSpan horarioFim, decimal valorConsulta)
    {
        MedicoId = medicoId;
        Data = data;
        HorarioInicio = horarioInicio;
        HorarioFim = horarioFim;
        ValorConsulta = valorConsulta;
    }

    public Guid MedicoId { get; set; }
    public DateTime Data { get; set; }
    public TimeSpan HorarioInicio { get; set; }
    public TimeSpan HorarioFim { get; set; }
    public decimal ValorConsulta { get; set; }
}