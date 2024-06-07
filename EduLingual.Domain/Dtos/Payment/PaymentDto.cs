using EduLingual.Domain.Common;
using EduLingual.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Domain.Dtos.Payment
{
    public class PaymentDto : BaseEntity
    {
        public string PaymentMethod { get; set; } = string.Empty;
        public double Fee { get; set; } = 0;
    }
}
