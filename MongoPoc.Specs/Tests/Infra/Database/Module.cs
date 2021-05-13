using Bks.Common.Specs.Abstractions.Database;
using Bks.Common.Specs.Infra.Database;
using JetBrains.Annotations;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Bks.Grading.Specs.Tests.Infra.Database
{
    [UsedImplicitly]
    internal sealed class Module : ICompositionModule<IDependencyRegistrator>
    {
        public void RegisterModule(IDependencyRegistrator dependencyRegistrator)
        {
            dependencyRegistrator
                .AddSingleton<IConnectionFactory, SqlDbConnectionFactory>();
        }
    }
}
