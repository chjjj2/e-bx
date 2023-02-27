using EBX.Test.AcceptanceTests.TestFixture.Types;
using Microsoft.Extensions.Configuration;

namespace EBX.Test.AcceptanceTests
{
    public class HttpClientFixture
    {
        public HttpClientFixture(IConfiguration config)
        {
            _config = config;

            HttpClient = new AuthenticatedHttpClient(_config["BaseUrl"]);
        }

        private readonly IConfiguration _config;

        public virtual AuthenticatedHttpClient HttpClient { get; private set; } 
    }
}
