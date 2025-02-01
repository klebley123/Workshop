using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class AddServiceOrderViewModel : MainViewModel
    {
        public ObservableCollection<Car> Cars { get; set; }
        public Car SelectedCar { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public string ServiceDescription { get; set; }
        public string SearchText { get; set; }
        public ObservableCollection<Car> FilteredCars { get; set; }
        public ObservableCollection<Car> SelectedCarAsList { get; set; }



        public ICommand SaveServiceOrderCommand { get; }
        public ICommand CancelCommand { get; }

        public AddServiceOrderViewModel(Car selectedCar)
        {
            SelectedCarAsList = new ObservableCollection<Car> { selectedCar };
            ServiceStartDate = DateTime.Now;
            PlannedEndDate = DateTime.Now.AddDays(1);
            SelectedCar = selectedCar;
            SaveServiceOrderCommand = new RelayCommand(SaveServiceOrder);
            CancelCommand = new RelayCommand(CloseWindow);
        }

        public AddServiceOrderViewModel()
        {
        }

        //private void LoadCars()
        //{
        //    using (var context = new WorkshopContext())
        //    {
        //        var cars = context.Cars.ToList();
        //        Cars.Clear();
        //        foreach (var car in cars)
        //        {
        //            Cars.Add(car);
        //        }
        //        FilterCars(); // Inicjalne filtrowanie
        //    }
        //}

        private void SaveServiceOrder()
        {
            if (ServiceStartDate > PlannedEndDate)
            {
                MessageBox.Show("Data zakończenia nie może być wcześniejsza niż data rozpoczęcia.");
                return;
            }

            using (var context = new WorkshopContext())
            {
                var serviceOrder = new ServiceOrder
                {
                    CarId = SelectedCarAsList.First().Id,
                    ServiceStartDate = ServiceStartDate,
                    PlannedEndDate = PlannedEndDate,
                    ServiceDescription = ServiceDescription
                };

                context.ServiceOrders.Add(serviceOrder);
                context.SaveChanges();
            }

            MessageBox.Show("Zgłoszenie serwisowe zostało zapisane.");
            CloseWindow();
        }

        private void CloseWindow()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive)?.Close();
        }


        //private void FilterCars()
        //{
        //    if (string.IsNullOrWhiteSpace(SearchText))
        //    {
        //        // Jeśli pole wyszukiwania jest puste, wyświetl wszystkie samochody
        //        FilteredCars.Clear();
        //        foreach (var car in Cars)
        //        {
        //            FilteredCars.Add(car);
        //        }
        //    }
        //    else
        //    {
        //        // Filtrowanie na podstawie tekstu wyszukiwania
        //        var filtered = Cars.Where(car =>
        //            car.Brand.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
        //            car.Model.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();

        //        FilteredCars.Clear();
        //        foreach (var car in filtered)
        //        {
        //            FilteredCars.Add(car);
        //        }
        //    }
        //}

    }
}
