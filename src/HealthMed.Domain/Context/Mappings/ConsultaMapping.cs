using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Domain.Context.Mappings;

public class ConsultaMapping : IEntityTypeConfiguration<Consulta>
{
    public void Configure(EntityTypeBuilder<Consulta> builder)
    {
        builder.ToTable("Consultas");

        builder
            .Property(c => c.Id)
            .IsRequired();

        builder.HasKey(c => c.Id);

        builder.Property(c => c.PacienteId)
            .IsRequired();

        builder.Property(c => c.MedicoId)
            .IsRequired();

        builder.Property(c => c.Data)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(c => c.Horario)
            .HasColumnType("time")
            .IsRequired();

        builder.Property(c => c.Status)
            .HasColumnType("varchar(20)")
            .IsRequired();
    }
}