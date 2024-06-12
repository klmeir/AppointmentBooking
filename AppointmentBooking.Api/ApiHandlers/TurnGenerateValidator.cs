using AppointmentBooking.Application.Turns;
using FluentValidation;

namespace AppointmentBooking.Api.ApiHandlers
{
    public class TurnGenerateValidator : AbstractValidator<TurnGenerateCommand>
    {
        public TurnGenerateValidator()
        {            
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
            RuleFor(x => x.IdService).NotEmpty();
        }
    }
}
