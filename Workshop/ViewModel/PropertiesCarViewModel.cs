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
    public class PropertiesCarViewModel : MainViewModel
    {
        public Car PropSelectedCar { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }

        public PropertiesCarViewModel(Car car)
        {
            PropSelectedCar = car;

            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
            DeleteCommand = new RelayCommand(Delete);
        }

        private void Save(object parameter)
        {
            if (string.IsNullOrWhiteSpace(PropSelectedCar.Brand))
            {
                MessageBox.Show("Pole 'Marka' nie może być puste.");
                return;
            }

            if (string.IsNullOrWhiteSpace(PropSelectedCar.Model))
            {
                MessageBox.Show("Pole 'Model' nie może być puste.");
                return;
            }

            if (PropSelectedCar.Year < 1886 || PropSelectedCar.Year > DateTime.Now.Year)
            {
                MessageBox.Show("Pole 'Rok produkcji' musi być wartością między 1886 a bieżącym rokiem.");
                return;
            }

            if (string.IsNullOrWhiteSpace(PropSelectedCar.OwnerName))
            {
                MessageBox.Show("Pole 'Imię' nie może być puste.");
                return;
            }

            if (string.IsNullOrWhiteSpace(PropSelectedCar.OwnerLastName))
            {
                MessageBox.Show("Pole 'Nazwisko' nie może być puste.");
                return;
            }

            try
            {
                using (var context = new WorkshopContext())
                {
                    var carToUpdate = context.Cars.Find(PropSelectedCar.Id);
                    if (carToUpdate != null)
                    {
                        carToUpdate.Brand = PropSelectedCar.Brand;
                        carToUpdate.Model = PropSelectedCar.Model;
                        carToUpdate.Year = PropSelectedCar.Year;
                        carToUpdate.OwnerName = PropSelectedCar.OwnerName;
                        carToUpdate.OwnerLastName = PropSelectedCar.OwnerLastName;

                        context.SaveChanges();
                        MessageBox.Show("Zaktualizowano zgłoszenie!");
                    }
                }
                CloseWindow(parameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił nie oczekiwany błąd: {ex.Message}");
            }
        }

        private void Cancel(object parameter)
        {
            CloseWindow(parameter);
        }

        private void Delete(object parameter)
        {
            var result = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Potwierdzenie usunięcia", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new WorkshopContext())
                    {
                        var carToDelete = context.Cars.Find(PropSelectedCar.Id);
                        if (carToDelete != null)
                        {
                            context.Cars.Remove(carToDelete);
                            context.SaveChanges();
                            MessageBox.Show("Rekord został usunięty.");
                        }
                    }
                    CloseWindow(parameter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił nie oczekiwany błąd: {ex.Message}");
                }
            }
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
