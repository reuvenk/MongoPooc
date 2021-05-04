using System;
using System.Threading.Tasks;
using MongoPocWebApplication1.Domain.Models;

namespace MongoPocWebApplication1.Domain.RepositoryInterfaces
{
    public interface ICountryRepository : IMongoRepository
    {
        Task<Country> AddAsync(Country country);
        Task<Country> GetByIdAsync(String id);
    }
}