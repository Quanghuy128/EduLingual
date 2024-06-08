using EduLingual.Application.Repository;
using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Payment;
using EduLingual.Domain.Dtos.UserCourse;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Enum;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EduLingual.Application.Repository.IUnitOfWork;

namespace EduLingual.Infrastructure.Service;

public class UserCourseService : BaseService<UserCourse>, IUserCourseService
{
    public UserCourseService(IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<UserCourse> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<Result<bool>> UserJoinCourseAsync(UserCourseRequest request)
    {
        try
        {
            User student = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(request.UserId), include: x => x.Include(x => x.Role));
            if (student == null) return BadRequest<bool>(MessageConstant.Vi.User.Fail.NotFoundUser);

            if (student.Role.RoleName != RoleName.UserRoleName) return BadRequest<bool>(MessageConstant.Vi.UserCourse.Fail.UserNotStudentRole);

            Course course = await _unitOfWork.GetRepository<Course>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(request.CourseId) && x.Status == CourseStatus.Active);
            if (course == null) return BadRequest<bool>(MessageConstant.Vi.Course.Fail.NotFoundCourse);

            UserCourse newUserCourse = new UserCourse();
            newUserCourse.CourseId = request.CourseId;
            newUserCourse.UserId = request.UserId;
            await _unitOfWork.GetRepository<UserCourse>().InsertAsync(newUserCourse);

            Payment payment = new Payment()
            {
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                PaymentMethod = request.PaymentMethod ?? "PayOS",
                Fee = request.Fee,
                CourseId = request.CourseId,
                UserId = request.UserId,
            };

            Payment result = await _unitOfWork.GetRepository<Payment>().InsertAsync(payment);

            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful)
            {
                throw new Exception(MessageConstant.Vi.UserCourse.Fail.JoinCourseFail);
            }
            return Success(isSuccessful);
        }
        catch (Exception ex)
        {
            return Fail<bool>(ex.Message);
        }
    }
}
