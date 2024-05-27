using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Data.Concrete
{
    public class AppRole : IdentityRole
    {
        [Required]
        [JsonPropertyName("id")]
        public override string? Id { get; set; }

        [Required(ErrorMessage = "Rol Adı bilgisi zorunlu.")]
        [JsonPropertyName("name")]
        [Display(Name = "Rol Adı *")]
        public override string? Name { get => base.Name; set => base.Name = value; }
    }
}
