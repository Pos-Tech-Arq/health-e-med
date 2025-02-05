using HealthMed.Domain.Entities;
using HealthMed.Infra.Contexts.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace HealthMed.Infra.Contexts;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    DbSet<Agenda> Agendas { get; set; }
    DbSet<Consulta> Consultas { get; set; }
    DbSet<Usuario> Usuarios { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AgendaMapping());
        modelBuilder.ApplyConfiguration(new ConsultaMapping());
        modelBuilder.ApplyConfiguration(new UsuarioMapping());

        base.OnModelCreating(modelBuilder);
    }

}
