namespace MongoPocWebApplication1.Common
{
    public interface IMongoRepository
    {
        public abstract string ModelName { get; }
        public void RegisterClassMapAndInit(MongoConnector mongoConnector);
    }
}
