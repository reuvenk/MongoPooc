using Attest.Testing.SpecFlow;
using JetBrains.Annotations;
using MongoPoc.Specs.Tests.Contracts.Context;
using RestSharp;
using TechTalk.SpecFlow;

namespace MongoPoc.Specs.Tests.Infra.Context
{
	[UsedImplicitly]
	public class MongoPocScenarioDataStore : ScenarioDataStoreBase, IMongoPocScenarioDataStore
	{
		public IRestResponse AddCountryResponse
		{
			get => GetValueImpl<IRestResponse>();
			set => SetValueImpl(value);
		}

		public MongoPocScenarioDataStore(ScenarioContext scenarioContext) 
			: base(scenarioContext)
		{
		}
	}
}
