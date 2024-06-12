using Autofac;
using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Entities;
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace EduLingual.Application
{
    public static class DependencyInjection
    {
        public static void RegisterMapster(this ContainerBuilder builder)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Default.IgnoreNullValues(true);

            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(BaseEntity).Assembly
            };

            config = config.ConfigCustomMapper();

            config.Scan(assemblies);

            builder.RegisterInstance(config).SingleInstance();
            builder.RegisterType<ServiceMapper>().As<IMapper>().InstancePerLifetimeScope();
        }
        private static TypeAdapterConfig ConfigCustomMapper(this TypeAdapterConfig config)
        {
            TypeAdapterConfig<UpdateCourseRequest, Course>.NewConfig().IgnoreNullValues(true);
            return config;
        }
    }
}
