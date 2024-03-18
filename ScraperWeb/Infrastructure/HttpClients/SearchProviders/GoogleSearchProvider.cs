using Application.Common.Interfaces;
using Application.Settings;
using Infrastructure.HttpClients.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.HttpClients.External
{
    public class GoogleSearchProvider : HttpClientBase, ISearchProvider
    {
        private readonly SearchSettings _searchSettings;

        public GoogleSearchProvider(HttpClient searchClient, IOptions<SearchSettings> searchSettings) : base(searchClient)
        {
            _searchSettings = searchSettings?.Value ?? throw new ArgumentNullException(nameof(searchSettings));
            SetClientBaseUrl(_searchSettings.BaseUrl);
        }

        public async Task<string> GetSearchResponse(string keywords)
        {
            using var client = Client;

            var keywordsQueryString = $"search?num={Constants.NUMBER_OF_RESULTS}&q={string.Join('+', keywords.Split(' '))}";
            var response = await client.GetAsync(keywordsQueryString);
           
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
