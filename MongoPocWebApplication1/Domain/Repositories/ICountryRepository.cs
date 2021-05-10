using System.Threading.Tasks;
using MongoDB.Driver;
using MongoPocWebApplication1.Domain.Entities;

namespace MongoPocWebApplication1.Domain.Repositories
{
    public interface ICountryRepository
    {
        Task AddAsync(Country country);
        Task<Country> GetByIdAsync(string id);
        Task<UpdateResult> UpdateAsync(Country country);
    }
}