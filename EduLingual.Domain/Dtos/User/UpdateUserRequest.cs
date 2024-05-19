﻿using EduLingual.Domain.Enum;

namespace EduLingual.Domain.Dtos.User
{
    public class UpdateUserRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public UserStatus? UserStatus { get; set; }
        public Guid? RoleId { get; set; }
    }
}
