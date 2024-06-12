using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Domain.Exception;
using AppointmentBooking.Domain.Ports;
using System.Diagnostics.Metrics;

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
            CheckValidStartDate(turnGenerate);
            CheckValidEndDate(turnGenerate);
            return await _turnRepository.GenerateTurns(turnGenerate);
        }

        void CheckValidStartDate(TurnGenerate t)
        {
            if (t.IsStartDateValid)
            {
                throw new CoreBusinessException("Start date must be equal to or greater than the current date");
            }
        }

        void CheckValidEndDate(TurnGenerate t)
        {
            if (t.IsEndDateValid)
            {
                throw new CoreBusinessException("End date must be equal to or greater than the start date");
            }
        }
    }
}
