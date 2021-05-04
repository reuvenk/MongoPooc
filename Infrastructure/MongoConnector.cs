using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoPocWebApplication1.Domain.RepositoryInterfaces;

namespace MongoPocWebApplication1.Infrastructure
{
    public class MongoConnector
    {
        public MongoClient Client { get; }
        public IMongoDatabase Database { get; }
        private string DomainName { get; set; }
        //mongodb://[username:password@]host1[:port1][,host2[:port2],...[,hostN[:portN]]][/[database][?options]]
        //private static readonly string Port = "27017";
        //private static readonly string Host = "localhost";
        //private static readonly int MaxPoolSize = 5;

        //private static readonly string DbConnectionString = $"mongodb://{Host}:{Port}?maxPoolSize={MaxPoolSize}";
        //private static readonly string EnvName = "local";
        //private static readonly string TeamName = "superstars";
        //private static readonly string DevName = "reuvenk";
        //private static readonly string MsName = "mongo-poc";

        private readonly ILogger<MongoConnector> logger;

        public MongoConnector(ILogger<MongoConnector> logger, IOptions<MongoSettings> settings)
        {
            this.logger = logger;
            logger.LogInformation("-->MongoConnector");
            logger.LogDebug($" Creating Mongo Client for connection string: {settings.Value.ConnectionString}");
            Client = new MongoClient(settings.Value.ConnectionString);

            DomainName = settings.Value.DomainName;
            var dbInstanceName = $"{settings.Value.InstanceName}_{DomainName}";

            //used for POC testing!!!
            logger.LogInformation($" Drop old DB Instance {dbInstanceName}");
            Client.DropDatabase(dbInstanceName);

            logger.LogInformation($" Creating DB Instance {dbInstanceName}");
            this.Database = this.Client.GetDatabase(dbInstanceName);
            logger.LogInformation("<--MongoConnector");
        }

        private string BuildInstanceName(IConfiguration configuration)
        {
            MongoInstance mongoInstance = configuration.GetSection(MongoInstance.SectionName).Get<MongoInstance>();
            StringBuilder sb = new StringBuilder();
            sb.Append(mongoInstance.Environment);
            if (!String.IsNullOrEmpty(mongoInstance.Team))
            {
                sb.Append($"_{mongoInstance.Team}");
            }
            if (!String.IsNullOrEmpty(mongoInstance.Username))
            {
                sb.Append($"_{mongoInstance.Username}");
            }
            sb.Append($"_{mongoInstance.Domain}");
            return sb.ToString();
        }

        private void UpdateDomainName(IConfiguration configuration)
        {
            MongoInstance mongoInstance = configuration.GetSection(MongoInstance.SectionName).Get<MongoInstance>();
            DomainName = mongoInstance.Domain;
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>(IMongoRepository mongoRepository, MongoCollectionSettings settings = null)
        {
            logger.LogDebug($"-->GetCollection {mongoRepository.ModelName}");
            var mongoCollection = this.Database.GetCollection<TDocument>($"{DomainName}_{mongoRepository.ModelName}", settings);
            logger.LogDebug("<--GetCollection");
            return mongoCollection;
        }
    }
}