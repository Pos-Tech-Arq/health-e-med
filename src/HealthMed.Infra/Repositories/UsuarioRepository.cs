using System.Security.Claims;
using HealthMed.Application.Contracts;
using HealthMed.Core.Enums;
using HealthMed.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Infra.Repositories;

public class UsuarioRepository(UserManager<Usuario> userManager) : IUsuarioRepository
{
    public async Task AddAsync(Usuario usuario, string senha)
    {
        var result = await userManager.CreateAsync(usuario, senha);

        if (!result.Succeeded)
        {
            var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Erro ao criar usuário, {errorMessages}");
        }

        var claim = new Claim(nameof(TipoUsuario), usuario.Tipo.ToString(), ClaimValueTypes.String);
        await userManager.AddClaimAsync(usuario, claim);
    }

    public Task<Usuario> GetAsync(string? cpf = null, string? crm = null)
    {
        var query = userManager.Users.AsQueryable();

        if (!string.IsNullOrEmpty(cpf))
        {
            query = query.Where(c => c.Cpf == cpf);
        }

        if (!string.IsNullOrEmpty(crm))
        {
            query = query.Where(c => c.Crm == crm);
        }

        return query.FirstAsync();
    }

    public Task<IList<Claim>> GetClaimsAsync(Usuario usuario)
    {
        return userManager.GetClaimsAsync(usuario);
    }

    public async Task<IEnumerable<Usuario>> GetUsuarios(TipoUsuario? tipoUsuario, string? especialidade)
    {
        var query = userManager.Users.AsQueryable();

        if (tipoUsuario != null)
        {
            query = query.Where(c => c.Tipo == tipoUsuario);
        }

        if (!string.IsNullOrEmpty(especialidade))
        {
            query = query.Where(c => c.Especialidade == especialidade);
        }

        return await query.ToListAsync();
    }
}