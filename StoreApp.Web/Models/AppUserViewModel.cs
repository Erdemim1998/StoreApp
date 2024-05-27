using StoreApp.Data.Concrete;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Web.Models
{
    public class AppUserViewModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

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
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Email bilgisi zorunlu.")]
        [JsonPropertyName("email")]
        [EmailAddress]
        [Display(Name = "Email *")]
        public string Email { get; set; } = null!;
    }
}
