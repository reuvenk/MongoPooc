using System.Net;
using Attest.Testing.Contracts;
using Attest.Testing.Lifecycle;
using Bks.Common.Specs.Abstractions.Web;
using Bks.Common.Specs.Infra.Web.RestSharp;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using MongoPoc.Specs.Tests.Contracts.Context;
using RestSharp;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;
using IRestClient = Bks.Common.Specs.Abstractions.Web.IRestClient;

namespace MongoPoc.Specs.Steps
{
    [Binding]
    public sealed class CountrySteps
    {
        private readonly IRequestFactory requestFactory;
        private readonly IMongoPocScenarioDataStore mongoPocScenarioDataStore;
        private readonly IDependencyResolver dependencyResolver;
        private readonly IStartDynamicApplicationModuleService startDynamicApplicationModuleService;
        private readonly IRestClient restClient;

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        public CountrySteps(
            IRestClientFactory restClientFactory, 
            IConfiguration configuration, 
            IRequestFactory requestFactory,
            IMongoPocScenarioDataStore mongoPocScenarioDataStore,
            IDependencyResolver dependencyResolver,
            IStartDynamicApplicationModuleService startDynamicApplicationModuleService)
        {
            restClient = restClientFactory.Create(configuration.ToRestClientOptions());
            this.requestFactory = requestFactory;
            this.mongoPocScenarioDataStore = mongoPocScenarioDataStore;
            this.dependencyResolver = dependencyResolver;
            this.startDynamicApplicationModuleService = startDynamicApplicationModuleService;
        }

        public class AddCountryRequest
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        [When(@"I add new country with name '(.*)' and id (.*)")]
        public void WhenIAddNewCountryWithNameAndId(string name, int id)
        {
            var request = requestFactory.GetRequest(new RequestCreationOptions(){
                        Route = "/Country",
                        Method = Method.POST,
                        UseAuthorization = false,
                        Body = new MongoPoc.Specs.Steps.CountrySteps.AddCountryRequest()
                        {
                            Id = 1,
                            Name = "Israel"
                        }
                    });

            var response = restClient.Execute(request);
            mongoPocScenarioDataStore.AddCountryResponse = response;
        }

        [Then(@"the response should be (.*)")]
        public void ThenTheResponseShouldBe(int expectedResponse)
        {
            mongoPocScenarioDataStore.AddCountryResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [When(@"I start the application")]
        public void WhenIStartTheApplication()
        {
            startDynamicApplicationModuleService.StartCollection(dependencyResolver
                    .ResolveAll<IDynamicApplicationModule>());
        }
    }
}
