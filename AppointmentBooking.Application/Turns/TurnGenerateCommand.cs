using AppointmentBooking.Domain.Dtos;
using MediatR;

namespace AppointmentBooking.Application.Turns
{
    public record TurnGenerateCommand(string StartDate, string EndDate, int IdService) : IRequest<List<TurnDto>>;
}
