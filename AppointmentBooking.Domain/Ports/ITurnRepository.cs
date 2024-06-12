using AppointmentBooking.Domain.Entities;

namespace AppointmentBooking.Domain.Ports
{
    public interface ITurnRepository
    {        
        Task<List<Turn>> GenerateTurns(TurnGenerate turnGenerate);
    }
}
