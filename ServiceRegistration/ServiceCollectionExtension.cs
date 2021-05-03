
// ReSharper disable CheckNamespace

using MongoPocWebApplication1.Common;
using MongoPocWebApplication1.Repository;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddMongoDbServices(this IServiceCollection services)
		{
			services.AddSingleton<IMongoRepository, CountryRepository>();
            services.AddSingleton<IMongoRepository, CityRepository>();
            services.AddSingleton<IMongoConnector, MongoConnector>();
            return services;
		}
    }
}