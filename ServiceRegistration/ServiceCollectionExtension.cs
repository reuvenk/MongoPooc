
// ReSharper disable CheckNamespace

using MongoPocWebApplication1.Domain.RepositoryInterfaces;
using MongoPocWebApplication1.Infrastructure;
using MongoPocWebApplication1.Infrastructure.Mongo.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddMongoDbServices(this IServiceCollection services)
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