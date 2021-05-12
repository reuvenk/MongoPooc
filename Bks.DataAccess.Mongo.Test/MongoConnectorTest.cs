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

        private ILogger<MongoConnector> mockLogger;
        private IOptions<MongoSettings> options;

        [TestInitialize]
        public void Init()
        {
            mockLogger = A.Fake<ILogger<MongoConnector>>();
            options = CreateMongoSettingsOptions();
        }

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
        public void Register_Class_Maps() // RegisterClassMaps_with3Mappers_allMappersAreUsed()
        {
            var mapperA = A.Fake<IMongoClassMapper>();
            var mapperB = A.Fake<IMongoClassMapper>();
            var mapperC = A.Fake<IMongoClassMapper>();
            var mockedClassMaps = new List<IMongoClassMapper>(){ mapperA , mapperB, mapperC };

            A.Fake<MongoConnector>(
                x => x.WithArgumentsForConstructor(
                    new object[] { options, mockLogger, mockedClassMaps }));
            A.CallTo(() => mapperA.Map()).MustHaveHappened();
            A.CallTo(() => mapperB.Map()).MustHaveHappened();
            A.CallTo(() => mapperC.Map()).MustHaveHappened();
        }

        [TestMethod]
        public void GetCollection()
        {
            var classMaps = A.Fake<IReadOnlyCollection<IMongoClassMapper>>();
            var mockMongo = A.Fake<MongoConnector>(
                x => x.WithArgumentsForConstructor(
                    new object[] {options, mockLogger, classMaps }));
            
            var mongoCollection = mockMongo.GetCollection<object>(DbCollectionName);
            mongoCollection.CollectionNamespace.ToString()
                .Should()
                .BeEquivalentTo($"{DbName}.{DbPrefix}_{DbCollectionName}");
        }
    }
}
