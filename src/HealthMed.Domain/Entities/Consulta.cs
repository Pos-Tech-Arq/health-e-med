using System.ComponentModel.DataAnnotations.Schema;
using HealthMed.Core.Data;
using HealthMed.Core.Entities;
using HealthMed.Core.Enums;

namespace HealthMed.Domain.Entities;

public class Consulta : Entidade, IAggregateRoot
{
    public Consulta(Guid pacienteId, Guid medicoId, DateTime data, TimeSpan horario)
    {
        PacienteId = pacienteId;
        MedicoId = medicoId;
        Data = data;
        Horario = horario;
        Status = StatusConsulta.Pendente;
    }

    public Guid PacienteId { get; private set; }
    public Guid MedicoId { get; private set; }
    public DateTime Data { get; private set; }
    public TimeSpan Horario { get; private set; }
    public StatusConsulta Status { get; private set; }
    
    public void Atualizar(StatusConsulta status)
    {
        Status = status;
    }

    [ForeignKey("PacienteId")] public virtual Usuario Paciente { get; set; } 
    [ForeignKey("MedicoId")] public virtual Usuario Medico { get; set; } 
}