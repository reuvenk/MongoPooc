﻿using Bks.DataAccess.Mongo.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Bks.DataAccess.Mongo.AspNetCore
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