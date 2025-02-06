﻿using System.Security.Claims;
using HealthMed.Application.Contracts;
using HealthMed.Core.Enums;
using HealthMed.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Infra.Repositories;

public class UsuarioRepository(UserManager<Usuario> userManager) : IUsuarioRepository
{
    public async Task AddAsync(Usuario usuario)
    {
        var claim = new Claim(nameof(TipoUsuario), usuario.Tipo.ToString(), ClaimValueTypes.String);

        await userManager.CreateAsync(usuario);
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
}