using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EduLingual.Api.Configuration
{
    public static class ConfigureAuthentication
    {
        public static IServiceCollection AddJwtValidation(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = AppConfig.JwtSetting.ValidIssuer,
                    ValidateIssuer = AppConfig.JwtSetting.ValidateIssuer,
                    ValidateAudience = AppConfig.JwtSetting.ValidateAudience,
                    ValidateIssuerSigningKey = AppConfig.JwtSetting.ValidateIssuerSigningKey,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(AppConfig.JwtSetting.SecretKey))
                };
            });
            return services;
        }
    }
}
