using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Domain.Context.Mappings;

public class ConsultaMapping : IEntityTypeConfiguration<Consulta>
{
    public void Configure(EntityTypeBuilder<Consulta> builder)
    {
        builder.ToTable("Consultas");
        
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
            .HasMaxLength(50); 
    
        builder.HasOne(c => c.Paciente)
            .WithMany() 
            .HasForeignKey(c => c.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(c => c.Medico)
            .WithMany() 
            .HasForeignKey(c => c.MedicoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}