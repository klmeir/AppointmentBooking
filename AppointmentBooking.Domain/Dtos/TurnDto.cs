namespace AppointmentBooking.Domain.Dtos
{
    public record TurnDto(int Id, int IdService, string Service, DateTime TurnDate, TimeSpan StartTime, TimeSpan EndTime, bool Status);
}
