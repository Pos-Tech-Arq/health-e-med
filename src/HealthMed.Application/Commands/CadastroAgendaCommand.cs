namespace HealthMed.Application.Commands;

public class CadastroAgendaCommand
{
    public CadastroAgendaCommand(DateTime data, TimeSpan horarioInicio, TimeSpan horarioFim, decimal valorConsulta)
    {
        Data = data;
        HorarioInicio = horarioInicio;
        HorarioFim = horarioFim;
        ValorConsulta = valorConsulta;
    }

    public DateTime Data { get; set; }
    public TimeSpan HorarioInicio { get; set; }
    public TimeSpan HorarioFim { get; set; }
    public decimal ValorConsulta { get; set; }
}