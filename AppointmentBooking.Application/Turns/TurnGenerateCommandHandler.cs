using AppointmentBooking.Domain.Dtos;
using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Domain.Services;
using MediatR;
using Microsoft.VisualBasic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppointmentBooking.Application.Turns
{
    public class TurnGenerateCommandHandler : IRequestHandler<TurnGenerateCommand, List<TurnDto>>
    {
        private readonly GenerateTurnService _service;

        public TurnGenerateCommandHandler(GenerateTurnService service) =>
            _service = service ?? throw new ArgumentNullException(nameof(service));


        public async Task<List<TurnDto>> Handle(TurnGenerateCommand request, CancellationToken cancellationToken)
        {            
            DateTime.TryParseExact(request.StartDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out var startDateDt);
            DateTime.TryParseExact(request.EndDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out var endDateDt);

            var startDate = new DateOnly(startDateDt.Year, startDateDt.Month, startDateDt.Day);
            var endDate = new DateOnly(endDateDt.Year, endDateDt.Month, endDateDt.Day);

            var turnsSaved = await _service.GenerateTurns(
                new TurnGenerate(startDate, endDate, request.IdService)
            );

            var turnsDtos = turnsSaved
                .Select(t => new TurnDto(t.Id, t.IdService, t.TurnDate, t.StartTime, t.EndTime, t.Status))
                .ToList();

            return turnsDtos;
        }
    }
}
