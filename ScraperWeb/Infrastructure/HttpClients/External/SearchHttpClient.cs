using Application.Settings;
using Infrastructure.HttpClients.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.HttpClients.External
{
    public class SearchHttpClient : HttpClientBase
    {
        private readonly SearchSettings _searchSettings;

        public SearchHttpClient(HttpClient searchClient, IOptions<SearchSettings> searchSettings):base(searchClient) {

            _searchSettings = searchSettings?.Value ?? throw new ArgumentNullException(nameof(searchSettings));
            SetClientBaseUrl(_searchSettings.BaseUrl);
        }

        public async Task<string> GetSearchResponse(string keywords)
        {
            using var client = Client;
            var response = await client.GetAsync(keywords);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
