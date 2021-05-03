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
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> logger;
        private readonly ICountryRepository countryRepository;
        private string CountryModelName { get; } = "Country";

        public CountryController(ILogger<CountryController> logger, IEnumerable<IMongoRepository> mongoRepositories)
        {
            this.logger = logger; this.countryRepository = (ICountryRepository)mongoRepositories.SingleOrDefault(r=>r.ModelName.Equals(CountryModelName));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> Get(string id)
        {
            return await countryRepository.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Country>> Post(Country country)
        {
            return await countryRepository.AddAsync(country);
        }
    }
}
