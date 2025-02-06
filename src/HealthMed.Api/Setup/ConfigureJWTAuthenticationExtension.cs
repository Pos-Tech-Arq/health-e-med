using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace HealthMed.Api.Setup;

public static class ConfigureJwtAuthenticationExtension
{
    public static void ConfigureJwtAuthentication(this IServiceCollection serviceCollection,
        IConfiguration configuration)
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
            options.AddPolicy("SomenteMedico", policy => policy.RequireRole("Medico"));
            options.AddPolicy("SomentePaciente", policy => policy.RequireRole("Paciente"));
        });
    }
}