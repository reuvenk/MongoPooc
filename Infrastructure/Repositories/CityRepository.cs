using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoPocWebApplication1.Domain.Models;
using MongoPocWebApplication1.Domain.RepositoryInterfaces;

namespace MongoPocWebApplication1.Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        private ILogger<CityRepository> Logger { get; set; }
        private IMongoCollection<City> CityCollection { get; set; }
        private MongoConnector MongoConnector { get; set; }

        public string ModelName => "City";
        
        public CityRepository(ILogger<CityRepository> logger, MongoConnector mongoConnector)
        {
            Logger = logger;
            MongoConnector = mongoConnector;

            //Register ClassMap...
            BsonClassMap.RegisterClassMap<City>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.SetIsRootClass(true);
                cm.GetMemberMap(c => c.Id).SetElementName("_id");
                cm.GetMemberMap(c => c.Name).SetElementName("name");
                cm.GetMemberMap(c => c.PopulationCount).SetElementName("population");
                cm.GetMemberMap(c => c.CountryId).SetElementName("countryId");
            });

            //init
            CityCollection = MongoConnector.GetCollection<City>(this);
        }

        public async Task<City> AddAsync(City city)
        {
            Logger.LogDebug($"Inserting City {city.Name}");
            await CityCollection.InsertOneAsync(city);
            return city;
        }

        public async Task<City> GetByNameAsync(string name)
        {
            Logger.LogDebug($"GetByNameAsync City: {name}");
            var filter = Builders<City>.Filter.Eq(c => c.Name, name);
            var result = await CityCollection.Find<City>(filter).FirstOrDefaultAsync();
            return result;
        }
    }
}