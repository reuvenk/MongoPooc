using MongoDB.Bson;

namespace MongoPocWebApplication1.Domain.Entities
{
    public class Country
    {
        public string Id { get; set; }
        public string Name { get; set; }

        //CatchAll is Used to encapsulate all none mapped members that exists in db
        internal BsonDocument CatchAll { get; set; }
        public Country()
        {
        }

        public Country(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}