using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.Authentication;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
using System.Linq.Expressions;

namespace EduLingual.Application.Service
{
    public interface IUserService
    {
        Task<(Tuple<string, Guid>, Result<LoginResponse>, User user)> Login(LoginRequest request);
        Task<(Tuple<string, Guid>, Result<RegisterResponse>, User user)> Register(RegisterRequest request);
        Task<Result<bool>> ForgetPassword(ForgetPasswordRequest request);
        Task<PagingResult<CourseViewModel>> GetCoursesByCenterId(int page, int size, Guid id);
        Task<PagingResult<UserCourseDto>> GetStudentsByCenterId(int page, int size, Guid centerId, Guid? courseId);
        Task<PagingResult<CourseDto>> GetCoursesByUserId(int page, int size, Guid userId);
        Task<PagingResult<UserViewModel>> GetPagination(Expression<Func<User, bool>>? predicate, int page, int size);
        Task<Result<UserViewModel>> Get(Guid id);
        Task<Result<UserViewModel>> Create(CreateUserRequest request);
        Task<Result<bool>> Update(Guid id, UpdateUserRequest request);
        Task<Result<bool>> Delete(Guid id);
    }
}
