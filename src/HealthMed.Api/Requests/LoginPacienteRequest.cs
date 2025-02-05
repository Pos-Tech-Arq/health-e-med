namespace HealthMed.Api.Requests;

public class LoginPacienteRequest
{
    public string Email { get; set; }
    public string Cpf { get; set; }
    public string Senha { get; set; }
}
