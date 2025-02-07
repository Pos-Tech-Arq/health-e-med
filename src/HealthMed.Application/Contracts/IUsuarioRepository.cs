using System.Security.Claims;
using HealthMed.Core.Enums;
using HealthMed.Domain.Entities;

namespace HealthMed.Application.Contracts;

public interface IUsuarioRepository
{
    Task AddAsync(Usuario usuario, string senha);
    Task<Usuario> GetAsync(string? cpf = null, string? crm  = null);
    Task<IList<Claim>> GetClaimsAsync(Usuario usuario);
    Task<IEnumerable<Usuario>> GetUsuarios(TipoUsuario? tipoUsuario, string? especialidade);
}