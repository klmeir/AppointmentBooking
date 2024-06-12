using AppointmentBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentBooking.Infrastructure.DataSource.ModelConfig
{
    public class CommerceConfiguration : IEntityTypeConfiguration<Commerce>
    {        
        public void Configure(EntityTypeBuilder<Commerce> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("comercios");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnName("id_comercio").IsRequired();

            builder.Property(b => b.NameCommerce).HasColumnName("nom_comercio").IsRequired();
            builder.Property(b => b.MaxCapacity).HasColumnName("aforo_maximo");

            builder
               .HasMany(b => b.Services)
               .WithOne(b => b.Commerce)
               .HasForeignKey(b => b.IdCommerce)
               .IsRequired();
        }
    }
}
