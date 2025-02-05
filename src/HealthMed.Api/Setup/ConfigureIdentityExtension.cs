using HealthMed.Domain.Context;
using HealthMed.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace HealthMed.Api.Setup;

public static class ConfigureIdentityExtension
{
    public static void ConfigureIdentity(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }
}