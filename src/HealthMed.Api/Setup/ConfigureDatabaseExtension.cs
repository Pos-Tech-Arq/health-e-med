using HealthMed.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Api.Setup;

public static class ConfigureDatabaseExtension
{
    public static void ConfigureDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(
            options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("HealthMed.Infra")
                ));
    }
}