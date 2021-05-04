using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoPocWebApplication1.Domain.Models;
using MongoPocWebApplication1.Domain.RepositoryInterfaces;

namespace MongoPocWebApplication1.ControllersPresentationAndApplication
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository cityRepository;
        private string CityModelName { get; } = "City";


        public CityController(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<City>> Get(string name)
        {
            return await cityRepository.GetByNameAsync(name);
        }

        [HttpPost]
        public async Task<ActionResult<City>> Post(City city)
        {
            return await cityRepository.AddAsync(city);
        }
    }
}
