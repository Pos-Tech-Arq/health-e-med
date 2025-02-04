namespace HealthMed.Domain.Commands;

public class AlterarConsultaCommand
{
    public Guid ConsultaId { get; private set; }
    public string Status { get; private set; }
}