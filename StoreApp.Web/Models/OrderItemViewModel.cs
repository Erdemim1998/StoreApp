using StoreApp.Data.Concrete;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Web.Models
{
    public class OrderItemViewModel
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("orderDate")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(UserId))]
        [JsonPropertyName("userId")]
        [Display(Name = "Kullanıcı *")]
        [Required(ErrorMessage = "Kullanıcı bilgisi zorunlu.")]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(CityId))]
        [JsonPropertyName("cityId")]
        [Display(Name = "İl *")]
        [Required(ErrorMessage = "İl bilgisi zorunlu.")]
        public int CityId { get; set; }

        [Phone]
        [JsonPropertyName("phoneNumber")]
        [Display(Name = "Telefon Numarası *")]
        [Required(ErrorMessage = "Telefon Numarası bilgisi zorunlu.")]
        public string PhoneNumber { get; set; } = null!;

        [EmailAddress]
        [JsonPropertyName("email")]
        [Display(Name = "Email *")]
        [Required(ErrorMessage = "Email bilgisi zorunlu.")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("address")]
        [Display(Name = "Adres *")]
        [Required(ErrorMessage = "Adres bilgisi zorunlu.")]
        public string Address { get; set; } = null!;

        [JsonPropertyName("cartName")]
        [Display(Name = "Kart İsmi *")]
        [Required(ErrorMessage = "Kart İsmi bilgisi zorunlu.")]
        public string CartName { get; set; } = null!;

        [JsonPropertyName("cartNumber")]
        [Display(Name = "Kart Numarası *")]
        [Required(ErrorMessage = "Kart Numarası bilgisi zorunlu.")]
        public string CartNumber { get; set; } = null!;

        [JsonPropertyName("expirationMonth")]
        [Display(Name = "Kart Bitiş Ayı *")]
        [Required(ErrorMessage = "Kart Bitiş Ayı bilgisi zorunlu.")]
        public string ExpirationMonth { get; set; } = null!;

        [JsonPropertyName("expirationYear")]
        [Display(Name = "Kart Bitiş Yılı *")]
        [Required(ErrorMessage = "Kart Bitiş Yılı bilgisi zorunlu.")]
        public string ExpirationYear { get; set; } = null!;

        [JsonPropertyName("cvc")]
        [Display(Name = "Cvc *")]
        [Required(ErrorMessage = "Cvc kodu bilgisi zorunlu.")]
        public string Cvc { get; set; } = null!;
    }
}
