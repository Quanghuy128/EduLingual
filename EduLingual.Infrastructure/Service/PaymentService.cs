using EduLingual.Application.Extensions;
using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.CourseArea;
using EduLingual.Domain.Dtos.Payment;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Pagination;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static EduLingual.Application.Repository.IUnitOfWork;

namespace EduLingual.Infrastructure.Service
{
    public class PaymentService : BaseService<PaymentService>, IPaymentService
    {
        public PaymentService(IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<PaymentService> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            try
            {
                Payment payment = await _unitOfWork.GetRepository<Payment>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));

                _unitOfWork.GetRepository<Payment>().DeleteAsync(payment);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.Payment.Fail.DeletePayment);
                }

                return Success(isSuccessful);
            }
            catch (Exception ex)
            {
                return Fail<bool>(ex.Message);
            }
        }

        public async Task<PagingResult<PaymentViewModel>> GetPagination(DateTime? startDate, DateTime? endDate, string? centerName, int page, int size)
        {
            try
            {
                IPaginate<Payment> payments = await _unitOfWork.GetRepository<Payment>().GetPagingListAsync(
                        predicate: BuildGetPaymentsQuery(startDate, endDate),
                        include: x => x.Include(x => x.User)
                                       .Include(x => x.Course).ThenInclude(x => x.Center),
                        orderBy: x => x.OrderByDescending(x => x.CreatedAt),
                        page: page,
                        size: size
                    );

                if (!String.IsNullOrEmpty(centerName))
                {
                    payments = await _unitOfWork.GetRepository<Payment>().GetPagingListAsync(
                        predicate: x => x.Course.Center.FullName.ToLower().Contains(centerName.ToLower().Trim()),
                        include: x => x.Include(x => x.Course).ThenInclude(x => x.Center),
                        page: page,
                        size: size
                    );
                }

                return SuccessWithPaging<PaymentViewModel>(
                        _mapper.Map<IPaginate<PaymentViewModel>>(payments),
                        page,
                        size,
                        payments.Total);
            }
            catch (Exception ex)
            {
            }
            return null!;
        }

        private Expression<Func<Payment, bool>> BuildGetPaymentsQuery(DateTime? startDate,
           DateTime? endDate)
        {
            Expression<Func<Payment, bool>> filterQuery = x => x.IsDeleted == false;

            if (startDate != null && endDate == null)
            {
                filterQuery = filterQuery.AndAlso(p =>
                    p.CreatedAt >= startDate && p.CreatedAt <= startDate.Value.AddDays(1));
            }
            else if (startDate != null)
            {
                filterQuery = filterQuery.AndAlso(p => p.CreatedAt >= startDate);
            }

            if (endDate != null)
            {
                filterQuery = filterQuery.AndAlso(p => p.CreatedAt <= endDate);
            }

            return filterQuery;
        }

        public async Task<Result<PaymentViewModel>> GetPaymentById(Guid id)
        {
            try
            {
                Payment payment = await _unitOfWork.GetRepository<Payment>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return Success(_mapper.Map<PaymentViewModel>(payment));
            }
            catch (Exception ex)
            {
                return BadRequest<PaymentViewModel>(ex.Message);
            }
        }

        public Task<Result<bool>> Update(Guid id, UpdatePaymentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
