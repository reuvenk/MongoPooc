using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoPocWebApplication1.Domain.Entities;
using MongoPocWebApplication1.Domain.Repositories;

namespace MongoPocWebApplication1.Infrastructure.Mongo.Repositories
{
    public class CityRepository : ICityRepository
    {
        // Defines mapping for collection name - how the current model (City) would be named inside mongo as a collection
        private const string CollectionName = "city";

        private readonly IMongoCollection<City> cityCollection;

        public CityRepository(LocationMongoConnector mongoConnector)
        {
            if (mongoConnector == null)
            {
                throw new ArgumentNullException(nameof(mongoConnector));
            }

            cityCollection = mongoConnector.GetCollection<City>(CollectionName);
        }
        
        public async Task AddAsync(City city)
        {
            await cityCollection.InsertOneAsync(city);
        }

        public async Task<City> GetByNameAsync(string name)
        {
            var filter = Builders<City>
                .Filter
                .Eq(c => c.Name, name);

            var result = await cityCollection
                .Find(filter)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}