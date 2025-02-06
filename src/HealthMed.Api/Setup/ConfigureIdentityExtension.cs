using HealthMed.Domain.Context;
using HealthMed.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HealthMed.Api.Setup;

public static class ConfigureIdentityExtension
{
    public static void ConfigureIdentity(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddIdentity<Usuario, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void UseIdentityConfiguration(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}