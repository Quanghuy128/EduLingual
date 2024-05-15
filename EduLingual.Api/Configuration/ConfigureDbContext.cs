using Autofac;
using EduLingual.Domain.Common;
using EduLingual.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;

namespace EduLingual.Api.Configuration
{
    public static class ConfigureDbContext
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(AppConfig.ConnectionString.DefaultConnection));
            return services;
        }

        public static ContainerBuilder AddDbContext(this ContainerBuilder builder)
        {
            builder.Register(c => new NpgsqlConnection(AppConfig.ConnectionString.DefaultConnection))
                    .As<IDbConnection>()
                    .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().As<DbContext>().InstancePerLifetimeScope();
            return builder;
        }
    }
}
