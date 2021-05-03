using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoPocWebApplication1.Common;
using MongoPocWebApplication1.Model;

namespace MongoPocWebApplication1.Repository
{
    public class CityRepository : ICityRepository
    {
        private ILogger<CityRepository> logger;
        private IMongoCollection<City> CityCollection { get; set; }

        public string ModelName => "City";

        public void RegisterClassMapAndInit(MongoConnector mongoConnector)
        {
            //Register ClassMap...
            BsonClassMap.RegisterClassMap<City>(cm =>
            {
                //TODO: Set collection name
                //cm.SetCollectionName("city")


                cm.SetIgnoreExtraElements(true);
                cm.SetIsRootClass(true);
                cm.GetMemberMap(c => c.Id).SetElementName("_id");
                cm.GetMemberMap(c => c.Name).SetElementName("name");
                cm.GetMemberMap(c => c.PopulationCount).SetElementName("population");
                cm.GetMemberMap(c => c.CountryId).SetElementName("countryId");
            });

            //init
            CityCollection = mongoConnector.GetCollection<City>("city", this);
        }

        public CityRepository(ILogger<CityRepository> logger, MongoConnector mongoConnector)
        {
            this.logger = logger;
        }

        public async Task<City> AddAsync(City city)
        {
            logger.LogDebug($"Inserting City {city.Name}");
            await CityCollection.InsertOneAsync(city);
            return city;
        }

        public async Task<City> GetByNameAsync(string name)
        {
            logger.LogDebug($"GetByNameAsync City: {name}");
            var filter = Builders<City>.Filter.Eq(c => c.Name, name);
            var result = await CityCollection.Find<City>(filter).FirstOrDefaultAsync();
            return result;
        }
    }
}