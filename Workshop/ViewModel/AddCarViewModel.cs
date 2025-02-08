using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Workshop.Command;
using Workshop.Data;
using Workshop.Models;

namespace Workshop.ViewModel
{
    public class AddCarViewModel : MainViewModel
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string OwnerName { get; set; }
        public string OwnerLastName { get; set; }
        //public DateTime ServiceStartDate { get; set; }
        //public DateTime PlannedEndDate { get; set; }
        //public string ServiceDescription { get; set; }

        public ICommand AddCarCommand { get; }
        public ICommand CancelCommand { get; }

        public AddCarViewModel()
        {
            //ServiceStartDate = DateTime.Now;
            //PlannedEndDate = DateTime.Now.Date.AddDays(1);
            AddCarCommand = new RelayCommand(AddCar);
            CancelCommand = new RelayCommand(Cancel);
        }

        //private void AddCar(object parameter)
        //{
        //    // Walidacja
        //    if (string.IsNullOrWhiteSpace(Brand) || string.IsNullOrWhiteSpace(Model) ||
        //        Year < 1886 || Year > DateTime.Now.Year || string.IsNullOrWhiteSpace(OwnerName) ||
        //        string.IsNullOrWhiteSpace(OwnerLastName) || ServiceStartDate < DateTime.Now.Date || 
        //        PlannedEndDate < ServiceStartDate)
        //    {
        //        MessageBox.Show("Wszyskie pola muszą zostać poprawnie wypełnione.");
        //        return;
        //    }

        //    try
        //    {
        //        using (var context = new WorkshopContext())
        //        {
        //            var newCar = new Car
        //            {
        //                Brand = this.Brand,
        //                Model = this.Model,
        //                Year = this.Year,
        //                OwnerName = this.OwnerName,
        //                OwnerLastName = this.OwnerLastName,
        //                ServiceStartDate = this.ServiceStartDate,
        //                PlannedEndDate = this.PlannedEndDate,
        //                ServiceDescription = this.ServiceDescription,
        //            };

        //            context.Cars.Add(newCar);
        //            context.SaveChanges();
        //        }

        //        MessageBox.Show("Auto zarejestrowane do serwisu!");
        //        CloseWindow(parameter); // Zamknij okno
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Wystąpił nie oczekiwany błąd podczas dodawnia: {ex.Message}");
        //    }
        //}

        private void AddCar(object parameter)
        {
            // Walidacja
            if (string.IsNullOrWhiteSpace(Brand))
            {
                MessageBox.Show("Pole 'Marka' nie może być puste.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Model))
            {
                MessageBox.Show("Pole 'Model' nie może być puste.");
                return;
            }

            if (Year < 1886 || Year > DateTime.Now.Year)
            {
                MessageBox.Show("Pole 'Rok' musi być wartością między 1886 a bieżącym rokiem.");
                return;
            }

            if (string.IsNullOrWhiteSpace(OwnerName))
            {
                MessageBox.Show("Pole 'Imię właściciela' nie może być puste.");
                return;
            }

            if (string.IsNullOrWhiteSpace(OwnerLastName))
            {
                MessageBox.Show("Pole 'Nazwisko właściciela' nie może być puste.");
                return;
            }

            try
            {
                using (var context = new WorkshopContext())
                {
                    var newCar = new Car
                    {
                        Brand = this.Brand,
                        Model = this.Model,
                        Year = this.Year,
                        OwnerName = this.OwnerName,
                        OwnerLastName = this.OwnerLastName,
                    };

                    context.Cars.Add(newCar);
                    context.SaveChanges();
                }

                MessageBox.Show("Auto zarejestrowane do serwisu!");
                CloseWindow(parameter); // Zamknij okno
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił nieoczekiwany błąd podczas dodawania: {ex.Message}");
            }
        }

        private void Cancel(object parameter)
        {
            CloseWindow(parameter); // Zamknij okno bez dodawania
        }

        private void CloseWindow(object parameter)
        {
            if (parameter is Window window)
            {
                window.DialogResult = true;
                window.Close();
            }
        }
    }
}
