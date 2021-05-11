using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoPocWebApplication1.ControllersPresentationAndApplication;
using MongoPocWebApplication1.Domain.Entities;
using MongoPocWebApplication1.Infrastructure.Mongo.Repositories;

namespace MogoPocTest
{
    [TestClass]
    public class LocationIntegrationTest
    {
        [TestMethod]
        public async Task Post_Country_ResultWithCreatedCountryId()
        {
            using LocationDbFixture locationDb = new LocationDbFixture();
            var countryController = new CountryController(new CountryRepository(locationDb.LocationMongoConnectorProp));
            var country = new Country("1", "Israel");

            var result = await countryController.Post(country);
            result.Value
                .Should()
                .BeEquivalentTo("1");
        }
    }
}
