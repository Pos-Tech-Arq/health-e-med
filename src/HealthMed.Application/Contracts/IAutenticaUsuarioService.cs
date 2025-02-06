using HealthMed.Application.Commands;

namespace HealthMed.Application.Contracts;

public interface IAutenticaUsuarioService
{
    Task Handle(RegistrarUsuarioCommand command);
    Task<string> Handle(LoginPacienteCommand command);
}