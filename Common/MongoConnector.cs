using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoPocWebApplication1.Controllers;

namespace MongoPocWebApplication1.Common
{
    public class MongoConnector : IMongoConnector
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

        private readonly IEnumerable<IMongoRepository> mongoRepositories;

        private readonly ILogger<MongoConnector> logger;

        //TODO: Pass MongoConfig
        public MongoConnector(IMongoConfiguratio configuration, ILogger<MongoConnector> logger)
        {
            this.logger = logger;
            this.mongoRepositories = mongoRepositories;
            logger.LogInformation("-->MongoConnector");

            //TODO: Pass connection in ctor
            var connectionString = BuildConnectionString(configuration);
            logger.LogDebug($" Creating Mongo Client for connection string: {connectionString}");
            Client = new MongoClient(connectionString);


            UpdateDomainName(configuration);

            //TODO: Pass db name in the constructor as string or pass MongoDbOptions{coonnesctionString, Team, UserName, Domain, Environment?}
            var dbInstanceName = BuildInstanceName(configuration);

            //used for POC testing!!!
            logger.LogInformation($" Drop old DB Instance {dbInstanceName}");
            Client.DropDatabase(dbInstanceName);

            logger.LogInformation($" Creating DB Instance {dbInstanceName}");
            this.Database = this.Client.GetDatabase(dbInstanceName);
            logger.LogInformation("<--MongoConnector");
        }

        public void Init(IConfiguration configuration)
        {
            foreach (var repo in mongoRepositories)
            {
                repo.RegisterClassMapAndInit(this);
            }
        }

        private static string BuildConnectionString(IConfiguration configuration)
        {
            MongoCluster mongoCluster = configuration.GetSection(MongoCluster.SectionName).Get<MongoCluster>();
            var dbConnectionString = $"mongodb://{mongoCluster.Host}:{mongoCluster.Port}?maxPoolSize={mongoCluster.MaxPoolSize}";
            return dbConnectionString;
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

        public IMongoCollection<TDocument> GetCollection<TDocument>(string collectionName, MongoCollectionSettings settings = null)
        {
            logger.LogDebug($"-->GetCollection {collectionName}");
            var mongoCollection = this.Database.GetCollection<TDocument>($"{DomainName}_{collectionName}", settings);
            logger.LogDebug("<--GetCollection");
            return mongoCollection;
        }
    }
}