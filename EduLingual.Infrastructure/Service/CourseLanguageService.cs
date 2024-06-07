using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.CourseArea;
using EduLingual.Domain.Dtos.CourseCategory;
using EduLingual.Domain.Dtos.CourseLanguage;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Pagination;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static EduLingual.Application.Repository.IUnitOfWork;

namespace EduLingual.Infrastructure.Service
{
    public class CourseLanguageService : BaseService<CourseLanguageService>, ICourseLanguageService
    {
        public CourseLanguageService(IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<CourseLanguageService> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<Result<CourseLanguageViewModel>> Get(Guid id)
        {
            try
            {
                CourseLanguage courseLanguage = await _unitOfWork.GetRepository<CourseLanguage>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return Success(_mapper.Map<CourseLanguageViewModel>(courseLanguage));
            }
            catch (Exception ex)
            {
                return BadRequest<CourseLanguageViewModel>(ex.Message);
            }
        }

        public async Task<Result<List<CourseLanguageViewModel>>> GetAll(Expression<Func<CourseLanguage, bool>>? predicate)
        {
            try
            {
                ICollection<CourseLanguage> courseLanguages = await _unitOfWork.GetRepository<CourseLanguage>().GetListAsync();
                return Success(_mapper.Map<List<CourseLanguageViewModel>>(courseLanguages));
            }
            catch (Exception ex)
            {
                return BadRequest<List<CourseLanguageViewModel>>(ex.Message);
            }
        }

        public async Task<PagingResult<CourseLanguageViewModel>> GetPagination(Expression<Func<CourseLanguage, bool>>? predicate, int page, int size)
        {
            try
            {
                IPaginate<CourseLanguage> courseLanguages = await _unitOfWork.GetRepository<CourseLanguage>().GetPagingListAsync();

                return SuccessWithPaging<CourseLanguageViewModel>(
                        _mapper.Map<IPaginate<CourseLanguageViewModel>>(courseLanguages),
                        page,
                        size,
                        courseLanguages.Total);
            }
            catch (Exception ex)
            {
            }
            return null!;
        }

        public async Task<Result<CourseLanguageViewModel>> Create(CreateCourseLanguageRequest request)
        {
            try
            {
                CourseLanguage courseLanguage = new CourseLanguage()
                {
                    Name = request.Name,
                    Status = request.CourseLanguageStatus,
                };

                CourseLanguage result = await _unitOfWork.GetRepository<CourseLanguage>().InsertAsync(courseLanguage);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.CourseLanguage.Fail.CreateCourseLanguage);
                }

                return Success(_mapper.Map<CourseLanguageViewModel>(result));
            }
            catch (Exception ex)
            {
                return Fail<CourseLanguageViewModel>(ex.Message);
            }
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            try
            {
                CourseLanguage courseLanguage = await _unitOfWork.GetRepository<CourseLanguage>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));

                _unitOfWork.GetRepository<CourseLanguage>().DeleteAsync(courseLanguage);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.CourseLanguage.Fail.DeleteCourseLanguage);
                }

                return Success(isSuccessful);
            }
            catch (Exception ex)
            {
                return Fail<bool>(ex.Message);
            }
        }

        public async Task<Result<bool>> Update(Guid id, UpdateCourseLanguageRequest request)
        {
            try
            {
                CourseLanguage courseLanguage = await _unitOfWork.GetRepository<CourseLanguage>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));

                CourseLanguage newCourseLanguage = new CourseLanguage()
                {
                    Id = id,
                    Name = request.Name ?? courseLanguage.Name,
                    Status = request.CourseLanguageStatus ?? courseLanguage.Status
                };

                _unitOfWork.GetRepository<CourseLanguage>().UpdateAsync(newCourseLanguage);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccessful)
                {
                    throw new Exception(MessageConstant.Vi.CourseLanguage.Fail.UpdateCourseLanguage);
                }
                return Success(isSuccessful);
            }
            catch (Exception ex)
            {
                return Fail<bool>(ex.Message);
            }
        }

        public async Task<Result<List<CourseCategoryViewModel>>> GetCategoriesByLanguageId(Guid id)
        {
            try
            {
                CourseLanguage courseLanguage = await _unitOfWork.GetRepository<CourseLanguage>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));
                if (courseLanguage == null) throw new Exception(MessageConstant.Vi.CourseLanguage.Fail.NotFoundCourseLanguage);

                ICollection<CourseCategory> courseLanguages = await _unitOfWork.GetRepository<CourseCategory>().GetListAsync(predicate: x => x.LanguageId.Equals(id));
                return Success(_mapper.Map<List<CourseCategoryViewModel>>(courseLanguages));
            }
            catch (Exception ex)
            {
                return BadRequest<List<CourseCategoryViewModel>>(ex.Message);
            }
        }
    }
}
