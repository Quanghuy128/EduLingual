using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduLingual.Domain.Constants;

namespace EduLingual.Domain.Dtos.Payment
{
    public class CreatePaymentRequest
    {
        [Required(ErrorMessage = MessageConstant.Vi.Payment.Require.PaymentMethodRequired)]
        public string PaymentMethod { get; set; } = string.Empty;
        [Required(ErrorMessage = MessageConstant.Vi.Payment.Require.FeeRequired)]
        public double Fee { get; set; } = 0;
        [Required(ErrorMessage = MessageConstant.Vi.Payment.Require.CourseRequired)]
        public Guid CourseId { get; set; }
        [Required(ErrorMessage = MessageConstant.Vi.Payment.Require.UserRequired)]
        public Guid UserId { get; set; }
    }
}
