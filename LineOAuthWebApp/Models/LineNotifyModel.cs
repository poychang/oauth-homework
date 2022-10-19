using System.Text.Json.Serialization;

namespace LineOAuthWebApp.Models
{
    public class LineNotifyAccessToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;
    }
}
