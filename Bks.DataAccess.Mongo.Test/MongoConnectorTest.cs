using System;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;

namespace Bks.DataAccess.Mongo.Test
{
    [TestClass]
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
            var mongoSettings = new MongoSettings()
            {
                ConnectionString = "mongodb://localhost:27017?maxPoolSize=5",
                Database = DbName,
                CollectionPrefix = DbPrefix
            };
            var mongoSettingsOptions = Options.Create(mongoSettings);
            return mongoSettingsOptions;
        }

        [TestMethod]
        public void RegisterClassMaps_with3Mappers_allMappersAreUsed()
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

        [TestMethod]
        public void GetCollection_withPrefix_ShouldReturnAnInstanceAccordingToNamingConventions()
        {
            var options = CreateMongoSettingsOptions();
            var mockMongo = A.Fake<MongoConnector>(
                x => x.WithArgumentsForConstructor(
                    new object[] {options, mockLogger, classMaps }));
            

            var mongoCollection = mockMongo.GetCollection<Object>(DbCollectionName);
            mongoCollection.CollectionNamespace.ToString()
                .Should()
                .BeEquivalentTo($"{DbName}.{DbPrefix}_{DbCollectionName}");
        }
    }
}
