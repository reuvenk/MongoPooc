using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Attest.Testing.Contracts;
using JetBrains.Annotations;

namespace MongoPoc.Specs.Tests.Modules
{
    [UsedImplicitly]
    internal sealed class ApplicationFacade : IApplicationFacade
    {
        private readonly IProcessManagementService processManagementService;
        private int processId;

        public ApplicationFacade(IProcessManagementService processManagementService)
        {
            this.processManagementService = processManagementService;
        }

        public void Start(string args)
        {
            processId = processManagementService.Start(
                "dotnet",
                args);
            Task.Delay(TimeSpan.FromSeconds(2)).Wait();
        }

        public void Stop()
        {
            processManagementService.Stop(processId);
        }
    }
}
