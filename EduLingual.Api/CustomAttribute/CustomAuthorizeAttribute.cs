using EduLingual.Api.Helpers;
using EduLingual.Domain.Enum;
using Microsoft.AspNetCore.Authorization;

namespace EduLingual.Api.CustomAttribute
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(params RoleEnum[] roleEnums)
        {
            var allowedRolesAsString = roleEnums.Select(x => x.GetDescriptionFromEnum());
            Roles = string.Join(",", allowedRolesAsString);
        }
    }
}
