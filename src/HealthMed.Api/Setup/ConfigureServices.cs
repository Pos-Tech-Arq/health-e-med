using HealthMed.Application.Contracts;
using HealthMed.Application.Services;

namespace HealthMed.Api.Setup;

public static class ConfigureServices
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICriarAlterarAgendaService, CriarAlterarAgendaService>();
        serviceCollection.AddScoped<IAgendarConsultaService, AgendarConsultaService>();
        serviceCollection.AddScoped<IAutenticaUsuarioService, AutenticaUsuarioService>();
        serviceCollection.AddScoped<IGerarTokenService, GerarTokenService>();
        serviceCollection.AddScoped<IFiltrarAgendaService, FiltrarAgendaService>();
    }
}