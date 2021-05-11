using System;
using Bks.DataAccess.Mongo;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoPocWebApplication1.Infrastructure.Mongo;

namespace MogoPocTest
{
    public class LocationDbFixture : IDisposable
    {
        private readonly ILogger<MongoConnector> mockLogger = A.Fake<ILogger<MongoConnector>>();
        private MongoSettings MongoSettings { get; }

        public LocationMongoConnector LocationMongoConnectorProp { get;}

        public LocationDbFixture()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            MongoSettings = new MongoSettings()
            {
                ConnectionString = config.GetSection("Mongo:ConnectionString").Value,
                //Database = config.GetSection("Mongo:Database").Value;
                // use a uniq DB instance Per test
                Database = $"test_db_{Guid.NewGuid()}",
                CollectionPrefix = config.GetSection("Mongo:CollectionPrefix").Value
            };
            IOptions<MongoSettings> appSettingsOptions = Options.Create(MongoSettings);
            LocationMongoConnectorProp = new LocationMongoConnector(appSettingsOptions, mockLogger);
        }

        public void Dispose()
        {
            var client = new MongoClient(MongoSettings.ConnectionString);
            client.DropDatabase(MongoSettings.Database);
        }
    }
}