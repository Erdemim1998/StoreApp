using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Data.Concrete
{
    public class Brand
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Marka Adı bilgisi zorunlu.")]
        [Display(Name = "Marka Adı *")]
        public string Name { get; set; } = null!;
    }
}
