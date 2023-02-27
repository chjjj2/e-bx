using EBX.TestHelpers.TestUtilities;
using System.Net;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;
using Xunit.Abstractions;

namespace EBX.Test.AcceptanceTests.Git
{
    public partial class GetCommitsTests
    {
        public GetCommitsTests(ITestOutputHelper testOutputHelper)
        {
            var config = AppSettings.Read();
            var httpClientFixture = new HttpClientFixture(config);
            var getRateReq = new GetCommitsRequests(httpClientFixture.HttpClient, testOutputHelper);
            var httpRequestSender = new HttpRequestSender(testOutputHelper);

            _testOutput = testOutputHelper;
            _getCommitsRequests = getRateReq;
            _httpRequestSender = httpRequestSender;
        }

        private readonly ITestOutputHelper _testOutput;
        private readonly GetCommitsRequests _getCommitsRequests;
        public readonly HttpRequestSender _httpRequestSender;

        [Theory]
        [MemberData(nameof(GetCommitsData))]
        public async Task VerifyGitCommitWithToken(string testCase, GetCommitsData data)
        {
            _testOutput.WriteLine($"{testCase}");

            var stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();

            this.Given(_ => _getCommitsRequests.I_send_a_request_to_get_commits_and_verify_the_response_has_correct_HttpStatusCode(
                stopWatch, _httpRequestSender, HttpStatusCode.OK, true))
            .Then(_ => _getCommitsRequests.I_verify_the_commit_name_is_that_expected(_httpRequestSender.ResponseMessage, data.ExpectedName))
            .Then(_ => _getCommitsRequests.I_verify_the_commit_date_is_that_expected(_httpRequestSender.ResponseMessage, data.ExpectedDate))
            .BDDfy();
        }

        [Theory]
        [MemberData(nameof(GetCommitsFailureData))]
        public async Task VerifyGitCommitWithoutToken(string testCase, GetCommitsData data)
        {
            _testOutput.WriteLine($"{testCase}");

            var stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();

            this.Given(_ => _getCommitsRequests.I_send_a_request_to_get_commits_and_verify_the_response_has_correct_HttpStatusCode(
                stopWatch, _httpRequestSender, HttpStatusCode.Forbidden, false))
                .Then(_ => _getCommitsRequests.I_verify_the_response_body_contains_the_string_expected(_httpRequestSender.ResponseMessage, data.ExpectedFailureReason))
            .BDDfy();
        }
    }
}
