namespace MongoPocWebApplication1.Domain.RepositoryInterfaces
{
    public interface IMongoRepository
    {
        abstract string ModelName { get; }
    }
}
