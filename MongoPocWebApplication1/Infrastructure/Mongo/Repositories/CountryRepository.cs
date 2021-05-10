using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoPocWebApplication1.Domain.Entities;
using MongoPocWebApplication1.Domain.Repositories;

namespace MongoPocWebApplication1.Infrastructure.Mongo.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        // Defines mapping for collection name - how the current model (Country) would be named inside mongo as a collection
        private const string  CollectionName = "Country";

        private IMongoCollection<Country> CountryCollection { get;}

        public CountryRepository(LocationMongoConnector mongoConnector)
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
        

        public async Task<UpdateResult> UpdateAsync(Country country)
        {

            var filter = Builders<Country>.Filter.Where(c => c.Id == country.Id);

            var update = Builders<Country>.Update.Set("name", country.Name);
                
            return await CountryCollection.UpdateOneAsync(
                filter,
                update);
        }
    }
}