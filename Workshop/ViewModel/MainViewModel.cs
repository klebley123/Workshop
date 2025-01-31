using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Workshop.Command;
using Workshop.Data;
using Workshop.Models;
using Workshop.Services;
using System.Data.Entity;
using Workshop.Views;
using System.Data.SqlClient;

namespace Workshop.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;

        private string _searchText;
        private Car _selectedCar;

        public ICommand OpenAddCarWindowCommand { get; }
        public ICommand ConnectToDBCommand { get; }
        public ICommand OpenPropertiesCarWindowCommand { get; }
        public ICommand OpenServiceOrderWindowCommand { get; }
        public ICommand FilterCarsCommand { get; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterCars();
            }
        }

        public Car SelectedCar
        {
            get => _selectedCar;
            set
            {
                _selectedCar = value;
                OnPropertyChanged(nameof(SelectedCar));
            }
        }

        public ObservableCollection<Car> Cars { get; }
        public ObservableCollection<Car> FilteredCars { get; }
        public ObservableCollection<ServiceOrder> ServiceOrders { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            Cars = new ObservableCollection<Car>();
            FilteredCars = new ObservableCollection<Car>();
            ServiceOrders = new ObservableCollection<ServiceOrder>();
            _apiService = new ApiService();

            LoadCarsFromDatabase();
            LoadServiceOrders();
            LoadData();

            OpenAddCarWindowCommand = new RelayCommand(OpenAddCarWindow);
            ConnectToDBCommand = new RelayCommand(ConnectToDB);
            OpenPropertiesCarWindowCommand = new RelayCommand(OpenCarPropertiesWindow);
            OpenServiceOrderWindowCommand = new RelayCommand(OpenServiceOrderWindow);

            FilterCarsCommand = new RelayCommand(FilterCars);
        }

        private async void LoadData()
        {
            var cars = await _apiService.GetCarsAsync();
            Cars.Clear();
            foreach (var car in cars)
            {
                Cars.Add(car);
            }

            var serviceOrders = await _apiService.GetServiceOrdersAsync();
            ServiceOrders.Clear();
            foreach (var order in serviceOrders)
            {
                ServiceOrders.Add(order);
            }
        }

        private void LoadServiceOrders()
        {
            using (var context = new WorkshopContext())
            {
                var orders = context.ServiceOrders.Include(so => so.Car).ToList();
                ServiceOrders.Clear();
                foreach (var order in orders)
                {
                    ServiceOrders.Add(order);
                }
            }
        }

        private void LoadCarsFromDatabase()
        {
            try
            {
                using (var context = new WorkshopContext())
                {
                    var carsFromDb = context.Cars.ToList();
                    Cars.Clear();
                    foreach (var car in carsFromDb)
                    {
                        Cars.Add(car);
                    }
                }
                FilterCars(); // Inicjalne filtrowanie
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas ładowania danych: {ex.Message}");
            }
        }

        private void FilterCars()
        {
            FilteredCars.Clear();

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                foreach (var car in Cars)
                {
                    FilteredCars.Add(car);
                }
            }
            else
            {
                var filtered = Cars.Where(car =>
                    car.Brand.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    car.Model.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    car.OwnerName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    car.OwnerLastName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    car.Year.ToString().Contains(SearchText)).ToList();

                foreach (var car in filtered)
                {
                    FilteredCars.Add(car);
                }
            }
        }

        private void OpenAddCarWindow(object parameter)
        {
            var addCarWindow = new AddCar();
            bool? result = addCarWindow.ShowDialog();

            if (result == true) // Jeśli samochód został dodany
            {
                LoadCarsFromDatabase(); // Odśwież dane w DataGrid
            }
        }

        private void OpenCarPropertiesWindow(object parameter)
        {
            if (SelectedCar != null)
            {
                var editWindow = new PropertiesCars(SelectedCar);
                bool? result = editWindow.ShowDialog();

                if (result == true)
                {
                    LoadCarsFromDatabase(); // Odśwież dane po edycji
                }
            }
        }

        private void OpenServiceOrderWindow(object parameter)
        {
            if (SelectedCar == null)
            {
                MessageBox.Show("Wybierz samochód przed zgłoszeniem serwisu.");
                return;
            }

            // Tworzenie instancji widoku i przekazanie wybranego samochodu
            var serviceOrderWindow = new ServiceOrderView(SelectedCar);

            serviceOrderWindow.ShowDialog(); // Wyświetlenie okna modalnego
            LoadServiceOrders(); // Odśwież zgłoszenia po zamknięciu okna
        }




        private bool CanEditSelectedCar(object parameter)
        {
            return SelectedCar != null; // Polecenie jest aktywne tylko, gdy wybrano samochód
        }

        private void ConnectToDB()
        {
            string connetionString;
            SqlConnection connection;
            connetionString = @"Data Source=LAPTOP-RJLJ2K5G;Initial Catalog=Workshop; Integrated Security=True; TrustServerCertificate=True; User ID=sa;Password=haslo";
            connection = new SqlConnection(connetionString);
            connection.Open();
            MessageBox.Show("Połączono z bazą danych!");
            connection.Close();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
