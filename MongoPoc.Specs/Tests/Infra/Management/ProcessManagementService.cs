using System;
using Attest.Testing.Management;
using JetBrains.Annotations;
using Polly;

namespace Bks.Grading.Specs.Tests.Infra.Management
{
    //TODO: Put to Common
    [UsedImplicitly]
    internal sealed class ProcessManagementService : WindowsProcessManagementService
    {
        protected override void RetryStop(Action stopAction)
        {
            //TODO: Make configurable
            Policy.Handle<Exception>()
                .WaitAndRetry(20, r => TimeSpan.FromMilliseconds(200)).Execute(stopAction);
        }
    }
}
