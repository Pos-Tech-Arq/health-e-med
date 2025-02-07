using System.ComponentModel.DataAnnotations.Schema;
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
    
    public void Atualizar(DateTime data, TimeSpan horarioInicio, TimeSpan horarioFim, decimal valor)
    {
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
    
    [ForeignKey("MedicoId")] public virtual Usuario Medico { get; set; }
}