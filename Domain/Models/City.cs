using System;

namespace MongoPocWebApplication1.Domain.Models
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PopulationCount { get; set; }
        public string CountryId { get; set; }


        public City(Guid id, string name, int populationCount, string countryId)
        {
            this.Id = id;
            this.Name = name;
            PopulationCount = populationCount;
            CountryId = countryId;
        }
    }
}