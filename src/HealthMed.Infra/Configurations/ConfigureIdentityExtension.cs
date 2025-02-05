using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using HealthMed.Infra.Contexts;
using HealthMed.Domain.Identity;

namespace HealthMed.Infra.Configurations;

public static class ConfigureIdentityExtension
{
    public static void ConfigureIdentity(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddIdentity<ApplicationUser, IdentityRole>()
                        .AddEntityFrameworkStores<ApplicationDbContext>()
                        .AddDefaultTokenProviders();
    }
}
