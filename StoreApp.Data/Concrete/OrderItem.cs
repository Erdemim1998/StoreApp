using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StoreApp.Data.Concrete
{
    public class OrderItem
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("orderDate")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(UserId))]
        [JsonPropertyName("userId")]
        [Display(Name = "Kullanıcı")]
        public string UserId { get; set; } = null!;

        [JsonPropertyName("user")]
        public AppUser User { get; set; } = null!;

        [ForeignKey(nameof(CityId))]
        [JsonPropertyName("cityId")]
        [Display(Name = "İl")]
        public int CityId { get; set; }

        [JsonPropertyName("city")]
        public City City { get; set; } = null!;

        [Phone]
        [JsonPropertyName("phoneNumber")]
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; } = null!;

        [EmailAddress]
        [JsonPropertyName("email")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("address")]
        [Display(Name = "Adres")]
        public string Address { get; set; } = null!;

        [JsonPropertyName("cartName")]
        [Display(Name = "Kart İsmi")]
        public string CartName { get; set; } = null!;

        [JsonPropertyName("cartNumber")]
        [Display(Name = "Kart Numarası")]
        public string CartNumber { get; set; } = null!;

        [JsonPropertyName("expirationMonth")]
        [Display(Name = "Kart Bitiş Ayı")]
        public string ExpirationMonth { get; set; } = null!;

        [JsonPropertyName("expirationYear")]
        [Display(Name = "Kart Bitiş Yılı")]
        public string ExpirationYear { get; set; } = null!;

        [JsonPropertyName("cvc")]
        public string Cvc { get; set; } = null!;
    }
}
