namespace HealthMed.Domain.Entities;

public class Consulta : Entidade
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

    public Guid PacienteId { get; set; }
    public Guid MedicoId { get; set; }
    public DateTime Data { get; set; }
    public TimeSpan Horario { get; set; }
    public string Status { get; set; }
}