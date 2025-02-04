using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Domain.Context.Mappings;

public class AgendaMapping : IEntityTypeConfiguration<Agenda>
{
    public void Configure(EntityTypeBuilder<Agenda> builder)
    {
        builder.ToTable("Agendas");

        builder
            .Property(c => c.Id)
            .IsRequired();

        builder.HasKey(c => c.Id);

        builder.Property(c => c.MedicoId)
            .IsRequired();

        builder.Property(c => c.Data)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(c => c.HorarioInicio)
            .HasColumnType("time")
            .IsRequired();

        builder.Property(c => c.HorarioFim)
            .HasColumnType("time")
            .IsRequired();

        builder.Property(c => c.Valor)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
    }
}