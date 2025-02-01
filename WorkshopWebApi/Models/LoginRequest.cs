using System.ComponentModel.DataAnnotations;

namespace WorkshopWebApi.Models
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; } // Surowe hasło, nie zahashowane
    }
}
