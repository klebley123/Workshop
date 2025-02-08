using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Workshop.Models;
using Newtonsoft.Json;

namespace Workshop.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {                                                                         //44385
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:44384/api/") };
        }

        public async Task<List<Car>> GetCarsAsync()
        {
            var response = await _httpClient.GetAsync("cars"); // Wysłanie żądania GET do API

            if (!response.IsSuccessStatusCode) // Sprawdzenie, czy serwer zwrócił poprawny status (200 OK)
            {
                var error = await response.Content.ReadAsStringAsync(); // Pobranie treści błędu
                throw new Exception($"Błąd API {response.StatusCode}: {error}"); // Rzucenie wyjątku z pełnym błędem
            }

            return await response.Content.ReadFromJsonAsync<List<Car>>(); // Zwrot listy samochodów
        }

        public async Task DeleteCarAsync(int carId)
        {
            var response = await _httpClient.DeleteAsync($"cars/{carId}");
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Błąd API {response.StatusCode}: {error}");
            }
        }

        public async Task<HttpResponseMessage> DeleteServiceOrderAsync(int orderId)
        {
            return await _httpClient.DeleteAsync($"serviceorders/{orderId}");
        }


        public async Task<List<ServiceOrder>> GetServiceOrdersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ServiceOrder>>("serviceorders");
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var loginData = new
            {
                Username = username,
                Password = password
            };

            var json = JsonConvert.SerializeObject(loginData);

            // 🔍 Debugowanie: Sprawdzenie JSON-a i adresu API
            Console.WriteLine($"Wysyłany JSON: {json}");
            Console.WriteLine($"Łączę się z: {_httpClient.BaseAddress}auth/login");

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Błąd logowania: {response.StatusCode} - {error}");
            }

            return await response.Content.ReadAsStringAsync(); // Powinien zwrócić token JWT
        }



    }
}
