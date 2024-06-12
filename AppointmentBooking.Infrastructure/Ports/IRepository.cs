using AppointmentBooking.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace AppointmentBooking.Infrastructure.Ports
{
    public interface IRepository<T> where T : DomainEntity
    {
        Task<List<T>> QueryProcedure(string procedureName, params SqlParameter[] sqlParameters);

    }
}
