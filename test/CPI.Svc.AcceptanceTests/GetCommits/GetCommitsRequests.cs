using System.Net;
using System.Threading.Tasks;
using Xunit.Abstractions;
using System.Diagnostics;
using EBX.Test.AcceptanceTests.TestFixture.Types;
using EBX.TestHelpers.TestUtilities;
using System.Net.Http;
using FluentAssertions;
using System;

namespace EBX.Test.AcceptanceTests.Git
{
    public class GetCommitsRequests
    {
        private readonly AuthenticatedHttpClient _client;
        private readonly ITestOutputHelper _testOutput;

        public GetCommitsRequests(AuthenticatedHttpClient client, ITestOutputHelper testOutput)
        {
            _client = client;
            _testOutput = testOutput;
        }

        public async Task I_send_a_request_to_get_commits_and_verify_the_response_has_correct_HttpStatusCode(Stopwatch stopwatch, HttpRequestSender httpRequestSender, HttpStatusCode httpStatusCode, bool withToken)
        {
            await httpRequestSender.I_send_a_get_request(stopwatch, _client, $"/repos/jbogard/MediatR/commits", withToken);
            httpRequestSender.The_response_should_have_the_correct_HttpStatusCode(httpStatusCode);
        }

        public async Task I_verify_the_commit_name_is_that_expected(HttpResponseMessage httpResponse, string expectedName)
        {
            var response = await HttpResponseParsers.The_response_should_be_deserialized_dynamically(httpResponse, _testOutput);

            //assumption that the first node is the most recent
            string name = response[0].commit.author.name;
            name.Should().Be(expectedName, $"because we thought the commit.author.name should be {expectedName}");
            _testOutput.WriteLine($"Verified commit.author.name was {name}");
        }

        public async Task I_verify_the_commit_date_is_that_expected(HttpResponseMessage httpResponse, DateTime expectedDate)
        {
            var response = await HttpResponseParsers.The_response_should_be_deserialized_dynamically(httpResponse, _testOutput);

            //assumption that the first node is the most recent
            DateTime date = response[0].commit.author.date;
            date.Should().BeSameDateAs(expectedDate, $"because we thought the commit.author.date should be {expectedDate}");
            _testOutput.WriteLine($"Verified commit.author.date was {date}");
        }

        public async Task I_verify_the_response_body_contains_the_string_expected(HttpResponseMessage httpResponse, string expectedFailure)
        {
            var response = await HttpResponseParsers.The_response_should_be_deserialized_dynamically(httpResponse, _testOutput);

            string message = response.message;
            message.Should().Be(expectedFailure, $"because we thought the response.message should be {expectedFailure}");
            _testOutput.WriteLine($"Verified response.message was {message}");
        }
    }
}