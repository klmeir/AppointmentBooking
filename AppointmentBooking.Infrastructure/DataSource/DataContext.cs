//using AppointmentBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AppointmentBooking.Infrastructure.DataSource
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _config;
        public DataContext(DbContextOptions<DataContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                return;
            }

            // load all entity config from current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

            // if using schema in db, uncomment the following line
            // modelBuilder.HasDefaultSchema(_config.GetValue<string>("SchemaName"));
            //modelBuilder.Entity<Commerce>();
            //modelBuilder.Entity<Service>();
            //modelBuilder.Entity<Turn>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
