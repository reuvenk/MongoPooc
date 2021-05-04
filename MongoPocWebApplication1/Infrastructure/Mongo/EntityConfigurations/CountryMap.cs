using MongoDB.Bson.Serialization;
using MongoPocWebApplication1.Domain.Models;

namespace MongoPocWebApplication1.Infrastructure.Mongo.EntityConfigurations
{
    public class CountryMap
    {
        public static void ConfigureClassMap()
        {
            BsonClassMap.RegisterClassMap<Country>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.SetIsRootClass(true);
                cm.MapIdMember(c => c.Id).SetElementName("_id");
                cm.GetMemberMap(c => c.Name).SetElementName("name");
            });
        }
    }
}