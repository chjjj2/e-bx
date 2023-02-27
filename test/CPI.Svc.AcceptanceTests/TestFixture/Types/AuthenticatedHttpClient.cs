using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EBX.Test.AcceptanceTests.TestFixture.Types
{
    public class AuthenticatedHttpClient
    {
        private readonly HttpClient _client;

        public AuthenticatedHttpClient(string baseAddress)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
        }

        private async Task SetHeaders(bool withToken)
        {
           // if (withToken)
             //   _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "EnterTokenHere");
            _client.DefaultRequestHeaders.Add("User-Agent", "chjjj2");
        }

        public async Task<HttpResponseMessage> GetAsync(string url, bool withToken)
        {
            await SetHeaders(withToken);
            return await _client.GetAsync(url);
        }
    }
}
