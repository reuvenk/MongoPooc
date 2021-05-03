using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using MongoPocWebApplication1.Common;
using MongoPocWebApplication1.Model;

namespace MongoPocWebApplication1.Repository
{
    public interface ICityRepository : IMongoRepository
    {
        const string Name = "" ;
        Task<City> AddAsync(City city);
        Task<City> GetByNameAsync(string name);
    }
}