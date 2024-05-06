using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [Required]
        [JsonPropertyName("password")]
        [Display(Name = "Parola")]
        public string Password { get; set; } = null!;
    }
}
