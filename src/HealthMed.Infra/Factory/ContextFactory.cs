using System.Text.Json;
using System.Text.Json.Nodes;
using HealthMed.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HealthMed.Infra.Factory;

public class ContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var file = Path.Combine("..", "HealthMed.Api", "appsettings.json");
        Console.WriteLine(file);
        var jsonString = File.ReadAllText(file);
        var json = JsonSerializer.Deserialize<JsonNode>(jsonString);
        string connectionString = (string)json!["ConnectionStrings"]!["DefaultConnection"]!;
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseSqlServer(connectionString,
            b => b.MigrationsAssembly("HealthMed.Infra"));
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}