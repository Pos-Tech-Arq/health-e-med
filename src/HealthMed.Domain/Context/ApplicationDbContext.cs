using HealthMed.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Domain.Context;

public class ApplicationDbContext : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>
{
    public DbSet<Agenda> Agendas { get; set; }
    public DbSet<Consulta> Consultas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}