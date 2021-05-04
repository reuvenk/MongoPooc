using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoPocWebApplication1.Domain.Models;
using MongoPocWebApplication1.Domain.RepositoryInterfaces;

namespace MongoPocWebApplication1.ControllersPresentationAndApplication
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository countryRepository;
        private string CountryModelName { get; } = "Country";

        public CountryController(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
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
