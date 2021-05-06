using System;
using System.Threading.Tasks;
using Bks.DataAccess.Mongo.Infrastructure;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoPocWebApplication1.Domain.Entities;
using MongoPocWebApplication1.Domain.Repositories;
using MongoPocWebApplication1.Infrastructure.Mongo.EntityConfigurations;

namespace MongoPocWebApplication1.Infrastructure.Mongo.Repositories
{
    public class CityRepository : ICityRepository
    {
        // Defines mapping for collection name - how the current model (City) would be named inside mongo as a collection
        private const string CollectionName = "city";

        private readonly IMongoCollection<City> cityCollection;

        public CityRepository(MongoConnector mongoConnector)
        {
            if (mongoConnector == null)
            {
                throw new ArgumentNullException(nameof(mongoConnector));
            }

            ConfigureClassMaps();

            cityCollection = mongoConnector.GetCollection<City>(CollectionName);
        }

        private static void ConfigureClassMaps()
        {
            CityMap.ConfigureClassMap();
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