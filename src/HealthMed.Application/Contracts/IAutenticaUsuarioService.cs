using HealthMed.Application.Commands;
using HealthMed.Application.Responses;

namespace HealthMed.Application.Contracts;

public interface IAutenticaUsuarioService
{
    Task Handle(RegistrarUsuarioCommand command);
    Task<UsuarioLoginResponse> Handle(LoginPacienteCommand command);
    Task<UsuarioLoginResponse> Handle(LoginMedicoCommand command);
}