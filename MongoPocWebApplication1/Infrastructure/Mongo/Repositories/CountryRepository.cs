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
        private const string  CountryCollectionName = "Country";
        private IMongoCollection<Country> CountryCollection { get;}

        public CountryRepository(MongoConnector mongoConnector)
        {
            CountryMap.ConfigureClassMap();

            CountryCollection = mongoConnector.GetCollection<Country>(CountryCollectionName);
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