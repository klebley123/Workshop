using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string OwnerName { get; set; }
        public string OwnerLastName { get; set; }
        //public DateTime ServiceStartDate { get; set; }
        //public DateTime PlannedEndDate { get; set; }
        //public string ServiceDescription { get; set; }

        public Car()
        {

        }

        public Car(int id, string brand, string model, int year, string ownerName, string ownerLastName)
        {
            this.Id = id;
            this.Brand = brand;
            this.Model = model;
            this.Year = year;
            this.OwnerName = ownerName;
            this.OwnerLastName = ownerLastName;
            //this.ServiceStartDate = serwisStartDate;
            //this.PlannedEndDate = plannedEndDate;
            //this.ServiceDescription = serviceDescription;
        }

        public Car(Car car)
        {
            this.Id = car.Id;
            this.Brand = car.Brand;
            this.Model = car.Model;
            this.Year = car.Year;
            this.OwnerName = car.OwnerName;
            this.OwnerLastName = car.OwnerLastName;
            //this.ServiceStartDate = car.ServiceStartDate;
            //this.PlannedEndDate= car.PlannedEndDate;
            //this.ServiceDescription = car.ServiceDescription;

        }
    }
}
