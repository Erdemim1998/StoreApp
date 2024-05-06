using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StoreApp.Data.Concrete
{
    public class Comment
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Yorum alanı girmek zorunlu!")]
        [JsonPropertyName("commentText")]
        public string CommentText { get; set; } = null!;

        [JsonPropertyName("commentDate")]
        public DateTime CommentDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(ProductId))]
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("product")]
        public Product Product { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        [JsonPropertyName("userId")]
        public string UserId { get; set; } = null!;

        [JsonPropertyName("user")]
        public AppUser User { get; set; } = null!;
    }
}
