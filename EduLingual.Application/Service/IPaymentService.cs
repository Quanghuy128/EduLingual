using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.Payment;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Application.Service
{
    public interface IPaymentService
    {
        Task<PagingResult<PaymentViewModel>> GetPagination(Expression<Func<Payment, bool>>? predicate, int page, int size);
        Task<Result<PaymentViewModel>> GetPaymentById(Guid id);
        Task<Result<PaymentViewModel>> Create(CreatePaymentRequest request);
        Task<Result<bool>> Update(Guid id, UpdatePaymentRequest request);
        Task<Result<bool>> Delete(Guid id);
    }
}
