using System;
using System.Threading.Tasks;
using MongoPocWebApplication1.Common;
using MongoPocWebApplication1.Model;

namespace MongoPocWebApplication1.Repository
{
    public interface ICountryRepository : IMongoRepository
    {
        Task<Country> AddAsync(Country country);
        Task<Country> GetByIdAsync(String id);
    }
}