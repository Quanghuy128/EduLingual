using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.CourseCategory;
using EduLingual.Domain.Dtos.CourseLanguage;
using EduLingual.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Application.Service
{
    public interface ICourseCategoryService
    {
        Task<Result<List<CourseCategoryViewModel>>> GetAll(Expression<Func<CourseCategory, bool>>? predicate);
        Task<PagingResult<CourseCategoryViewModel>> GetPagination(Expression<Func<CourseCategory, bool>>? predicate, int page, int size);
        Task<Result<CourseCategoryViewModel>> Get(Guid id);
        Task<Result<CourseCategoryViewModel>> Create(CreateCourseCategoryRequest request);
        Task<Result<bool>> Update(Guid id, UpdateCourseCategoryRequest request);
        Task<Result<bool>> Delete(Guid id);
    }
}
