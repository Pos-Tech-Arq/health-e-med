using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Domain.Context.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100); 

        builder.Property(u => u.Senha)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.Tipo)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(u => u.Cpf)
            .IsRequired()
            .HasMaxLength(14); 

        builder.Property(u => u.Crm)
            .HasMaxLength(20).IsRequired(false); 

        builder.Property(u => u.Especialidade)
            .HasMaxLength(100).IsRequired(false); 

        builder.ToTable("Usuarios");

        // Configura índices únicos
        builder.HasIndex(u => u.Email)
            .IsUnique(); 

        builder.HasIndex(u => u.Cpf)
            .IsUnique();

        builder.HasIndex(u => u.Crm)
            .IsUnique() 
            .HasFilter("[Crm] IS NOT NULL"); 

        // Configuração condicional para campos específicos de médicos
        builder.HasCheckConstraint("CK_Usuario_Tipo", "[Tipo] IN ('medico', 'paciente')");
    }
}
