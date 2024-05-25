using EduLingual.Api.Helpers;
using EduLingual.Application.GoogleServices.Auth;
using EduLingual.Application.Repository;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Authentication;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Enum;
using EduLingual.Infrastructure.Service;
using Google.Apis.Oauth2.v2.Data;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace EduLingual.Infrastructure.GoogleServices.Auth
{
    public class GoogleService : BaseService<GoogleService>, IGoogleService
    {
        public GoogleService(IUnitOfWork.IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<GoogleService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<Result<(Tuple<string, Guid>, Result<LoginResponse>, User)>> GoogleLogin(string token)
        {
            Userinfo userinfo = null!;
            User user = null!;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync("https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + token);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        JObject userInfoJSON = JObject.Parse(json);
                        userinfo = new Userinfo()
                        {
                            Id = userInfoJSON["id"]?.ToString(),
                            Email = userInfoJSON["email"]?.ToString(),
                            Name = userInfoJSON["name"]?.ToString(),
                            GivenName = userInfoJSON["given_name"]?.ToString(),
                            FamilyName = userInfoJSON["family_name"]?.ToString(),
                            Picture = userInfoJSON["picture"]?.ToString(),
                            VerifiedEmail = bool.Parse(userInfoJSON["verified_email"]!.ToString()!),
                        };

                        //Add User To DB
                        var foundUser = await _unitOfWork.GetRepository<User>()
                                                    .SingleOrDefaultAsync(predicate: x => x.Email.Equals(userinfo.Email));
                        if (foundUser == null)
                        {
                            Role userRole = await _unitOfWork.GetRepository<Role>().SingleOrDefaultAsync(predicate: x => x.RoleName.Equals("User"));
                            User newUser = new User()
                            {
                                Email = userinfo.Email!,
                                FullName = userinfo.GivenName!,
                                ImageUrl = userinfo.Picture!,
                                RoleId = userRole.Id,
                            };
                            user = await _unitOfWork.GetRepository<User>()
                                                    .InsertAsync(newUser);

                            if (user == null)
                            {
                                throw new ArgumentException(MessageConstant.Vi.User.Fail.CreateUser);
                            }
                            RoleEnum role = EnumHelper.ParseEnum<RoleEnum>(user.Role.RoleName);
                            Tuple<string, Guid> guidClaim = null!;
                            LoginResponse loginResponse = null!;

                            loginResponse = new LoginResponse(user.Id, user.UserName, user.FullName, user.Role.RoleName, user.Status.GetDescriptionFromEnum());
                            return Success((guidClaim, Success(loginResponse), user));
                        }
                    }
                    else
                    {
                        throw new Exception(MessageConstant.Vi.Auth.GoogleLoginFail);
                    }
                }
                catch (Exception ex)
                {
                    return Fail<(Tuple<string, Guid>, Result<LoginResponse>, User)>(ex.Message);
                }
            }
            return null!;
        }
    }
}
