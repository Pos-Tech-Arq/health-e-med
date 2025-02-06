using HealthMed.Application.Contracts;
using HealthMed.Application.Services;

namespace HealthMed.Api.Setup;

public static class ConfigureRepositories
{
    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICriaAgendaService, CriaAgendaService>();
        serviceCollection.AddScoped<IAgendarConsultaService, AgendarConsultaService>();
    }
}