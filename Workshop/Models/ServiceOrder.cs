using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Models
{
    public class ServiceOrder
    {
        public int Id { get; set; } // Klucz główny
        public int CarId { get; set; } // Klucz obcy do tabeli Cars
        public DateTime ServiceStartDate { get; set; } // Data rozpoczęcia serwisu
        public DateTime PlannedEndDate { get; set; } // Planowana data zakończenia serwisu
        public string ServiceDescription { get; set; } // Opis czynności serwisowych
        public Car Car { get; set; } // Nawigacja do powiązanego samochodu
    }
}
