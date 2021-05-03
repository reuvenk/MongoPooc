namespace MongoPocWebApplication1.Common
{
    public interface IMongoRepository
    {
        abstract string ModelName { get; }
        void RegisterClassMapAndInit(MongoConnector mongoConnector);
    }
}
