using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Application.Service
{
    public interface ICourseService
    {
        Task<List<CourseViewModel>> GetCourses(CourseFilter courseFilter);
        Task<List<CourseViewModel>> GetCoursesByArea(string areaId);
        Task<List<CourseViewModel>> GetCoursesByLanguage(string languageId);
        Task<List<CourseViewModel>> GetCoursesByCategory(string categoryId);
        Task<CourseViewModel> GetCourseById(Guid id);
        Task<CourseViewModel> GetCourseByCondition(Expression<Func<UserDto, bool>> predicate);
        Task<CourseViewModel> Create(CreateCourseRequest request);
        Task<bool> Update(UpdateCourseRequest request);
        Task<bool> Delete(Guid id);
    }
}
