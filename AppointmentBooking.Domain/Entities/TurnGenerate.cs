namespace AppointmentBooking.Domain.Entities
{
    public class TurnGenerate
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int IdServicio { get; set; }

        public TurnGenerate(DateOnly startDate, DateOnly endDate, int idServicio)
        {
            StartDate = startDate;
            EndDate = endDate;
            IdServicio = idServicio;
        }
    }
}
