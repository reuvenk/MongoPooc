using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoPocWebApplication1.Domain.Models;
using MongoPocWebApplication1.Domain.RepositoryInterfaces;
using MongoPocWebApplication1.Infrastructure.Mongo.EntityConfigurations;

namespace MongoPocWebApplication1.Infrastructure.Mongo.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private IMongoCollection<Country> CountryCollection { get; set; }
        private MongoConnector MongoConnector { get; set; }
        public string ModelName => "Country";

        public CountryRepository(MongoConnector mongoConnector)
        {
            MongoConnector = mongoConnector;

            CountryMap.ConfigureClassMap();

            CountryCollection = MongoConnector.GetCollection<Country>(ModelName.ToLower());
        }

        public async Task<Country> AddAsync(Country country)
        {
            await CountryCollection.InsertOneAsync(country);
            return country;
        }

        public async Task<Country> GetByIdAsync(String id)
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