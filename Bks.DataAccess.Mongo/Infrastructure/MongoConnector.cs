using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver.Core.Events;


namespace Bks.DataAccess.Mongo.Infrastructure
{
    public abstract class MongoConnector
    {
        private readonly string collectionPrefix;
        private readonly IMongoDatabase database;
        
        protected MongoConnector(
            IOptions<MongoSettings> settings,
            ILogger<MongoConnector> logger,
            IReadOnlyCollection<IMongoClassMapper> classMaps)
        {
            var config = settings.Value;

            this.collectionPrefix = config.CollectionPrefix;
            
            var clientSettings = BuildSettings(logger, config);
            var client = new MongoClient(clientSettings);

            //used for POC testing!!!
            client.DropDatabase(config.Database);

            this.database = client.GetDatabase(config.Database);
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>(string name)
        {
            var mongoCollection = database.GetCollection<TDocument>($"{collectionPrefix}_{name}");
            return mongoCollection;
        }

        protected static void RegisterClassMaps(IReadOnlyCollection<IMongoClassMapper> classMaps)
        {
            foreach (var map  in classMaps)
            {
                map.Execute();
            }
        }

        private static MongoClientSettings BuildSettings(ILogger<MongoConnector> logger, MongoSettings config)
        {
            var url = new MongoUrl(config.ConnectionString);
            var clientSettings = MongoClientSettings.FromUrl(url);
            clientSettings.ClusterConfigurator = BuildCommandLogger(logger);
            return clientSettings;
        }

        private static Action<ClusterBuilder> BuildCommandLogger(ILogger<MongoConnector> logger)
        {
            //TODO: Provide logger factory that allows dedicated filters per command
            return cb => cb.Subscribe<CommandStartedEvent>(e =>
            {
                logger.LogDebug($"Executed command - {e.CommandName} - {e.Command.ToJson()}");
            });
        }
    }
}