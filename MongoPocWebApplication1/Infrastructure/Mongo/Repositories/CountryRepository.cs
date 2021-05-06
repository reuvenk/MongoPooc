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
    public class CountryRepository : ICountryRepository
    {
        // Defines mapping for collection name - how the current model (Country) would be named inside mongo as a collection
        private const string  CollectionName = "Country";
        private IMongoCollection<Country> CountryCollection { get;}

        //Make sure to ConfigureClassMap only once using a static init
        static CountryRepository()
        {
            CountryMap.ConfigureClassMap();
        }

        public CountryRepository(MongoConnector mongoConnector)
        {
            CountryCollection = mongoConnector.GetCollection<Country>(CollectionName);
        }

        public async Task AddAsync(Country country)
        {
            await CountryCollection.InsertOneAsync(country);
        }

        public async Task<Country> GetByIdAsync(string id)
        {
            var filter = Builders<Country>
                .Filter
                .Eq(c => c.Id, id);

            var result = await CountryCollection
                .Find(filter)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}