namespace MongoPocWebApplication1.Domain.Entities
{
    public class Country
    {
        public string Id { get; set; }
        public string Name { get; set; }

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