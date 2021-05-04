//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using MongoDB.Driver;
//using MongoPocWebApplication1.Domain.RepositoryInterfaces;


////Nuget: Bks.DataAccess.Mongo
//namespace MongoPocWebApplication1.Infrastructure
//{
//    public class MongoConnector
//    {
//        public MongoClient Client { get; }
//        public IMongoDatabase Database { get; }
//        private string CollectionPrefix { get; set; }

//        public MongoConnector(IOptions<MongoSettings> settings)
//        {
//            Client = new MongoClient(settings.Value.ConnectionString);

//            CollectionPrefix = settings
//                .Value
//                .CollectionPrefix;

//            var dbInstanceName = $"{settings.Value.Database}";

//            //used for POC testing!!!
//            Client.DropDatabase(dbInstanceName);

//            this.Database = Client.GetDatabase(dbInstanceName);
//        }
        
//        public IMongoCollection<TDocument> GetCollection<TDocument>(string name)
//        {
//            var mongoCollection = this.Database.GetCollection<TDocument>($"{CollectionPrefix}_{name}");
//            return mongoCollection;
//        }
//    }
//}