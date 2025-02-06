using HealthMed.Application.Responses;
using HealthMed.Domain.Entities;

namespace HealthMed.Application.Contracts;

public interface IGerarTokenService
{
    Task<UsuarioLoginResponse> GerarJwt(Usuario usuario);
}