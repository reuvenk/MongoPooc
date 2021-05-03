using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoPocWebApplication1.Common;
using MongoPocWebApplication1.Model;
using MongoPocWebApplication1.Repository;

namespace MongoPocWebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> logger;
        private readonly ICityRepository cityRepository;
        private string CityModelName { get; } = "City";


        public CityController(ILogger<CityController> logger, IEnumerable<IMongoRepository> mongoRepositories)
        {
            this.logger = logger;
            this.cityRepository = (ICityRepository)mongoRepositories.SingleOrDefault(r => r.ModelName.Equals(CityModelName));
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<City>> Get(string name)
        {
            return await cityRepository.FindByName(name);
        }

        [HttpPost]
        public async Task<ActionResult<City>> Post(City city)
        {
            return await cityRepository.Insert(city);
        }
    }
}
