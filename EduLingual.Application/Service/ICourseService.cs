using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Enum;
using System.Linq.Expressions;

namespace EduLingual.Application.Service
{
    public interface ICourseService
    {
        Task<Result<List<CourseViewModel>>> GetCourses(string? title, CourseFilter? courseFilter, string? sort);
        Task<Result<List<UserCourseDto>>> GetStudentsByCourse(Guid id);
        // Task<Result<List<CourseViewModel>>> GetTopCourses(Guid id);
        Task<PagingResult<CourseViewModel>> GetPagination(int page, int size, string? title, CourseStatus? status, Guid? centerId);
        Task<Result<CourseViewModel>> GetCourseById(Guid id);
        Task<Result<CourseViewModel>> Create(CreateCourseRequest request);
        Task<Result<bool>> Update(Guid id, UpdateCourseRequest request);
        Task<Result<bool>> Delete(Guid id);
    }
}
