using MongoDB.Driver;


//Nuget: Bks.DataAccess.Mongo
namespace Bks.DataAccess.Mongo.Infrastructure
{
    public class MongoConnector
    {
        private readonly string collectionPrefix;
        private readonly IMongoDatabase database;

        //TODO: consider using a private variable
        private readonly MongoClient client;

        public MongoConnector(string connectionString, string collectionPrefix, string database)
        {
            this.client = new MongoClient(connectionString);
            this.collectionPrefix = collectionPrefix;
            
            //used for POC testing!!!
            client.DropDatabase(database);

            this.database = client.GetDatabase(database);
        }
        
        public IMongoCollection<TDocument> GetCollection<TDocument>(string name)
        {
            var mongoCollection = database.GetCollection<TDocument>($"{collectionPrefix}_{name}");
            return mongoCollection;
        }
    }
}