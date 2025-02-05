using System;
using System.Collections.Generic;
using System.Linq;
using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Infra.Contexts.Mappings;

    public class AgendaMapping : IEntityTypeConfiguration<Agenda>
    {
        public void Configure(EntityTypeBuilder<Agenda> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Data)
                .IsRequired()
                .HasColumnType("date"); 

            builder.Property(a => a.HorarioInicio)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(a => a.HorarioFim)
                .IsRequired()
                .HasColumnType("time");

        builder.Property(a => a.Valor)
              .HasColumnType("decimal(18,2)") 
              .HasPrecision(18, 2).IsRequired(); 

        builder.ToTable("Agendas");
        }
    }
