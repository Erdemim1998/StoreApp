using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Data.Concrete
{
    public class AppUser : IdentityUser
    {
        [JsonPropertyName("id")]
        public override string Id { get; set; } = null!;

        [Required(ErrorMessage = "Adı Soyadı bilgisi zorunlu.")]
        [JsonPropertyName("fullName")]
        [Display(Name = "Adı Soyadı *")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Resim bilgisi zorunlu.")]
        [JsonPropertyName("image")]
        [Display(Name = "Resim *")]
        public string Image { get; set; } = null!;

        [Required(ErrorMessage = "Kullanıcı Adı bilgisi zorunlu.")]
        [JsonPropertyName("userName")]
        [Display(Name = "Kullanıcı Adı *")]
        public override string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Email bilgisi zorunlu.")]
        [JsonPropertyName("email")]
        [EmailAddress]
        [Display(Name = "Email *")]
        public override string Email { get; set; } = null!;

        [JsonPropertyName("dateAdded")]
        public DateTime DateAdded { get; set; } = DateTime.Now;

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
