using Xunit.Abstractions;
using System.Net.Http;
using FluentAssertions;
using System.Net;
using System.Diagnostics;
using System.Threading.Tasks;
using EBX.Test.AcceptanceTests.TestFixture.Types;

namespace EBX.TestHelpers.TestUtilities
{
    public class HttpRequestSender
    {
        private readonly ITestOutputHelper _testOutput;
        public HttpResponseMessage? ResponseMessage { get; set; }

        public HttpRequestSender(ITestOutputHelper testOutput)
        {
            _testOutput = testOutput;
        }


        public async Task I_send_a_get_request(Stopwatch stopwatch, AuthenticatedHttpClient _client, string endpoint, bool withToken)
        {
            _testOutput.WriteLine($"When I post a get request to {endpoint}");

            ResponseMessage = await _client.GetAsync(endpoint, withToken);

            _testOutput.WriteLine($"Then a get request is sent to {endpoint} after {stopwatch.ElapsedMilliseconds}ms");

            var responseMessage = await ResponseMessage.Content.ReadAsStringAsync();
            _testOutput.WriteLine($"ResponseMessage: {responseMessage}");
        }

        public void The_response_should_have_the_correct_HttpStatusCode(HttpStatusCode httpStatusCode)
        {
            _testOutput.WriteLine($"Then the response should have the correct HTTP status code: {httpStatusCode}");
            ResponseMessage!.StatusCode.Should().Be(httpStatusCode);
        }
    }
}
