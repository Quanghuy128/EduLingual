﻿using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.Authentication;
using EduLingual.Domain.Dtos.User;
using EduLingual.Domain.Entities;
using System.Linq.Expressions;

namespace EduLingual.Application.Service
{
    public interface IUserService
    {
        Task<(Tuple<string, Guid>, Result<LoginResponse>, User user)> Login(LoginRequest request);
        Task<Result<List<UserViewModel>>> GetAll(Expression<Func<User, bool>>? predicate);
        Task<PagingResult<UserViewModel>> GetPagination(Expression<Func<User, bool>>? predicate, int page, int size);
        Task<Result<UserViewModel>> Get(Guid id);
        Task<Result<UserViewModel>> GetByCondition(Expression<Func<User, bool>> predicate);
        Task<Result<UserViewModel>> Create(CreateUserRequest request);
        Task<Result<bool>> Update(Guid id, UpdateUserRequest request);
        Task<Result<bool>> Delete(Guid id);
    }
}
