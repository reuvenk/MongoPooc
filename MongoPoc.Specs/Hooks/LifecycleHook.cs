using Attest.Testing.Contracts;
using Attest.Testing.Core;
using Bks.Common.Specs.Bootstrapping;
using BoDi;
using JetBrains.Annotations;
using Solid.Bootstrapping;
using Solid.Common;
using Solid.Core;
using Solid.Extensibility;
using Solid.IoC.Adapters.BoDi;
using Solid.Practices.IoC;
using Solid.Practices.Middleware;
using TechTalk.SpecFlow;
using BootstrapperBase = Attest.Testing.Bootstrapping.BootstrapperBase;

namespace MongoPoc.Specs.Hooks
{
    /// <summary>
    ///     This hook initalises the inversion-of-control (dependency-inversion) container of the framework
    ///     And executes the whole lifecycle for the container and application modules.
    ///     After the initialisation is finished you can author your own registrations and easily plug them into the container.
    ///     The code below contains the bare minimum of the functionality and shouldn't be modified.
    /// </summary>
    [Binding]
    [UsedImplicitly]
    internal sealed class LifecycleHook
    {
        private static ILifecycleService lifecycleService;
        private readonly ObjectContainerAdapter iocContainer;

        public LifecycleHook(ObjectContainer objectContainer)
        {
            // ReSharper disable once ArrangeThisQualifier
            this.iocContainer = new ObjectContainerAdapter(objectContainer);
        }

        [BeforeTestRun]
        [UsedImplicitly]
        public static void BeforeAllScenarios()
        {
            PlatformProvider.Current = new NetStandardPlatformProvider();
        }

        [BeforeScenario]
        [UsedImplicitly]
        public void BeforeScenario()
        {
            var startup = new Startup(iocContainer);
            startup.Initialize();
            lifecycleService = iocContainer.Resolve<ILifecycleService>();
            lifecycleService.Setup();
        }

        [AfterScenario]
        [UsedImplicitly]
        public void AfterScenario()
        {
            iocContainer.Teardown();
        }

        [AfterTestRun]
        public static void AfterAllScenarios()
        {
            lifecycleService.Teardown();
        }
    }

    /// <summary>
    ///     This is an auxiliary class used during inversion-of-control container initialisation process.
    ///     It has no usage outside this process and doesn't need to be extended right now.
    /// </summary>
    internal class Startup : IInitializable
    {
        private readonly IIocContainer iocContainer;

        public Startup(IIocContainer iocContainer)
        {
            this.iocContainer = iocContainer;
        }

        public void Initialize()
        {
            var bootstrapper = new Bootstrapper(iocContainer);
            bootstrapper
                .Use(new RegisterCustomCompositionModulesMiddleware<BootstrapperBase, IDependencyRegistrator>())
                .Use(new RegisterResolverMiddleware<BootstrapperBase>(iocContainer))
                .Use(new UseLifecycleMiddleware<BootstrapperBase>());
            bootstrapper.Use(new RegisterRequestFactoryMiddleware());
            bootstrapper.Use(new RegisterRestClientFactoryMiddleware());
            bootstrapper.Initialize();
        }
    }

    /// <summary>
    ///     This is an auxiliary class used during inversion-of-control initialisation process.
    ///     It has no usage outside this process and doesn't need to be extended right now.
    /// </summary>
    internal sealed class Bootstrapper : BootstrapperBase, IExtensible<IHaveRegistrator>
    {
        private readonly ExtensibilityAspect<IHaveRegistrator> registratorExtensibilityAspect;

        public Bootstrapper(IDependencyRegistrator dependencyRegistrator) : base(dependencyRegistrator)
        {
            // ReSharper disable once ArrangeThisQualifier
            this.registratorExtensibilityAspect = new ExtensibilityAspect<IHaveRegistrator>(this);
            // ReSharper disable once ArrangeThisQualifier
            UseAspect(this.registratorExtensibilityAspect);
        }

        public IHaveRegistrator Use(IMiddleware<IHaveRegistrator> middleware)
        {
            return registratorExtensibilityAspect.Use(middleware);
        }
    }
}
