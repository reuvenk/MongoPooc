﻿using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoPocWebApplication1.Domain.Models;

namespace MongoPocWebApplication1.Infrastructure.Mongo.EntityConfigurations
{
    public class CityMap
    {
        public static void ConfigureClassMap()
        {
            BsonClassMap.RegisterClassMap<City>(cm =>
            {
                //TODO: check how to work without automap, 
                cm.AutoMap();
                //TODO: check how to work with SetIgnoreExtraElements false & , 
                // as explained in https://mongodb.github.io/mongo-csharp-driver/2.12/reference/bson/mapping/
                cm.SetIgnoreExtraElements(true);
                cm.SetIsRootClass(true);
                //Example of- Setting the ID and Using the GUID ID Generator cm.MapExtraElementsMember(c => c.CatchAll); 
                cm.MapIdMember(c => c.Id).SetElementName("_id").SetIdGenerator(CombGuidGenerator.Instance); ;
                cm.GetMemberMap(c => c.Name).SetElementName("name");
                cm.GetMemberMap(c => c.PopulationCount).SetElementName("population");
                cm.GetMemberMap(c => c.CountryId).SetElementName("countryId");
            });
        }
    }
}