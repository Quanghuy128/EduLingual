using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
using System.Linq.Expressions;

namespace EduLingual.Application.Service
{
    public interface ICourseService
    {
        Task<Result<List<CourseViewModel>>> GetCourses(CourseFilter courseFilter);
        Task<Result<List<UserCourseDto>>> GetStudentsByCourse(Guid id);
        // Task<Result<List<CourseViewModel>>> GetTopCourses(Guid id);
        Task<PagingResult<CourseViewModel>> GetPagination(Expression<Func<Course, bool>>? predicate, int page, int size);
        Task<Result<CourseViewModel>> GetCourseById(Guid id);
        Task<Result<CourseViewModel>> GetCourseByCondition(Expression<Func<UserDto, bool>> predicate);
        Task<Result<CourseViewModel>> Create(CreateCourseRequest request);
        Task<Result<bool>> Update(Guid id, UpdateCourseRequest request);
        Task<Result<bool>> Delete(Guid id);
    }
}
