namespace HealthMed.Domain.Entities;

public class Consulta : Entidade
{
    public Guid PacienteId { get; set; }
    public Guid MedicoId { get; set; }
    public DateTime Data { get; set; }
    public TimeSpan Horario { get; set; }
    public string Status { get; set; }
}