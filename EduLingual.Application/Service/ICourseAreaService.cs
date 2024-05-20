using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.CourseArea;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
using System.Linq.Expressions;

namespace EduLingual.Application.Service
{
    public interface ICourseAreaService
    {
        Task<Result<List<CourseAreaViewModel>>> GetAll(Expression<Func<CourseArea, bool>>? predicate);
        Task<PagingResult<CourseAreaViewModel>> GetPagination(Expression<Func<CourseArea, bool>>? predicate, int page, int size);
        Task<Result<CourseAreaViewModel>> Get(Guid id);
        Task<Result<CourseAreaViewModel>> Create(CreateCourseAreaRequest request);
        Task<Result<bool>> Update(Guid id, UpdateCourseAreaRequest request);
        Task<Result<bool>> Delete(Guid id);
    }
}
