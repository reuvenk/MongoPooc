using System;
using System.Data.Common;
using Attest.Testing.Contracts;
using JetBrains.Annotations;
using MongoDB.Driver;

namespace MongoPoc.Specs.Tests.Modules
{
    [UsedImplicitly]
    public sealed class WebApplicationModule : IDynamicApplicationModule
    {
        private readonly IProcessManagementService processManagementService;
        private readonly IApplicationPathInfo applicationPathInfo;
        private readonly IApplicationFacade applicationFacade;

        private int webServerProcessId;
        private string dbInstanceName;
        private const string ConnectionString = "mongodb://root:example@localhost:27018";
        public string Id => "MongoPoc";
        public string RelativePath => applicationPathInfo.RelativePath;

        public WebApplicationModule(IProcessManagementService processManagementService)
        {
            this.processManagementService = processManagementService;
            applicationPathInfo = new ApplicationPathInfo();
            applicationFacade = new ApplicationFacade(processManagementService);
        }

        public void Stop()
        {
            processManagementService.Stop(webServerProcessId);
            var client = new MongoClient("mongodb://root:example@localhost:27018");

            client.DropDatabase(dbInstanceName);
        }

        public void Start()
        {
            dbInstanceName = $"test_db_{Guid.NewGuid()}";
            webServerProcessId = processManagementService.Start("dotnet",
                $"run " +
                $"Mongo:ConnectionString={ConnectionString} " +
                $"Mongo:Database={dbInstanceName} {applicationPathInfo.Executable} " +
                $"--launch-profile MongoPocWebApplication1");
        }
    }
}
