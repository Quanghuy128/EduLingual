using EduLingual.Application.Repository;
using EduLingual.Domain.Common;
using EduLingual.Domain.Pagination;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Claims;
using static EduLingual.Application.Repository.IUnitOfWork;

namespace EduLingual.Infrastructure.Service
{
    public abstract class BaseService<T> where T : class
    {
        protected IUnitOfWork<ApplicationDbContext> _unitOfWork;
        protected ILogger<T> _logger;
        protected IMapper _mapper;
        protected IHttpContextAccessor _httpContextAccessor;

        public BaseService(IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<T> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected string GetUsernameFromJwt()
        {
            string username = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return username;
        }

        protected string GetRoleFromJwt()
        {
            string role = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role)!;
            return role;
        }

        protected Result<TEntity> Success<TEntity>(TEntity data)
        {
            return new Result<TEntity>
            {
                Data = data,
                StatusCode = HttpStatusCode.OK,
            };
        }

        protected PagingResult<TEntity> Success<TEntity>(IPaginate<TEntity> data)
        {
            return new PagingResult<TEntity>(data)
            {
                Data = data,
                StatusCode = HttpStatusCode.OK,
            };
        }

        protected Result<TEntity> BadRequest<TEntity>(string message)
        {
            return new Result<TEntity>
            {
                Message = message,
                StatusCode = HttpStatusCode.BadRequest,
            };
        }

        protected Result<TEntity> BadRequests<TEntity>(string message)
        {
            return new Result<TEntity>
            {
                Message = message,
                StatusCode = HttpStatusCode.BadRequest,
            };
        }

        protected Result<TEntity> NotFound<TEntity>(string message)
        {
            return new Result<TEntity>
            {
                Message = message,
                StatusCode = HttpStatusCode.NotFound,
            };
        }

        protected PagingResult<TEntity> NotFounds<TEntity>(string message)
        {
            return new PagingResult<TEntity>
            {
                Message = message,
                StatusCode = HttpStatusCode.NotFound,
            };
        }

    }
}
