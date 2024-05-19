using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace EduLingual.Api.Configuration
{
    public static class ConfigureSwaggerOptions
    {
        public static void AddSwaggerDocumentation(this SwaggerGenOptions options)
        {

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }

        public static IServiceCollection AddSwaggerGenOption(this IServiceCollection service)
        {
            return service.AddSwaggerGen(c =>
            {
                c.AddSwaggerDocumentation();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Edulingual API",
                    Version = "v1",
                    Description = "Edulingual API",
                    Contact = new OpenApiContact
                    {
                        Name = "Contact Developers",
                        Url = new Uri("https://github.com/Quanghuy128/EduLingual")
                    }
                });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
            });
        }
    }
}
