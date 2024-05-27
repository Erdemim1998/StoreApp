using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Web.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email bilgisi zorunlu.")]
        [JsonPropertyName("email")]
        [EmailAddress]
        [Display(Name = "Email *")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Parola bilgisi zorunlu.")]
        [JsonPropertyName("password")]
        [Display(Name = "Parola *")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Parola Tekrar bilgisi zorunlu.")]
        [Compare("Password")]
        [JsonPropertyName("passwordConfirmed")]
        [Display(Name = "Parola Tekrar *")]
        public string PasswordConfirmed { get; set; } = null!;
    }
}
