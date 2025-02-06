using HealthMed.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace HealthMed.Domain.Entities;

public class Usuario : IdentityUser<Guid>
{
    protected Usuario()
    {
    }

    public Usuario(string nome, string email, string senha, TipoUsuario tipo, string cpf, string? crm,
        string? especialidade)
    {
        UserName = nome;
        Email = email;
        PasswordHash = senha;
        Tipo = tipo;
        Cpf = cpf;
        Crm = crm;
        Especialidade = especialidade;
    }

    public TipoUsuario Tipo { get; private set; }
    public string Cpf { get; private set; }
    public string? Crm { get; private set; }
    public string? Especialidade { get; private set; }

    public void AtualizarCrm(string crm)
    {
        if (Tipo
            != TipoUsuario.Medico)
        {
            throw new InvalidOperationException("Apenas médicos podem ter CRM.");
        }

        Crm = crm;
    }

    public void AtualizarEspecialidade(string especialidade)
    {
        if (Tipo
            != TipoUsuario.Medico)
        {
            throw new InvalidOperationException("Apenas médicos podem ter CRM.");
        }

        Especialidade = especialidade;
    }
}