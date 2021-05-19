using System.Reflection;
using Bks.Common.IoC.Registration;
using Bks.Common.Specs.Abstractions.FileUpload;
using Bks.Common.Specs.Infra.FileUpload;
using JetBrains.Annotations;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace MongoPoc.Specs.Tests.Infra.Context
{
    [UsedImplicitly]
    internal sealed class Module : ICompositionModule<IDependencyRegistrator>
    {
        public void RegisterModule(IDependencyRegistrator dependencyRegistrator)
        {
            var assemblies = new[] { Assembly.GetExecutingAssembly() };
            dependencyRegistrator.RegisterScenarioDataStoresAsContracts(assemblies);
            dependencyRegistrator.RegisterSingleton<IFileUploadResultDataStore, FileUploadResultDataStore>();
        }
    }
}
