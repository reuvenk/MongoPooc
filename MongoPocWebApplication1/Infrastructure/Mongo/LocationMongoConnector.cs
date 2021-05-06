using System.Collections.Generic;
using Bks.DataAccess.Mongo.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoPocWebApplication1.Infrastructure.Mongo.EntityConfigurations;

namespace MongoPocWebApplication1.Infrastructure.Mongo
{
    public class LocationMongoConnector : MongoConnector
    {
        public LocationMongoConnector(IOptions<MongoSettings> settings, ILogger<MongoConnector> logger)
            : base(settings, logger, GetClassMaps())
        {
        }

        private static IReadOnlyCollection<IMongoClassMapper> GetClassMaps()
        {
            var classMaps = new List<IMongoClassMapper>()
            {
                new CityMap(),
                new CountryMap()
            };
            return classMaps;
        }
    }
}