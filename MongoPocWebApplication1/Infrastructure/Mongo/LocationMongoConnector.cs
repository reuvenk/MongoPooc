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
            : base(settings, logger, new List<CityMap.ConfigureClassMap()>())
        {
            RegisterClassMaps();
        }

        private static void RegisterClassMaps()
        {
            CityMap.ConfigureClassMap();
        }
    }
}