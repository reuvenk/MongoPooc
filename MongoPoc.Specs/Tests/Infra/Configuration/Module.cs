using System;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace MongoPoc.Specs.Tests.Infra.Configuration
{
    [UsedImplicitly]
    internal sealed class Module : ICompositionModule<IDependencyRegistrator>
    {
        private static object syncObject = new object();
        public void RegisterModule(IDependencyRegistrator dependencyRegistrator)
        {
            lock (syncObject)
            {
                var allSettingsFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "appsettings.*.json");
                var envSpecificSettingsFiles = allSettingsFiles.Where(t => !t.Contains("Personal")).ToArray();
                if (envSpecificSettingsFiles.Length != 1)
                {
                    throw new Exception(
                        $"Expect to have exactly one env-specific settings file, but found {string.Join(",", envSpecificSettingsFiles)}.");
                }

                var configurationBuilder = new ConfigurationBuilder()
                    .AddEnvironmentVariables();
                foreach (var settingsFile in allSettingsFiles)
                {
                    configurationBuilder.AddJsonFile(settingsFile);
                }

                dependencyRegistrator.AddSingleton<IConfiguration>(() => configurationBuilder.Build());
            }
        }

        
    }
}
