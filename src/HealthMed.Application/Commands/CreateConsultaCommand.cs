namespace HealthMed.Application.Commands;

public class CreateConsultaCommand
{
    public Guid PacientId { get; set; }
    public Guid MedicoId { get; set; }
    public DateTime Data { get; set; }
    public TimeSpan Horario { get; set; }
}