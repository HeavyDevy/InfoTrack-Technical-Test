using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace Infrastructure.HttpClients.Common
{
    public abstract class HttpClientBase
    {
        private const int _requestTimeoutSeconds = 30;
        private const bool _clearDefaultRequestHeaders = true;
        private const string _defaultMediaType = "application/json";
        private readonly Encoding _defaultEncoding = Encoding.UTF8;

        protected HttpClient Client { get; }
        protected Encoding Encoding;
        protected string MediaType;
        protected JsonSerializerSettings JsonSerializerSettings;
        protected JsonSerializerOptions JsonSerializerOptions;

        public HttpClientBase(HttpClient client, int requestTimeoutSeconds = _requestTimeoutSeconds, bool clearDefaultRequestHeaders = _clearDefaultRequestHeaders)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            Client.Timeout = new TimeSpan(0, 0, requestTimeoutSeconds);
            if (clearDefaultRequestHeaders)
            {
                this.Client.DefaultRequestHeaders.Clear();
            }

            Encoding = _defaultEncoding;
            MediaType = _defaultMediaType;
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));

            JsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            JsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        }

        protected void SetClientBaseUrl(string baseUrl) => Client.BaseAddress = new Uri(baseUrl);

        protected void SetClientHeaderAuthorization(string scheme, string parameter) => Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, parameter);

        protected void AddDefaultRequestHeaders(IEnumerable<KeyValuePair<string, string>> headers, bool clearDefaultRequestHeaders = false)
        {
            if (clearDefaultRequestHeaders)
            {
                Client.DefaultRequestHeaders.Clear();
            }

            foreach (var header in headers)
            {
                Client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        protected string JsonSerializeObject(object value, JsonSerializerSettings settings = null) => JsonConvert.SerializeObject(value, settings);

        protected StringContent StringContent(string content, Encoding encoding = null, string mediaType = null) => new StringContent(content, encoding, mediaType);
    }
}
