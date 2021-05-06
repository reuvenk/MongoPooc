﻿using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;


//Nuget: Bks.DataAccess.Mongo
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
            
            var mongoConnectionUrl = new MongoUrl(config.ConnectionString);
            var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);
            mongoClientSettings.ClusterConfigurator = cb => {
                cb.Subscribe<CommandStartedEvent>(e => {
                    logger.LogDebug($"{e.CommandName} - {e.Command.ToJson()}");
                });
            };
            var client = new MongoClient(mongoClientSettings);

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
    }
}