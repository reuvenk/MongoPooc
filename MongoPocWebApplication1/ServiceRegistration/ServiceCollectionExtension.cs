
// ReSharper disable CheckNamespace

using Bks.DataAccess.Mongo.Infrastructure;
using MongoPocWebApplication1.Domain.Repositories;
using MongoPocWebApplication1.Infrastructure;
using MongoPocWebApplication1.Infrastructure.Mongo.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
	{
        //TODO: Pass IConfiguration
		public static IServiceCollection AddMongoDbServices(this IServiceCollection services, string connectionString, string collectionPrefix, string database)
        {
            services.AddSingleton<MongoConnector>();
            services.AddSingleton<ICountryRepository, CountryRepository>();
            services.AddSingleton<ICityRepository, CityRepository>();
            return services;
		}

        public static void RegisterClassMapAndInit(this IServiceCollection services)
        {

        }
    }
}