using EduLingual.Application.Repository;
using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Course;
using EduLingual.Domain.Dtos.CourseArea;
using EduLingual.Domain.Dtos.Feedback;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Pagination;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static EduLingual.Application.Repository.IUnitOfWork;

namespace EduLingual.Infrastructure.Service;

public class FeedbackService : BaseService<Feedback>, IFeedbackService
{
    public FeedbackService(IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<Feedback> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<Result<FeedbackViewModel>> Create(CreateFeedbackRequest request)
    {
        try
        {
            User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(request.UserId));
            if (user == null) return BadRequest<FeedbackViewModel>(MessageConstant.Vi.User.Fail.NotFoundUser);

            Feedback newFeedback = new Feedback
            {
                Description = request.Description,
                Rating = request.Rating,
                FeedbackStatus = Domain.Enum.FeedbackStatus.Active,
                UserId = request.UserId,
            };

            Feedback result = await _unitOfWork.GetRepository<Feedback>().InsertAsync(newFeedback);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            if (!isSuccessful)
            {
                throw new Exception(MessageConstant.Vi.Feedback.Fail.CreateFeedback);
            }

            return Success(_mapper.Map<FeedbackViewModel>(result));
        } catch (Exception ex)
        {
            return Fail<FeedbackViewModel>(ex.Message);
        }
    }

    public async Task<Result<bool>> Delete(Guid id)
    {
        try
        {
            Feedback feedback = await _unitOfWork.GetRepository<Feedback>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));
            if (feedback == null) return BadRequest<bool>(MessageConstant.Vi.Feedback.Fail.NotFoundFeedback);
            _unitOfWork.GetRepository<Feedback>().DeleteAsync(feedback);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            if (!isSuccessful)
            {
                throw new Exception(MessageConstant.Vi.Feedback.Fail.DeleteFeedback);
            }

            return Success(isSuccessful);
        }
        catch (Exception ex)
        {
            return Fail<bool>(ex.Message);
        }
    }

    public async Task<Result<FeedbackViewModel>> Get(Guid id)
    {
        Feedback feedback = await _unitOfWork.GetRepository<Feedback>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));

        if (feedback == null) return BadRequest<FeedbackViewModel>(MessageConstant.Vi.Feedback.Fail.NotFoundFeedback);

        return Success(_mapper.Map<FeedbackViewModel>(feedback));
    }

    public async Task<PagingResult<FeedbackViewModel>> GetPagination(Expression<Func<Feedback, bool>>? predicate, int page, int size)
    {
        try
        {
            IPaginate<Feedback> feedbacks = await _unitOfWork.GetRepository<Feedback>().GetPagingListAsync(selector: x => x, include: x => x.Include(x => x.User));

            return SuccessWithPaging<FeedbackViewModel>(
                    _mapper.Map<IPaginate<FeedbackViewModel>>(feedbacks),
                    page,
                    size,
                    feedbacks.Total);
        }
        catch (Exception ex)
        {
        }
        return null!;
    }

    public async Task<Result<bool>> Update(Guid id, UpdateFeedbackRequest request)
    {
        try
        {
            Feedback feedback = await _unitOfWork.GetRepository<Feedback>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));
            if (feedback == null) return BadRequest<bool>(MessageConstant.Vi.Feedback.Fail.NotFoundFeedback);
            User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(request.UserId));
            if(user == null) return BadRequest<bool>(MessageConstant.Vi.User.Fail.NotFoundUser);
            Feedback updateFeedback = new Feedback
            {
                Id = id,
                Description = request.Description ?? feedback.Description,
                Rating = request.Rating ?? feedback.Rating,
                User = user ?? feedback.User,
                FeedbackStatus = request.FeedbackStatus ?? feedback.FeedbackStatus
            };

            _unitOfWork.GetRepository<Feedback>().UpdateAsync(updateFeedback);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            if (!isSuccessful)
            {
                throw new Exception(MessageConstant.Vi.Feedback.Fail.UpdateFeedback);
            }
            return Success(isSuccessful);
        }
        catch (Exception ex)
        {
            return Fail<bool>(ex.Message);
        }
    }
}
