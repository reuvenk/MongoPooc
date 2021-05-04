using System.Threading.Tasks;
using MongoPocWebApplication1.Domain.Models;

namespace MongoPocWebApplication1.Domain.RepositoryInterfaces
{
    public interface ICityRepository : IMongoRepository
    {
        const string Name = "" ;
        Task<City> AddAsync(City city);
        Task<City> GetByNameAsync(string name);
    }
}