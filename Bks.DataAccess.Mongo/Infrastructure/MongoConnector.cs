using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


//Nuget: Bks.DataAccess.Mongo
namespace Bks.DataAccess.Mongo.Infrastructure
{
    public class MongoConnector
    {
        private readonly string collectionPrefix;
        private readonly IMongoDatabase database;

        public MongoConnector(
            IOptions<MongoSettings> settings,
            ILogger<MongoConnector> logger)
        {
            var config = settings.Value;
            this.collectionPrefix = config.CollectionPrefix;

            
            var client = new MongoClient(config.ConnectionString);

            //used for POC testing!!!
            client.DropDatabase(config.Database);

            this.database = client.GetDatabase(config.Database);
        }
        
        public IMongoCollection<TDocument> GetCollection<TDocument>(string name)
        {
            var mongoCollection = database.GetCollection<TDocument>($"{collectionPrefix}_{name}");
            return mongoCollection;
        }
    }
}