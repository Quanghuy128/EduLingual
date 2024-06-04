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

        public async Task<Result<PaymentViewModel>> Create(CreatePaymentRequest request)
        {
            try
            {
                Payment payment = new Payment()
                {
                    PaymentMethod = request.PaymentMethod,
                    Fee = request.Fee,
                    CourseId = request.CourseId,
                    UserId = request.UserId,
                };

                Payment result = await _unitOfWork.GetRepository<Payment>().InsertAsync(payment);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.Payment.Fail.CreatePayment);
                }

                return Success(_mapper.Map<PaymentViewModel>(result));
            }
            catch (Exception ex)
            {
                return Fail<PaymentViewModel>(ex.Message);
            }
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

        public async Task<PagingResult<PaymentViewModel>> GetPagination(Expression<Func<Payment, bool>>? predicate, int page, int size)
        {
            try
            {
                IPaginate<Payment> payments = await _unitOfWork.GetRepository<Payment>().GetPagingListAsync();

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
