using RestSharp;

namespace MongoPoc.Specs.Tests.Contracts.Context
{
    public interface IMongoPocScenarioDataStore
    {
        public IRestResponse AddCountryResponse { get; set; }
    }
}
