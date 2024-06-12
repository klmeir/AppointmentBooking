using AppointmentBooking.Domain.Dtos;
using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Domain.Services;
using MediatR;

namespace AppointmentBooking.Application.Turns
{
    public class TurnGenerateCommandHandler : IRequestHandler<TurnGenerateCommand, List<TurnDto>>
    {
        private readonly GenerateTurnService _service;

        public TurnGenerateCommandHandler(GenerateTurnService service) =>
            _service = service ?? throw new ArgumentNullException(nameof(service));


        public async Task<List<TurnDto>> Handle(TurnGenerateCommand request, CancellationToken cancellationToken)
        {
            var startDateDt = DateTime.Parse(request.StartDate);
            var endDateDt = DateTime.Parse(request.EndDate);

            var startDate = new DateOnly(startDateDt.Year, startDateDt.Month, startDateDt.Day);
            var endDate = new DateOnly(endDateDt.Year, endDateDt.Month, endDateDt.Day);

            var turnsSaved = await _service.GenerateTurns(
                new TurnGenerate(startDate, endDate, request.IdService)
            );

            var turnsDtos = turnsSaved
                .Select(t => new TurnDto(t.Id, t.IdService, t.Service?.ServiceName!, t.TurnDate, t.StartTime, t.EndTime, t.Status))
                .ToList();

            return turnsDtos;
        }
    }
}
