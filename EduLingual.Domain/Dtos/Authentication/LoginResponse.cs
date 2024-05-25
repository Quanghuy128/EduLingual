using EduLingual.Api.Helpers;
using EduLingual.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Authentication
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public RoleEnum Role { get; set; }
        public UserStatus Status { get; set; }

        public LoginResponse(Guid id, string username, string name, string role, string status)
        {
            Id = id;
            Username = username;
            Name = name;
            Role = EnumHelper.ParseEnum<RoleEnum>(role);
            Status = EnumHelper.ParseEnum<UserStatus>(status);
        }
    }
}
