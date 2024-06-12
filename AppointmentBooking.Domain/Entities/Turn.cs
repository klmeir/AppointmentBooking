namespace AppointmentBooking.Domain.Entities
{
    public class Turn : DomainEntity
    {                
        public int IdService { get; set; }                       
        public DateTime TurnDate { get; set; }        
        public TimeSpan StartTime { get; set; }        
        public TimeSpan EndTime { get; set; }        
        public bool Status { get; set; }

        public virtual Service Service { get; set; }

        public Turn(int idService, DateTime turnDate, TimeSpan startTime, TimeSpan endTime, bool status)
        {            
            IdService = idService;
            TurnDate = turnDate;
            StartTime = startTime;
            EndTime = endTime;
            Status = status;
        }
    }
}
