namespace HealthMed.Domain.Commands;

public class AgendarConsultaCommand
{
    public AgendarConsultaCommand(Guid pacienteId, Guid medicoId, DateTime data, TimeSpan horario)
    {
        PacienteId = pacienteId;
        MedicoId = medicoId;
        Data = data;
        Horario = horario;
    }

    public Guid PacienteId { get; private set; }
    public Guid MedicoId { get; private set; }
    public DateTime Data { get; private set; }
    public TimeSpan Horario { get; private set; }
}