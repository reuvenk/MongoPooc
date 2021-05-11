using Microsoft.Extensions.DependencyInjection;

namespace Bks.DataAccess.Mongo
{
    public static class MongoServiceCollectionExtension
    {
        public static IServiceCollection AddMongoConnector<TConnector>(this IServiceCollection services) 
            where TConnector: MongoConnector
        {
            services.AddSingleton<TConnector>();
            return services;
        }
    }
}
