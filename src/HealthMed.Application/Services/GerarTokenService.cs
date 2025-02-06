using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HealthMed.Application.Contracts;
using HealthMed.Application.Responses;
using HealthMed.Core.Extensions;
using HealthMed.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HealthMed.Application.Services;

public class GerarTokenService : IGerarTokenService
{
    private IUsuarioRepository _usuarioRepository;
    private readonly AppSettings _appSettings;

    public GerarTokenService(IUsuarioRepository usuarioRepository,
        IOptions<AppSettings> appSettings)
    {
        _usuarioRepository = usuarioRepository;
        _appSettings = appSettings.Value;
    }

    public async Task<UsuarioLoginResponse> GerarJwt(Usuario usuario)
    {
        var claims = await _usuarioRepository.GetClaimsAsync(usuario);
        var usuarioClaims = await ObterClaimsUsuario(usuario, claims);
        var token = CodificarToken(usuarioClaims);

        return ObterRespostaToken(token, usuario, claims);
    }

    private async Task<ClaimsIdentity> ObterClaimsUsuario(Usuario usuario, ICollection<Claim> claims)
    {
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, usuario.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(),
            ClaimValueTypes.Integer64));

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        return identityClaims;
    }

    private string CodificarToken(ClaimsIdentity identityClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Emissor,
            Audience = _appSettings.ValidoEm,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        return tokenHandler.WriteToken(token);
    }

    private UsuarioLoginResponse ObterRespostaToken(string token, Usuario usuario, IEnumerable<Claim> claims)
    {
        return new UsuarioLoginResponse
        {
            AccessToken = token,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
            UsuarioToken = new UsuarioToken
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value })
            }
        };
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
            .TotalSeconds);
}