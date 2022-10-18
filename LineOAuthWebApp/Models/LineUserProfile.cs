using System.Text.Json.Serialization;

namespace LineOAuthWebApp.Models
{
    public class LineUserProfile
    {

        [JsonPropertyName("sub")]
        public string Sub { get; set; } = string.Empty;

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; } = string.Empty;

        [JsonPropertyName("pictureUrl")]
        public string PictureUrl { get; set; } = string.Empty;

        [JsonPropertyName("statusMessage")]
        public string StatusMessage { get; set; } = string.Empty;
    }
}
