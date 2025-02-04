using HealthMed.Domain.Contracts;

namespace HealthMed.Domain.Entities;

public class Consulta : Entidade, IAggregateRoot
{
    public Consulta(Guid pacienteId, Guid medicoId, DateTime data, TimeSpan horario)
    {
        PacienteId = pacienteId;
        MedicoId = medicoId;
        Data = data;
        Horario = horario;
        Status = "Pendente";
    }

    public void ConfirmarConsulta()
    {
        Status = "Confirmado";
    }

    public void CancelarConsulta()
    {
        Status = "Cancelado";
    }

    public void RecusarConsulta()
    {
        Status = "Recusado";
    }

    public Guid PacienteId { get; private set; }
    public Guid MedicoId { get; private set; }
    public DateTime Data { get; private set; }
    public TimeSpan Horario { get; private set; }
    public string Status { get; private set; }
}