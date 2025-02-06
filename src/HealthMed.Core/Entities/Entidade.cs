namespace HealthMed.Core.Entities;

public abstract class Entidade
{
    public Guid Id { get; protected set; }

    protected Entidade()
    {
    }
}