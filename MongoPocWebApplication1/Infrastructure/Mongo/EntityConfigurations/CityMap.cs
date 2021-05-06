using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoPocWebApplication1.Domain.Entities;

namespace MongoPocWebApplication1.Infrastructure.Mongo.EntityConfigurations
{
    public class CityMap : IMongoClassMapper
    {
        public void Execute()
        {
            BsonClassMap.RegisterClassMap<City>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.SetIsRootClass(true);
                //Example of- Setting the ID and Using the GUID ID Generator cm.MapExtraElementsMember(c => c.CatchAll); 
                cm.MapIdMember(c => c.Id)
                    .SetElementName("_id")
                    .SetIdGenerator(CombGuidGenerator.Instance);
                cm.GetMemberMap(c => c.Name).SetElementName("name");
                cm.GetMemberMap(c => c.PopulationCount).SetElementName("population");
                cm.GetMemberMap(c => c.CountryId).SetElementName("countryId");
            }); ;
        }
    }
}