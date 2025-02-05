using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Infra.Contexts.Mappings;

public class ConsultaMapping : IEntityTypeConfiguration<Consulta>
{
    public void Configure(EntityTypeBuilder<Consulta> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.PacienteId)
            .IsRequired();

        builder.Property(c => c.MedicoId)
            .IsRequired();

        builder.Property(c => c.Data)
            .IsRequired()
            .HasColumnType("date"); 

        builder.Property(c => c.Horario)
            .IsRequired()
            .HasColumnType("time"); 
        builder.Property(c => c.Status)
            .IsRequired()
            .HasMaxLength(50)
            .HasDefaultValue("Pendente"); 

        // Define o nome da tabela no banco de dados
        builder.ToTable("Consultas");

        // Configura relacionamentos
        builder.HasOne(c => c.Paciente)
            .WithMany(u => u.ConsultasComoPaciente) 
            .HasForeignKey(c => c.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Medico)
            .WithMany(u => u.ConsultasComoMedico) 
            .HasForeignKey(c => c.MedicoId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
