using HealthMed.Domain.Entities;

namespace HealthMed.Application.Contracts;

public interface IUsuarioRepository
{
    Task AddAsync(Usuario usuario);
}