using Bks.DataAccess.Mongo;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using MongoPocWebApplication1.ControllersPresentationAndApplication;
using MongoPocWebApplication1.Domain.Entities;
using MongoPocWebApplication1.Domain.Repositories;
using Xunit;

namespace MogoPocTest
{
    public class CountryControllerUnitTest
    {
        private readonly ICountryRepository countryRepositoryMock = A.Fake<ICountryRepository>();
        private readonly ILogger<MongoConnector> mockLogger = A.Fake<ILogger<MongoConnector>>();
        
        CountryControllerUnitTest()
        {

        }
        [Fact]
        public void Post_Created_Country_In_Repo()
        {
            //Arrange 
            // A.CallTo(() => countryRepo.AddAsync(null)).Returns()
            var countryController = new CountryController(countryRepositoryMock);
            //Act
            var country = new Country("1", "Israel");
            countryController.Post(country);
            //Assert
            A.CallTo(countryRepositoryMock.AddAsync(country)).MustHaveHappened();
        }
    }
}
