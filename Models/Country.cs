namespace MongoPocWebApplication1.Model
{
    public class Country
    {
        public string Id { get; set; }
        public string Name { get; set; }

        //Is empty ctor required for mongo?
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