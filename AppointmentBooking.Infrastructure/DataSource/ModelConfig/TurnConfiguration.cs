using AppointmentBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentBooking.Infrastructure.DataSource.ModelConfig
{
    public class TurnConfiguration : IEntityTypeConfiguration<Turn>
    {        
        public void Configure(EntityTypeBuilder<Turn> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("turnos");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnName("id_turno").IsRequired();

            builder.Property(b => b.IdService).HasColumnName("id_servicio").IsRequired();
            builder.HasIndex(b => b.IdService);

            builder.Property(b => b.TurnDate).HasColumnName("fecha_turno").IsRequired();
            builder.Property(b => b.StartTime).HasColumnName("hora_inicio").IsRequired();
            builder.Property(b => b.EndTime).HasColumnName("hora_fin").IsRequired();
            builder.Property(b => b.Status).HasColumnName("estado");

            //builder
            //   .HasOne(b => b.Service)
            //   .WithMany(b => b.Turns)
            //   .HasForeignKey(b => b.IdService)
            //   .IsRequired();
        }
    }
}
