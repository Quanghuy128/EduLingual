using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.CourseArea;
using EduLingual.Domain.Dtos.CourseLanguage;
using EduLingual.Domain.Entities;
using System.Linq.Expressions;

namespace EduLingual.Application.Service
{
    public interface ICourseLanguageService
    {
        Task<Result<List<CourseLanguageViewModel>>> GetAll(Expression<Func<CourseLanguage, bool>>? predicate);
        Task<PagingResult<CourseLanguageViewModel>> GetPagination(Expression<Func<CourseLanguage, bool>>? predicate, int page, int size);
        Task<Result<CourseLanguageViewModel>> Get(Guid id);
        Task<Result<CourseLanguageViewModel>> Create(CreateCourseLanguageRequest request);
        Task<Result<bool>> Update(Guid id, UpdateCourseLanguageRequest request);
        Task<Result<bool>> Delete(Guid id);
    }
}
