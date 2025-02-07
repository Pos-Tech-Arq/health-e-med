using HealthMed.Core.Enums;

namespace HealthMed.Api.Responses;

public class UsuarioReponse
{
    public Guid Id { get; set; }
    public TipoUsuario Tipo { get; private set; }
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public string? Crm { get; private set; }
    public string? Especialidade { get; private set; }

    public UsuarioReponse(Guid id, TipoUsuario tipo, string nome, string cpf, string? crm, string? especialidade)
    {
        Id = id;
        Tipo = tipo;
        Nome = nome;
        Cpf = cpf;
        Crm = crm;
        Especialidade = especialidade;
    }
}