using Microsoft.Extensions.Configuration;

namespace EBX.Test.AcceptanceTests
{

    public static class AppSettings
    {
        public static IConfigurationRoot Read() => 
            TestHelpers.TestUtilities.AppSettings.Read();
    }
}
