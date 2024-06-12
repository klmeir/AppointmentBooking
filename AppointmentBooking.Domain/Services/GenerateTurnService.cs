using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Domain.Ports;

namespace AppointmentBooking.Domain.Services
{
    [DomainService]
    public class GenerateTurnService
    {
        private readonly ITurnRepository _turnRepository;

        public GenerateTurnService(ITurnRepository turnRepository)
        {
            _turnRepository = turnRepository;
        }

        public async Task<List<Turn>> GenerateTurns(TurnGenerate turnGenerate) 
        {
            return await _turnRepository.GenerateTurns(turnGenerate);
        }
    }
}
