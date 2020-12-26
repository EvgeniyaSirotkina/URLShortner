using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using URLShortner.Data.EF;
using URLShortner.Data.Interfaces;
using URLShortner.Data.Repositories;

namespace URLShortner.Service.Infrastructure
{
    /// <summary>
    /// Static class to extend DI container.
    /// </summary>
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// Define extension method to registrate dependencies.
        /// </summary>
        public static IServiceCollection AddRepository(this IServiceCollection services, string connection)
        {
            services.AddTransient<IRepository, Repository>();
            services.AddDbContext<UrlContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("URLShortner.Web")));

            /*
            services.AddDbContext<UrlContext>(options => options
                .UseMySql(connection,
                    mysqlOptions =>
                        mysqlOptions.ServerVersion(new ServerVersion(new Version(10, 4, 6), ServerType.MariaDb))));

             using Pomelo.EntityFrameworkCore.MySql.Storage;
             using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

             *Version(10, 4, 6) is my MariaDB version, to see your version run following   
             * request in MariaDB's console   
             * SELECT VERSION();   
            */

            return services;
        }
    }
}
