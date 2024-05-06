using StoreApp.Data.Concrete;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Web.Models
{
    public class AppUserViewModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [Required]
        [JsonPropertyName("fullName")]
        [Display(Name = "Adı Soyadı")]
        public string FullName { get; set; } = null!;

        [Required]
        [JsonPropertyName("image")]
        [Display(Name = "Resim")]
        public string Image { get; set; } = null!;

        [Required]
        [JsonPropertyName("userName")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; } = null!;

        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;
    }
}
