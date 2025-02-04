using HealthMed.Application.Contracts;
using HealthMed.Application.Services;

namespace HealthMed.Api.Setup;

public static class ConfigureServices
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICriaAgendaService, CriaAgendaService>();
        serviceCollection.AddScoped<IAgendarConsultaService, AgendarConsultaService>();
    }
}