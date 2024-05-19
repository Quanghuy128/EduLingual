﻿using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Pagination;
using System.Linq.Expressions;

namespace EduLingual.Application.Service
{
    public interface ICourseService
    {
        Task<Result<List<CourseViewModel>>> GetCourses(CourseFilter courseFilter);
        Task<Result<List<CourseViewModel>>> GetCoursesByArea(string areaId);
        Task<Result<List<CourseViewModel>>> GetCoursesByLanguage(string languageId);
        Task<Result<List<CourseViewModel>>> GetCoursesByCategory(string categoryId);
        Task<Result<CourseViewModel>> GetCourseById(Guid id);
        Task<Result<CourseViewModel>> GetCourseByCondition(Expression<Func<UserDto, bool>> predicate);
        Task<Result<CourseViewModel>> Create(CreateCourseRequest request);
        Task<Result<bool>> Update(UpdateCourseRequest request);
        Task<Result<bool>> Delete(Guid id);
    }
}