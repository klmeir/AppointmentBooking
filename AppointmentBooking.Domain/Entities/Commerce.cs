namespace AppointmentBooking.Domain.Entities
{
    public class Commerce : DomainEntity
    { 
        public string NameCommerce { get; set; }
        public int? MaxCapacity { get; set; }
        public virtual ICollection<Service>? Services { get; set; }

        public Commerce(string nameCommerce, int? maxCapacity)
        {            
            NameCommerce = nameCommerce;
            MaxCapacity = maxCapacity;
        }
    }
}
