using Microsoft.Extensions.Configuration;
using System;

namespace EBX.TestHelpers.TestUtilities
{
    public static class AppSettings
    {
        public static IConfigurationRoot Read()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .Build();
        }
    }
}
