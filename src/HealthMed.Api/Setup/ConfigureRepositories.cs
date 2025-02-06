using HealthMed.Application.Contracts;
using HealthMed.Application.Services;
using HealthMed.Infra.Repositories;

namespace HealthMed.Api.Setup;

public static class ConfigureRepositories
{
    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAgendaRepository, AgendaRepository>();
        serviceCollection.AddScoped<IConsultaRepository, ConsultaRepository>();
        serviceCollection.AddScoped<IUsuarioRepository, UsuarioRepository>();
    }
}