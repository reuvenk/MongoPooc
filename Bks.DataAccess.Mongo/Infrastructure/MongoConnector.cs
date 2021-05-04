using MongoDB.Driver;


//Nuget: Bks.DataAccess.Mongo
namespace Bks.DataAccess.Mongo.Infrastructure
{
    public class MongoConnector
    {
        public MongoClient Client { get; }
        public IMongoDatabase Database { get; }
        private string CollectionPrefix { get; set; }

        public MongoConnector(string connectionString, string collectionPrefix, string database)
        {
            Client = new MongoClient(connectionString);

            CollectionPrefix = collectionPrefix;
            
            //used for POC testing!!!
            Client.DropDatabase(database);

            this.Database = Client.GetDatabase(database);
        }
        
        public IMongoCollection<TDocument> GetCollection<TDocument>(string name)
        {
            var mongoCollection = this.Database.GetCollection<TDocument>($"{CollectionPrefix}_{name}");
            return mongoCollection;
        }
    }
}