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
            processManagementService.Start("docker",
                $"stop mongodb-integration-tests-sample");

            processManagementService.Stop(handle);
        }
    }
}
