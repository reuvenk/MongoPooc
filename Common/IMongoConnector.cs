using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MongoPocWebApplication1.Common
{
    public interface IMongoConnector
    {
        void Init(IConfiguration configuration);
        IMongoCollection<TDocument> GetCollection<TDocument>(IMongoRepository mongoRepository, MongoCollectionSettings settings = null);
    }
}