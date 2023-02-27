using System;
using Xunit;

namespace EBX.Test.AcceptanceTests.Git
{
    public class GetCommitsData
    {
        public string ExpectedName { get; set; }
        public DateTime ExpectedDate { get; set; }
        public string? ExpectedFailureReason { get; set; }
    }

    public partial class GetCommitsTests
    {
        
        public static TheoryData< string, GetCommitsData> GetCommitsData =>
            new TheoryData< string, GetCommitsData>
            {
                {
                    "Valid test for verifying git commit",
                    new GetCommitsData
                    {
                        ExpectedName = "Jimmy Bogard",
                        ExpectedDate = DateTime.Parse("2023-02-15")
                    }
                },
            };

        public static TheoryData<string, GetCommitsData> GetCommitsFailureData =>
            new TheoryData<string, GetCommitsData>
            {
                {
                    "Invalid test for verifying Bad credentials",
                    new GetCommitsData
                    {
                        ExpectedFailureReason = "Bad credentials"
                    }
                },
            };
    }
}
