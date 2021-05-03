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

        public CityController(ILogger<CityController> logger, ICityRepository cityRepository)
        {
            this.logger = logger;
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