using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.CourseArea;
using EduLingual.Domain.Dtos.Feedback;
using EduLingual.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Application.Service;

public interface IFeedbackService
{
    Task<PagingResult<FeedbackViewModel>> GetPagination(Expression<Func<Feedback, bool>>? predicate, int page, int size);
    Task<Result<FeedbackViewModel>> Get(Guid id);
    Task<Result<FeedbackViewModel>> Create(CreateFeedbackRequest request);
    Task<Result<bool>> Update(Guid id, UpdateFeedbackRequest request);
    Task<Result<bool>> Delete(Guid id);
}
