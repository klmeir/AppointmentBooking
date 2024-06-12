using AppointmentBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentBooking.Infrastructure.DataSource.ModelConfig
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {        
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("servicios");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnName("id_servicio").IsRequired();

            builder.Property(b => b.IdCommerce).HasColumnName("id_comercio").IsRequired();
            builder.HasIndex(b => b.IdCommerce);

            builder.Property(b => b.ServiceName).HasColumnName("nom_servicio").IsRequired();
            builder.Property(b => b.OpeningTime).HasColumnName("hora_apertura").IsRequired();
            builder.Property(b => b.ClosingTime).HasColumnName("hora_cierre").IsRequired();
            builder.Property(b => b.Duration).HasColumnName("duracion").IsRequired();

            builder
               .HasMany(b => b.Turns)
               .WithOne(b => b.Service)
               .HasForeignKey(b => b.IdService)
               .IsRequired();
        }
    }
}
