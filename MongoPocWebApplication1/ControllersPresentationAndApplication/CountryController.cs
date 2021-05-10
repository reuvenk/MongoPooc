using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MongoPocWebApplication1.Domain.Entities;
using MongoPocWebApplication1.Domain.Repositories;

namespace MongoPocWebApplication1.ControllersPresentationAndApplication
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository countryRepository;
        
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
        public async Task<ActionResult<string>> Post(Country country)
        {
            await countryRepository.AddAsync(country);
            return country.Id;
        }

        [HttpPatch]
        public async Task<IActionResult> PatchCountryWithModelState(
            [FromBody] JsonPatchDocument<Country> patchDoc, string id)
        {
            if (patchDoc != null)
            {
                var country = await countryRepository.GetByIdAsync(id);

                patchDoc.ApplyTo(country, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updateResult = await countryRepository.UpdateAsync(country);

                return new ObjectResult(updateResult);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
