using System.Threading.Tasks;
using MongoPocWebApplication1.Domain.Entities;

namespace MongoPocWebApplication1.Domain.Repositories
{
    public interface ICityRepository
    {
        Task AddAsync(City city);
        Task<City> GetByNameAsync(string name);
    }
}