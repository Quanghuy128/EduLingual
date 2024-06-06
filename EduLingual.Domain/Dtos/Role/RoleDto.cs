using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Role
{
    public class RoleDto
    {
        public Guid Id { get; set; } 
        public string RoleName { get; set; }
    }
}
