using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Forge.Client.Models;
using Microsoft.Extensions.Configuration;

namespace Forge.Client.Tests
{
    public static class TestHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets(typeof(TestHelper).Assembly)
                .AddEnvironmentVariables()
                .Build();
        }

        public static ForgeClientArgs GetClientArgs()
        {
            var configuration = new ForgeClientArgs();

            var iConfig = GetIConfigurationRoot();

            iConfig.Bind("Forge", configuration);

            return configuration;
        }
    }
}
