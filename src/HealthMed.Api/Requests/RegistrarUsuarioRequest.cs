namespace HealthMed.Api.Requests;

public class RegistrarUsuarioRequest
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Tipo { get; set; } 
    public string Cpf { get; set; }
    public string Crm { get; set; }
    public string Especialidade { get; set; }
}
