using EduLingual.Application.Repository;
using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.Dashboard;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Enum;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EduLingual.Infrastructure.Service;

public class DashboardService : BaseService<ReportDataDto>, IDashboardService
{
    private static DateTime Monday = GetStartOfWeek(DateTime.Now.AddHours(7), DayOfWeek.Monday);
    private static DateTime Tuesday = Monday.AddDays(1);
    private static DateTime Wednesday = Tuesday.AddDays(1);
    private static DateTime Thursday = Wednesday.AddDays(1);
    private static DateTime Friday = Thursday.AddDays(1);
    private static DateTime Saturday = Friday.AddDays(1);
    private static DateTime Sunday = Saturday.AddDays(1);

    public DashboardService(IUnitOfWork.IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<ReportDataDto> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    private static DateTime GetStartOfWeek(DateTime date, DayOfWeek startOfWeek)
    {
        int diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
        return date.AddDays(-1 * diff).Date;
    }

    public async Task<Result<ReportDataDto>> GetRevenueInWeek()
    {
        try
        {
            var paymentsMonday = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt >= Monday && p.CreatedAt < Tuesday);
            var totalRevenueMonday = paymentsMonday.Sum(p => p.Fee);

            var paymentsTuesday = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt >= Tuesday && p.CreatedAt < Wednesday);
            var totalRevenueTuesday = paymentsTuesday.Sum(p => p.Fee);

            var paymentsWednesday = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt >= Wednesday && p.CreatedAt < Thursday);
            var totalRevenueWednesday = paymentsWednesday.Sum(p => p.Fee);

            var paymentsThursday = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt >= Thursday && p.CreatedAt < Friday);
            var totalRevenueThursday = paymentsThursday.Sum(p => p.Fee);

            var paymentsFriday = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt >= Friday && p.CreatedAt < Saturday);
            var totalRevenueFriday = paymentsFriday.Sum(p => p.Fee);

            var paymentsSaturday = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt >= Saturday && p.CreatedAt < Sunday);
            var totalRevenueSaturday = paymentsSaturday.Sum(p => p.Fee);

            var paymentsSunday = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt >= Sunday && p.CreatedAt < Sunday.AddDays(1));
            var totalRevenueSunday = paymentsTuesday.Sum(p => p.Fee);

            var reportData = new ReportDataDto()
            {
                DataInMonday = totalRevenueMonday,
                DataInTuesday = totalRevenueTuesday,
                DataInWednesday = totalRevenueWednesday,
                DataInThursday = totalRevenueThursday,
                DataInFriday = totalRevenueFriday,
                DataInSaturday = totalRevenueSaturday,
                DataInSunday = totalRevenueSunday,
            };
            return Success(reportData);
        }
        catch (Exception ex)
        {
            return Fail<ReportDataDto>(ex.Message);
        }
    }

    public async Task<Result<ReportDataDto>> GetTeacherInWeek()
    {
        try
        {
            string roleName = Enum.GetName(typeof(RoleEnum), RoleEnum.Teacher);
            var usersMonday = await _unitOfWork.GetRepository<User>().GetListAsync(
                   include: u => u.Include(u => u.Role),
                   predicate: t => t.CreatedAt >= Monday && t.CreatedAt < Tuesday && t.Role.RoleName == roleName
               );
            var totalUsersMonday = usersMonday.Count;

            var usersTuesday = await _unitOfWork.GetRepository<User>().GetListAsync(
                    include: u => u.Include(u => u.Role),
                    predicate: t => t.CreatedAt >= Tuesday && t.CreatedAt < Wednesday && t.Role.RoleName == roleName
                );
            var totalUsersTuesday = usersTuesday.Count;

            var usersWednesday = await _unitOfWork.GetRepository<User>().GetListAsync(
                    include: u => u.Include(u => u.Role),
                    predicate: t => t.CreatedAt >= Wednesday && t.CreatedAt < Thursday && t.Role.RoleName == roleName
                );
            var totalUsersWednesday = usersWednesday.Count;

            var usersThursday = await _unitOfWork.GetRepository<User>().GetListAsync(
                    include: u => u.Include(u => u.Role),
                    predicate: t => t.CreatedAt >= Thursday && t.CreatedAt < Friday && t.Role.RoleName == roleName
                );
            var totalUsersThursday = usersThursday.Count;

            var usersFriday = await _unitOfWork.GetRepository<User>().GetListAsync(
                    include: u => u.Include(u => u.Role),
                    predicate: t => t.CreatedAt >= Friday && t.CreatedAt < Saturday && t.Role.RoleName == roleName
                );
            var totalUsersFriday = usersFriday.Count;

            var usersSaturday = await _unitOfWork.GetRepository<User>().GetListAsync(
                    include: u => u.Include(u => u.Role),
                    predicate: t => t.CreatedAt >= Saturday && t.CreatedAt < Sunday && t.Role.RoleName == roleName
                );
            var totalUsersSaturday = usersSaturday.Count;

            var usersSunday = await _unitOfWork.GetRepository<User>().GetListAsync(
                    include: u => u.Include(u => u.Role),
                    predicate: t => t.CreatedAt >= Sunday && t.CreatedAt < Sunday.AddDays(1) && t.Role.RoleName == roleName
                );
            var totalUsersSunday = usersSunday.Count;

            var reportData = new ReportDataDto()
            {
                DataInMonday = totalUsersMonday,
                DataInTuesday = totalUsersTuesday,
                DataInWednesday = totalUsersWednesday,
                DataInThursday = totalUsersThursday,
                DataInFriday = totalUsersFriday,
                DataInSaturday = totalUsersSaturday,
                DataInSunday = totalUsersSunday,
            };
            return Success(reportData);
        }
        catch (Exception ex)
        {
            return Fail<ReportDataDto>(ex.Message);
        }
    }

    public async Task<Result<ReportDataDto>> GetUserInWeek()
    {
        try
        {
            string roleName = Enum.GetName(typeof(RoleEnum), RoleEnum.User);
            var usersMonday = await _unitOfWork.GetRepository<User>().GetListAsync(
                    include: u => u.Include(u => u.Role), 
                    predicate: t => t.CreatedAt >= Monday && t.CreatedAt < Tuesday && t.Role.RoleName == roleName
                );
            var totalUsersMonday = usersMonday.Count;

            var usersTuesday = await _unitOfWork.GetRepository<User>().GetListAsync(
                    include: u => u.Include(u => u.Role),
                    predicate: t => t.CreatedAt >= Tuesday && t.CreatedAt < Wednesday && t.Role.RoleName == roleName
                );
            var totalUsersTuesday = usersTuesday.Count;

            var usersWednesday = await _unitOfWork.GetRepository<User>().GetListAsync(
                    include: u => u.Include(u => u.Role),
                    predicate: t => t.CreatedAt >= Wednesday && t.CreatedAt < Thursday && t.Role.RoleName == roleName
                );
            var totalUsersWednesday = usersWednesday.Count;

            var usersThursday = await _unitOfWork.GetRepository<User>().GetListAsync(
                    include: u => u.Include(u => u.Role),
                    predicate: t => t.CreatedAt >= Thursday && t.CreatedAt < Friday && t.Role.RoleName == roleName
                );
            var totalUsersThursday = usersThursday.Count;

            var usersFriday = await _unitOfWork.GetRepository<User>().GetListAsync(
                    include: u => u.Include(u => u.Role),
                    predicate: t => t.CreatedAt >= Friday && t.CreatedAt < Saturday && t.Role.RoleName == roleName
                );
            var totalUsersFriday = usersFriday.Count;

            var usersSaturday = await _unitOfWork.GetRepository<User>().GetListAsync(
                    include: u => u.Include(u => u.Role),
                    predicate: t => t.CreatedAt >= Saturday && t.CreatedAt < Sunday && t.Role.RoleName == roleName
                );
            var totalUsersSaturday = usersSaturday.Count;

            var usersSunday = await _unitOfWork.GetRepository<User>().GetListAsync(
                    include: u => u.Include(u => u.Role),
                    predicate: t => t.CreatedAt >= Sunday && t.CreatedAt < Sunday.AddDays(1) && t.Role.RoleName == roleName
                );
            var totalUsersSunday = usersSunday.Count;

            var reportData = new ReportDataDto()
            {
                DataInMonday = totalUsersMonday,
                DataInTuesday = totalUsersTuesday,
                DataInWednesday = totalUsersWednesday,
                DataInThursday = totalUsersThursday,
                DataInFriday = totalUsersFriday,
                DataInSaturday = totalUsersSaturday,
                DataInSunday = totalUsersSunday,
            };
            return Success(reportData);
        }
        catch (Exception ex)
        {
            return Fail<ReportDataDto>(ex.Message);
        }
    }
}
