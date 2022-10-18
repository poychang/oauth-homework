namespace LineOAuthWebApp.Services
{
    public class LineNotifyService
    {
        private HttpClient _httpClient;

        public LineNotifyService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(LineNotifyService));
        }
    }
}
