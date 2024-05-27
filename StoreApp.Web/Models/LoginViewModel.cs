using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email bilgisi zorunlu.")]
        [JsonPropertyName("email")]
        [Display(Name = "Email *")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Parola bilgisi zorunlu.")]
        [JsonPropertyName("password")]
        [Display(Name = "Parola *")]
        public string Password { get; set; } = null!;
    }
}
