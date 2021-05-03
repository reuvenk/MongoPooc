using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoPocWebApplication1.Common;
using MongoPocWebApplication1.Model;

namespace MongoPocWebApplication1.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private ILogger<CountryRepository> logger;
        private IMongoCollection<Country> CountryCollection { get; set; }

        public string ModelName => "Country";
        public void RegisterClassMapAndInit(MongoConnector mongoConnector)
        {
            BsonClassMap.RegisterClassMap<Country>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.SetIsRootClass(true);
                cm.GetMemberMap(c => c.Id).SetElementName("_id");
                cm.GetMemberMap(c => c.Name).SetElementName("name");
            });

            CountryCollection = mongoConnector.GetCollection<Country>(this);
        }

        public CountryRepository(ILogger<CountryRepository> logger)
        {
            this.logger = logger;
        }

        public async Task<Country> AddAsync(Country country)
        {
            logger.LogDebug($"Inserting Country {country.Name}");
            await CountryCollection.InsertOneAsync(country);
            return country;
        }

        public async Task<Country> GetByIdAsync(String id)
        {
            logger.LogDebug($"GetByIdAsync Country Id {id}");
            var filter = Builders<Country>.Filter.Eq(c => c.Id, id);
            var result = await CountryCollection.Find<Country>(filter).FirstOrDefaultAsync();
            return result;
        }
    }
}