using HealthMed.Application.Commands;
using HealthMed.Application.Responses;
using HealthMed.Domain.Entities;

namespace HealthMed.Application.Contracts;

public interface IAutenticaUsuarioService
{
    Task<UsuarioLoginResponse> Handle(RegistrarUsuarioCommand command);
    Task<UsuarioLoginResponse> Handle(LoginPacienteCommand command);
    Task<UsuarioLoginResponse> Handle(LoginMedicoCommand command);

}