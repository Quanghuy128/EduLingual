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
        Task<PagingResult<CourseViewModel>> GetCourses(int page, int size, string? title, CourseFilter? courseFilter, string? sort);
        Task<Result<List<CourseViewModel>>> GetHighlightedCourses();
        Task<PagingResult<CourseViewModel>> GetPagination(int page, int size, string? title, CourseStatus? status, string? centerName);
        Task<Result<CourseViewModel>> GetCourseById(Guid id);
        Task<Result<CourseViewModel>> Create(CreateCourseRequest request);
        Task<Result<bool>> Update(Guid id, UpdateCourseRequest request);
        Task<Result<bool>> Delete(Guid id);
    }
}
