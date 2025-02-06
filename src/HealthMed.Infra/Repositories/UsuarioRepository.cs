using System.Security.Claims;
using HealthMed.Application.Contracts;
using HealthMed.Core.Enums;
using HealthMed.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HealthMed.Infra.Repositories;

public class UsuarioRepository(UserManager<Usuario> userManager) : IUsuarioRepository
{
    public async Task AddAsync(Usuario usuario)
    {
        var claim = new Claim(nameof(TipoUsuario), usuario.Tipo.ToString(), ClaimValueTypes.String);
        
        await userManager.CreateAsync(usuario);
        await userManager.AddClaimAsync(usuario, claim);
    }
}