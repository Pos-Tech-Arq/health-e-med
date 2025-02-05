using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HealthMed.Infra.Configurations;

public static class ConfigureJWTAuthenticationExtension
{
    public static void ConfigureJWTAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var key = Encoding.UTF8.GetBytes("b5X8mL2qR9vT1zN7aP3cY6wK0gD4sF!");
        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        serviceCollection.AddAuthorization(options =>
        {
            options.AddPolicy("MedicoPolicy", policy => policy.RequireRole("Medico"));
            options.AddPolicy("UsuarioPolicy", policy => policy.RequireRole("Usuario"));
        });

    }
}
