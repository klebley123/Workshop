using System.ComponentModel.DataAnnotations;

namespace WorkshopWebApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; } // Hasło będzie przechowywane w postaci hash

        [Required]
        public string Role { get; set; } = "User"; // Domyślnie użytkownik
    }
}
