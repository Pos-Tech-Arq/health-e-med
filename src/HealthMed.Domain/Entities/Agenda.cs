using HealthMed.Core.Entities;

namespace HealthMed.Domain.Entities;

public class Agenda : Entidade
{
    public Agenda(Guid medicoId, DateTime data, TimeSpan horarioInicio, TimeSpan horarioFim, decimal valor)
    {
        MedicoId = medicoId;
        Data = data;
        HorarioInicio = horarioInicio;
        HorarioFim = horarioFim;
        Valor = valor;
    }

    public Guid MedicoId { get; set; }
    public DateTime Data { get; private set; }
    public TimeSpan HorarioInicio { get; private set; }
    public TimeSpan HorarioFim { get; private set; }
    public decimal Valor { get; private set; }
}