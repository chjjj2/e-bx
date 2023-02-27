using System;
using System.Threading.Tasks;
using Xunit.Abstractions;
using System.Net.Http;
using Newtonsoft.Json;

namespace EBX.TestHelpers.TestUtilities
{
    public class HttpResponseParsers
    {
        public static async Task<dynamic> The_response_should_be_deserialized_dynamically(HttpResponseMessage response, ITestOutputHelper testOutput)
        {
            var content = await response.Content.ReadAsStringAsync();

            dynamic deserializedResponse = JsonConvert.DeserializeObject<dynamic>(content);

            if (deserializedResponse == null)
            {
                testOutput.WriteLine($"Failed to deserialise response");
                throw new ArgumentNullException(nameof(response));
            }

            testOutput.WriteLine($"Response was deserialized");
            return deserializedResponse;
        }
    }
}
