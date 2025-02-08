using HealthMed.Application.Commands;
using HealthMed.Application.Contracts;
using HealthMed.Application.Responses;
using HealthMed.Core.Exceptions;
using HealthMed.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HealthMed.Application.Services;

public class AutenticaUsuarioService(
    IUsuarioRepository usuarioRepository,
    IGerarTokenService tokenService,
    SignInManager<Usuario> signInManager)
    : IAutenticaUsuarioService
{
    public async Task<UsuarioLoginResponse> Handle(RegistrarUsuarioCommand command)
    {
        var usuario = new Usuario(command.Nome, command.Email, command.Tipo, command.Cpf, command.Crm,
            command.Especialidade);

        await usuarioRepository.AddAsync(usuario, command.Senha);

        await signInManager.PasswordSignInAsync(usuario.Email, command.Senha, false, false);
        return await tokenService.GerarJwt(usuario);
    }

    public async Task<UsuarioLoginResponse> Handle(LoginPacienteCommand command)
    {
        var usuario = await usuarioRepository.GetAsync(cpf: command.Cpf);
        await ValidaLogin(command.Senha, usuario);

        return await tokenService.GerarJwt(usuario);
    }

    public async Task<UsuarioLoginResponse> Handle(LoginMedicoCommand command)
    {
        var usuario = await usuarioRepository.GetAsync(crm: command.Crm);
        await ValidaLogin(command.Senha, usuario);

        return await tokenService.GerarJwt(usuario);
    }

    public async Task ValidaLogin(string senha, Usuario usuario)
    {
        var result = await signInManager.PasswordSignInAsync(usuario.Email, senha, false, false);

        if (!result.Succeeded)
        {
            throw new DomainException("Usuário ou senha inválidos.");
        }
    }
}