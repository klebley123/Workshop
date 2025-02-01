using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Workshop.Models;

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

        public async Task<List<ServiceOrder>> GetServiceOrdersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ServiceOrder>>("serviceorders");
        }
    }
}
