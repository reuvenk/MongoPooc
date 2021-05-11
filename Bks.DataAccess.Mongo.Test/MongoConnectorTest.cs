using System;
using System.Collections.Generic;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Xunit;

namespace Bks.DataAccess.Mongo.Test
{
    public class MongoConnectorTest
    {
        private const string DbName = "MY_DB_NAME";
        private const string DbPrefix = "MY_DB_PREFIX";
        private const string DbCollectionName = "MY_COLLECTION_NAME";

        private readonly ILogger<MongoConnector> mockLogger = 
            A.Fake<ILogger<MongoConnector>>();
        
        private readonly IReadOnlyCollection<IMongoClassMapper> classMaps =
            A.Fake<IReadOnlyCollection<IMongoClassMapper>>();

        private static IOptions<MongoSettings> CreateMongoSettingsOptions()
        {
            MongoSettings mongoSettings = new MongoSettings()
            {
                ConnectionString = "mongodb://localhost:27017?maxPoolSize=5",
                Database = DbName,
                CollectionPrefix = DbPrefix
            };
            IOptions<MongoSettings> mongoSettingsOptions = Options.Create(mongoSettings);
            return mongoSettingsOptions;
        }

        [Fact]
        public void TestRegisterClassMap()
        {
            var mapperA = A.Fake<IMongoClassMapper>();
            var mapperB = A.Fake<IMongoClassMapper>();
            var mapperC = A.Fake<IMongoClassMapper>();
            IReadOnlyCollection<IMongoClassMapper> mockedClassMaps = new List<IMongoClassMapper>(){ mapperA , mapperB, mapperC };

            var options = CreateMongoSettingsOptions();
            var mockMongo = A.Fake<MongoConnector>(
                x => x.WithArgumentsForConstructor(
                    new object[] { options, mockLogger, mockedClassMaps }));
            A.CallTo(() => mapperA.Map()).MustHaveHappened();
            A.CallTo(() => mapperB.Map()).MustHaveHappened();
            A.CallTo(() => mapperC.Map()).MustHaveHappened();
        }

        [Fact]
        public void TestGetCollection()
        {
            var options = CreateMongoSettingsOptions();
            var mockMongo = A.Fake<MongoConnector>(
                x => x.WithArgumentsForConstructor(
                    new object[] {options, mockLogger, classMaps }));
            
            var mongoCollection = mockMongo.GetCollection<Object>(DbCollectionName);
            Assert.NotNull(mongoCollection);
            Assert.Equal(mongoCollection.CollectionNamespace.ToString(),$"{DbName}.{DbPrefix}_{DbCollectionName}");
        }
    }
}
