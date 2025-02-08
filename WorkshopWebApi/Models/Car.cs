namespace WorkshopWebApi.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string OwnerName { get; set; } = string.Empty;   
        public string OwnerLastName { get; set; } = string.Empty; 
        public bool IsDeleted { get; set; } = false;

    }
}
