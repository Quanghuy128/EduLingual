using EduLingual.Api.Helpers;
using EduLingual.Domain.Enum;
using Microsoft.AspNetCore.Authorization;

namespace EduLingual.Api.Configuration
{
    public class ConfigureAuthorizeAttribute : AuthorizeAttribute
    {
        public ConfigureAuthorizeAttribute(params RoleEnum[] roles) {
            var allowedRolesAsString = roles.Select(x => x.GetDescriptionFromEnum());
            Roles = string.Join(",", allowedRolesAsString);
        }
    }
}
