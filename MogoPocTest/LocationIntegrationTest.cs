using System.Threading.Tasks;
using MongoPocWebApplication1.ControllersPresentationAndApplication;
using MongoPocWebApplication1.Domain.Entities;
using MongoPocWebApplication1.Infrastructure.Mongo.Repositories;
using Xunit;

namespace MogoPocTest
{
    public class LocationIntegrationTest
    {
        [Fact]
        public async Task CreateCountry()
        {
            using LocationDbFixture locationDb = new LocationDbFixture();
            var countryController = new CountryController(new CountryRepository(locationDb.LocationMongoConnectorProp));
            var country = new Country("1", "Israel");

            var result = await countryController.Post(country);
            Assert.NotNull(result);
        }
    }
}
