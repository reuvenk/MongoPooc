
// ReSharper disable CheckNamespace

using Bks.DataAccess.Mongo;
using Microsoft.Extensions.Configuration;
using MongoPocWebApplication1.Domain.Repositories;
using MongoPocWebApplication1.Infrastructure.Mongo;
using MongoPocWebApplication1.Infrastructure.Mongo.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddMongoDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoSettings>(options =>
            {
                options.ConnectionString = configuration.GetSection("Mongo:ConnectionString").Value;
                options.Database = configuration.GetSection("Mongo:Database").Value;
                options.CollectionPrefix = configuration.GetSection("Mongo:CollectionPrefix").Value;
            });

            services.AddMongoConnector<LocationMongoConnector>();//options => options.services);

            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            return services;
		}
    }
}