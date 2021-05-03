using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using MongoPocWebApplication1.Common;
using MongoPocWebApplication1.Model;

namespace MongoPocWebApplication1.Repository
{
    public interface ICityRepository : IMongoRepository
    {
        //TODO: what is the purpose?
        const string Name = "" ;

        //Is Add Async in Mongo?
        Task<City> AddAsync(City city);
        Task<City> GetByNameAsync(string name);
    }
}