using Bks.DataAccess.Mongo;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoPocWebApplication1.ControllersPresentationAndApplication;
using MongoPocWebApplication1.Domain.Entities;
using MongoPocWebApplication1.Domain.Repositories;

namespace MogoPocTest
{
    [TestClass]
    public class CountryControllerUnitTest
    {
        private readonly ICountryRepository countryRepositoryMock = A.Fake<ICountryRepository>();
        private readonly ILogger<MongoConnector> mockLogger = A.Fake<ILogger<MongoConnector>>();
        
        CountryControllerUnitTest()
        {

        }
        [TestMethod]
        public async void Post_Country_ShouldInvokeAddAsyncOnRepo()
        {
            //Arrange 
            // A.CallTo(() => countryRepo.AddAsync(null)).Returns()
            var countryController = new CountryController(countryRepositoryMock);
            //Act
            var country = new Country("1", "Israel");
            await countryController.Post(country);
            //Assert
            A.CallTo(countryRepositoryMock.AddAsync(country)).MustHaveHappened();
        }
    }
}
