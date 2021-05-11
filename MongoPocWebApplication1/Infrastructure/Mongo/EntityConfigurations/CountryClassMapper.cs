using Bks.DataAccess.Mongo;
using MongoDB.Bson.Serialization;
using MongoPocWebApplication1.Domain.Entities;

namespace MongoPocWebApplication1.Infrastructure.Mongo.EntityConfigurations
{
    public class CountryMap : IMongoClassMapper
    {
        public void Map()
        {
            BsonClassMap.RegisterClassMap<Country>(cm =>
            {
                //Working Without AutoMap example
                // as explained in https://mongodb.github.io/mongo-csharp-driver/2.12/reference/bson/mapping/
                // cm.AutoMap();
                // cm.SetIgnoreExtraElements(true);
                cm.SetIsRootClass(true);
                cm.MapIdMember(c => c.Id).SetElementName("_id");
                //When working With AutoMap: Use MapMember(..).SetElementName(..)
                //Without AutoMap: Use GetMemberMap(..).SetElementName(..) 
                cm.MapMember(c => c.Name).SetElementName("name");
                //CatchAll Used to encapsulate all none mapped members that exists in db
                cm.MapExtraElementsMember(c => c.CatchAll);
                //cm.GetMemberMap(c => c.Name).SetElementName("name");
            });
        }
    }
}