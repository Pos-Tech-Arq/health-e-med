using System.ComponentModel;

namespace HealthMed.Core.Enums;

public enum StatusConsulta
{
    [Description("Pendente")] Pendente = 1,
    [Description("Confirmado")]Confirmado = 2,
    [Description("Recusado")]Recusado = 3,
    [Description("Cancelado")]Cancelado = 4
}