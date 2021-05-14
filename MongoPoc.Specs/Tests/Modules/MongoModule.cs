using System;
using System.Threading.Tasks;
using Attest.Testing.Contracts;
using JetBrains.Annotations;
using MongoDB.Driver;

namespace MongoPoc.Specs.Tests.Modules
{
    [UsedImplicitly]
    internal sealed class MongoModule : IStaticApplicationModule
    {
        private readonly IProcessManagementService processManagementService;
        private readonly IApplicationPathInfo mongoApplicationPathInfo;
        private const string DockerImageName = "mongodb-integration-tests-sample";
        public MongoModule(IProcessManagementService processManagementService)
        {
            this.processManagementService = processManagementService;
            // ReSharper disable once ArrangeThisQualifier
            this.mongoApplicationPathInfo = new MongoApplicationPathInfo();
        }

        public string Id => "Mongo instance";
        public string RelativePath => mongoApplicationPathInfo.RelativePath;


        public int Start()
        {
            return processManagementService.Start("docker-compose",
                $"up -d");
        }

        public void Stop(int handle)
        {
            var stopProcessId = processManagementService.Start(
                "docker",
                $"stop {DockerImageName}");
            //wait for docker stop to finish
            Task.Delay(TimeSpan.FromSeconds(2)).Wait();
            //close open windows
            processManagementService.Stop(stopProcessId);
            processManagementService.Stop(handle);
        }
    }
}
