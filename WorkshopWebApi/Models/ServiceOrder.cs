namespace WorkshopWebApi.Models
{
    public class ServiceOrder
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public string ServiceDescription { get; set; }
        public Car Car { get; set; }
    }
}
