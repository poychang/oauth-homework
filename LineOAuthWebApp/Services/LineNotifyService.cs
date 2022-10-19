using LineOAuthWebApp.Models;
using System.Text.Json;

namespace LineOAuthWebApp.Services
{
    public class LineNotifyService
    {
        private HttpClient _httpClient;

        public LineNotifyService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(LineNotifyService));
        }

        public string LineNotifyAuthorizeUrl(string clientId, string redirectUri, string state)
        {
            return $"https://notify-bot.line.me/oauth/authorize"
                + $"?response_type=code"
                + $"&client_id={clientId}"
                + $"&redirect_uri={redirectUri}"
                + $"&scope=notify"
                + $"&state={state}";
        }

        public async Task<string> FetchLineNotifyAccessTokenAsync(string code, string clientId, string clientSecret, string returnUri)
        {
            var endpoint = "https://notify-bot.line.me/oauth/token";
            var payload = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "redirect_uri", returnUri },
                { "client_id", clientId },
                { "client_secret", clientSecret }
            });
            var response = await _httpClient.PostAsync(endpoint, payload);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();

            return JsonSerializer.Deserialize<LineNotifyAccessToken>(responseStream)?.AccessToken ?? string.Empty;
        }

        public async Task<bool> SendMessageAsync(string accessToken, string message)
        {
            var endpoint = "https://notify-api.line.me/api/notify";
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "message", message }
            });
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return true;
        }

        public async Task RevokeAccessTokenAsync(string accessToken, Action postAction = default!)
        {
            var endpoint = "https://notify-api.line.me/api/revoke";
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            if (postAction is not null) postAction();
        }
    }
}
