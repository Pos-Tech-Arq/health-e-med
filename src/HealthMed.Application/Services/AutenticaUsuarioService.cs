using HealthMed.Application.Commands;
using HealthMed.Application.Contracts;
using HealthMed.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HealthMed.Application.Services;

public class AutenticaUsuarioService(IUsuarioRepository usuarioRepository, SignInManager<Usuario> signInManager) : IAutenticaUsuarioService
{
    public Task Handle(RegistrarUsuarioCommand command)
    {
        var usuario = new Usuario(command.Nome, command.Email, command.Senha, command.Tipo, command.Cpf, command.Crm,
            command.Especialidade);

        return usuarioRepository.AddAsync(usuario);
    }

    public async Task<string> Handle(LoginPacienteCommand command)
    {
        var usuario = await usuarioRepository.GetAsync(cpf: command.Cpf);
         //TODO Gerar token
        await signInManager.SignInAsync(usuario, false);
        return string.Empty;
    }
}