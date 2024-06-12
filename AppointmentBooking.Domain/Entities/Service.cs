namespace AppointmentBooking.Domain.Entities
{
    public class Service : DomainEntity
    { 
        public int IdCommerce { get; set; }
        public string ServiceName { get; set; }        
        public TimeSpan OpeningTime { get; set; }        
        public TimeSpan ClosingTime { get; set; }        
        public int Duration { get; set; }
                       
        public virtual Commerce Commerce { get; set; }        
        public virtual ICollection<Turn>? Turns { get; set; }

        public Service(int idCommerce, string serviceName, TimeSpan openingTime, TimeSpan closingTime, int duration)
        {   
            IdCommerce = idCommerce;
            ServiceName = serviceName;
            OpeningTime = openingTime;
            ClosingTime = closingTime;
            Duration = duration;
        }
    }
}
