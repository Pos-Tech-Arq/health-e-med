using HealthMed.Core.Enums;

namespace HealthMed.Application.Commands;

public class RegistrarUsuarioCommand
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public TipoUsuario Tipo { get; set; } 
    public string Cpf { get; set; }
    public string? Crm { get; set; }
    public string? Especialidade { get; set; }
}