using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Domain.Ports;
using AppointmentBooking.Infrastructure.Ports;
using Microsoft.Data.SqlClient;

namespace AppointmentBooking.Infrastructure.Adapters
{
    [Repository]
    public class TurnRepository : ITurnRepository
    {
        readonly IRepository<Turn> _dataSource;
        public TurnRepository(IRepository<Turn> dataSource) => _dataSource = dataSource
        ?? throw new ArgumentNullException(nameof(dataSource));

        public Task<List<Turn>> GenerateTurns(TurnGenerate turnGenerate)
        {
            var spName = "GenerarTurnos";
            var parameters = new SqlParameter[] { 
                new SqlParameter("fecha_inicio", turnGenerate.StartDate),
                new SqlParameter("fecha_fin", turnGenerate.EndDate),
                new SqlParameter("id_servicio", turnGenerate.IdServicio)
            };

            return _dataSource.QueryProcedure(spName, parameters);
        }
    }
}
