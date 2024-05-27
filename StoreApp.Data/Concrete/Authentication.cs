using System.Text.Json.Serialization;

namespace StoreApp.Data.Concrete
{
    public class Authentication
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("isAuthenticated")]
        public bool IsAuthenticated { get; set; }

        [JsonPropertyName("isAdmin")]
        public bool IsAdmin { get; set; }

        [JsonPropertyName("userId")]
        public string? UserId { get; set; }
    }
}
