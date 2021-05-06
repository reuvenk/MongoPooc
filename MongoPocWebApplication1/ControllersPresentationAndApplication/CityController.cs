using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoPocWebApplication1.Domain.Entities;
using MongoPocWebApplication1.Domain.Repositories;

namespace MongoPocWebApplication1.ControllersPresentationAndApplication
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository cityRepository;
        
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
        public async Task<ActionResult<Guid>> Post(City city)
        {
            await cityRepository.AddAsync(city);

            return city.Id;
        }
    }
}
