using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Data.Concrete
{
    public class City
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Şehir Adı bilgisi zorunlu.")]
        [JsonPropertyName("name")]
        [Display(Name = "Şehir Adı *")]
        public string Name { get; set; } = null!;
    }
}
