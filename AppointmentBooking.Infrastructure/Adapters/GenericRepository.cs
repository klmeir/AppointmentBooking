using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Infrastructure.DataSource;
using AppointmentBooking.Infrastructure.Ports;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Infrastructure.Adapters
{
    public class GenericRepository<T> : IRepository<T> where T : DomainEntity
    {
        readonly DataContext Context;
        readonly DbSet<T> _dataset;

        public GenericRepository(DataContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _dataset = Context.Set<T>();
        }

        public async Task<List<T>> QueryProcedure(string procedureName, params SqlParameter[] sqlParameters)
        {
            var query = $"EXEC {procedureName} {string.Join(", ", sqlParameters.Select(s => $"@{s.ParameterName}"))}";

            return await Context.Set<T>()
                .FromSqlRaw(query, sqlParameters as object[])
                .ToListAsync();
        }

    }
}
