using HealthMed.Application.Commands;

namespace HealthMed.Application.Contracts;

public interface IRegistraUsuarioService
{
    Task Handle(RegistrarUsuarioCommand command);
}