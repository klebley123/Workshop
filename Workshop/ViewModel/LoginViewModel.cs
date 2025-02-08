using System.Windows;
using System.Windows.Input;
using Workshop.Command;
using Workshop.Services;
using System.Threading.Tasks;
using System;
using System.ComponentModel;
using Workshop.Views;
using System.Linq;

namespace Workshop.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public LoginViewModel(ApiService apiService)
        {
            _apiService = apiService;
            LoginCommand = new RelayCommand(async param => await Login());
        }

        private async Task Login()
        {
            try
            {
                Console.WriteLine($"Username: {Username}, Password: {Password}");
                string token = await _apiService.LoginAsync(Username, Password);
                MessageBox.Show("Zalogowano pomyślnie!");

                // Zamknij `LoginView` z wynikiem `true`
                Window loginWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is LoginView);
                if (loginWindow != null)
                {
                    loginWindow.DialogResult = true; // ✅ Informujemy `ShowDialog()`, że logowanie było udane
                    Console.WriteLine("DialogResult ustawione na TRUE");
                    loginWindow.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd logowania: {ex.Message}");
            }
        }
    }
}
