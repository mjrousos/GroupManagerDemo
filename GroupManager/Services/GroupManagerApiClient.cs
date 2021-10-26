using GroupManager.Models;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GroupManager.Services
{
    public class GroupManagerApiClient : IGroupManagerApiClient
    {
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly HttpClient _httpClient;
        private readonly GroupManagerApiOptions _options;

        public GroupManagerApiClient(ITokenAcquisition tokenAcquisition, HttpClient httpClient, IOptions<GroupManagerApiOptions> options)
        {
            _tokenAcquisition = tokenAcquisition;
            _options = options.Value;

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_options.ApiBaseUrl);
        }

        public Task<string> GetCodeName() => GetAsync<string>("CodeName");

        private async Task<string> GetAsync<T>(string url)
        {
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(_options.Scopes).ConfigureAwait(false);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync(url).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
            }

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        //    return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync().ConfigureAwait(false)).ConfigureAwait(false);
        }
    }
}
