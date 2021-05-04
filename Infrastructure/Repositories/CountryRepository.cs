using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoPocWebApplication1.Domain.Models;
using MongoPocWebApplication1.Domain.RepositoryInterfaces;

namespace MongoPocWebApplication1.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private ILogger<CountryRepository> Logger { get; set; }
        private IMongoCollection<Country> CountryCollection { get; set; }
        private MongoConnector MongoConnector { get; set; }
        public string ModelName => "Country";

        public CountryRepository(ILogger<CountryRepository> logger, MongoConnector mongoConnector)
        {
            Logger = logger;
            MongoConnector = mongoConnector;
            BsonClassMap.RegisterClassMap<Country>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.SetIsRootClass(true);
                cm.GetMemberMap(c => c.Id).SetElementName("_id");
                cm.GetMemberMap(c => c.Name).SetElementName("name");
            });

            CountryCollection = MongoConnector.GetCollection<Country>(this);
        }

        public async Task<Country> AddAsync(Country country)
        {
            Logger.LogDebug($"Inserting Country {country.Name}");
            await CountryCollection.InsertOneAsync(country);
            return country;
        }

        public async Task<Country> GetByIdAsync(String id)
        {
            Logger.LogDebug($"GetByIdAsync Country Id {id}");
            var filter = Builders<Country>.Filter.Eq(c => c.Id, id);
            var result = await CountryCollection.Find<Country>(filter).FirstOrDefaultAsync();
            return result;
        }
    }
}