using AppointmentBooking.Application.Turns;
using FluentValidation;

namespace AppointmentBooking.Api.ApiHandlers
{
    public class TurnGenerateValidator : AbstractValidator<TurnGenerateCommand>
    {
        private string DateFormat { get; set; }

        public TurnGenerateValidator()
        {
            DateFormat = "dd/MM/yyyy";
            RuleFor(x => x.StartDate).NotEmpty().Must(BeValidDate).WithMessage($"Invalid date format. The date should be in format '{DateFormat}'"); ;
            RuleFor(x => x.EndDate).NotEmpty().Must(BeValidDate).WithMessage($"Invalid date format. The date should be in format '{DateFormat}'"); ;
            RuleFor(x => x.IdService).NotEmpty().WithMessage("IdService is required");
        }

        private bool BeValidDate(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return true;

            return DateTime.TryParseExact(date, DateFormat, null, System.Globalization.DateTimeStyles.None, out _);
        }
    }
}
