using System.ComponentModel;

namespace HealthMed.Core.Enums;

public enum TipoUsuario
{
    [Description("Medico ")] Medico = 1,
    [Description("Paciente")] Paciente = 2
}