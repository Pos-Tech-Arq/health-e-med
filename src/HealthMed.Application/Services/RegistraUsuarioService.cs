using HealthMed.Application.Commands;
using HealthMed.Application.Contracts;
using HealthMed.Domain.Entities;

namespace HealthMed.Application.Services;

public class RegistraUsuarioService(IUsuarioRepository usuarioRepository) : IRegistraUsuarioService
{
    public Task Handle(RegistrarUsuarioCommand command)
    {
        var usuario = new Usuario(command.Nome, command.Email, command.Senha, command.Tipo, command.Cpf, command.Crm,
            command.Especialidade);

        return usuarioRepository.AddAsync(usuario);
    }
}