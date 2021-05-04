using System.Threading.Tasks;
using Bks.DataAccess.Mongo.Infrastructure;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoPocWebApplication1.Domain.Models;
using MongoPocWebApplication1.Domain.RepositoryInterfaces;
using MongoPocWebApplication1.Infrastructure.Mongo.EntityConfigurations;

namespace MongoPocWebApplication1.Infrastructure.Mongo.Repositories
{
    public class CityRepository : ICityRepository
    {
        private IMongoCollection<City> CityCollection { get; set; }
        private MongoConnector MongoConnector { get; set; }

        public string ModelName => "City";
        
        public CityRepository(MongoConnector mongoConnector)
        {
            MongoConnector = mongoConnector;
            CityMap.ConfigureClassMap();
            //init
            CityCollection = MongoConnector.GetCollection<City>(ModelName.ToLower());
        }

        public async Task<City> AddAsync(City city)
        {
            await CityCollection.InsertOneAsync(city);
            return city;
        }

        public async Task<City> GetByNameAsync(string name)
        {
            var filter = Builders<City>.Filter.Eq(c => c.Name, name);
            var result = await CityCollection.Find<City>(filter).FirstOrDefaultAsync();
            return result;
        }
    }
}