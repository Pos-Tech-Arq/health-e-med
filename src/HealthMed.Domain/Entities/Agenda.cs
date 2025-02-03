namespace HealthMed.Domain.Entities;

public class Agenda : Entidade
{
    public Agenda(DateTime data, TimeSpan horarioInicio, TimeSpan horarioFim, decimal valor)
    {
        Data = data;
        HorarioInicio = horarioInicio;
        HorarioFim = horarioFim;
        Valor = valor;
    }

    public DateTime Data { get; private set; }
    public TimeSpan HorarioInicio { get; private set; }
    public TimeSpan HorarioFim { get; private set; }
    public decimal Valor { get; private set; }
}