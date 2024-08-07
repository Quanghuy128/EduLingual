﻿using EduLingual.Application.Repository;
using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.Dashboard;
using EduLingual.Domain.Entities;
using EduLingual.Domain.Enum;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EduLingual.Infrastructure.Service;

public class DashboardService : BaseService<ReportDataDto>, IDashboardService
{
    #region Day of the week
    private static DateTime Monday = new DateTime();
    private static DateTime Tuesday = new DateTime();
    private static DateTime Wednesday = new DateTime();
    private static DateTime Thursday = new DateTime();
    private static DateTime Friday = new DateTime();
    private static DateTime Saturday = new DateTime();
    private static DateTime Sunday = new DateTime();
    #endregion
    private int ThisYear = DateTime.Now.Year;

    public DashboardService(IUnitOfWork.IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<ReportDataDto> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
        Monday = GetStartOfWeek(DateTime.Now, DayOfWeek.Monday);
        Tuesday = Monday.AddDays(1);
        Wednesday = Tuesday.AddDays(1);
        Thursday = Wednesday.AddDays(1);
        Friday = Thursday.AddDays(1);
        Saturday = Friday.AddDays(1);
        Sunday = Saturday.AddDays(1);
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
            var totalRevenueSunday = paymentsSunday.Sum(p => p.Fee);

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

    public async Task<Result<ReportDataTodayDto>> GetReportDataToday()
    {
        try
        {
            var paymentsToday = await _unitOfWork.GetRepository<Payment>().GetListAsync(
                    predicate: p => p.CreatedAt >= DateTime.Now && p.CreatedAt < DateTime.Now.AddDays(1)
                );
            var revenueToday = paymentsToday.Sum(x => x.Fee);
            var totalPaymentsToday = paymentsToday.Count();

            var profitsToday = await _unitOfWork.GetRepository<Payment>().GetListAsync(
                    selector: x => x.Fee * (double)10 / 100,
                   predicate: p => p.CreatedAt >= DateTime.Now && p.CreatedAt < DateTime.Now.AddDays(1)
               );

            var sumProfitToday = profitsToday.Sum();

            var reportData = new ReportDataTodayDto()
            {
                Revenue = revenueToday,
                TotalPayments = totalPaymentsToday,
                Profit = sumProfitToday,
            };
            return Success(reportData);
        }
        catch (Exception ex)
        {
            return Fail<ReportDataTodayDto>(ex.Message);
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

    public async Task<Result<ReportByMonthDto>> GetRevenueByMonth()
    {
        try
        {
            var paymentsJanuary = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt.Month == 1 && p.CreatedAt.Year == ThisYear);
            var totalRevenueJanuary = paymentsJanuary.Sum(p => p.Fee);

            var paymentsFebruary = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt.Month == 2 && p.CreatedAt.Year == ThisYear);
            var totalRevenueFebruary = paymentsFebruary.Sum(p => p.Fee);

            var paymentsMarch = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt.Month == 3 && p.CreatedAt.Year == ThisYear);
            var totalRevenueMarch = paymentsMarch.Sum(p => p.Fee);

            var paymentsApril = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt.Month == 4 && p.CreatedAt.Year == ThisYear);
            var totalRevenueApril = paymentsApril.Sum(p => p.Fee);

            var paymentsMay = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt.Month == 5 && p.CreatedAt.Year == ThisYear);
            var totalRevenueMay = paymentsMay.Sum(p => p.Fee);

            var paymentsJune = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt.Month == 6 && p.CreatedAt.Year == ThisYear);
            var totalRevenueJune = paymentsJune.Sum(p => p.Fee);

            var paymentsJuly = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt.Month == 7 && p.CreatedAt.Year == ThisYear);
            var totalRevenueJuly = paymentsJuly.Sum(p => p.Fee);

            var paymentsAugust = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt.Month == 8 && p.CreatedAt.Year == ThisYear);
            var totalRevenueAugust = paymentsAugust.Sum(p => p.Fee);

            var paymentsSeptember = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt.Month == 9 && p.CreatedAt.Year == ThisYear);
            var totalRevenueSeptember = paymentsSeptember.Sum(p => p.Fee);

            var paymentsOctober = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt.Month == 10 && p.CreatedAt.Year == ThisYear);
            var totalRevenueOctober = paymentsOctober.Sum(p => p.Fee);

            var paymentsNovember = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt.Month == 11 && p.CreatedAt.Year == ThisYear);
            var totalRevenueNovember = paymentsNovember.Sum(p => p.Fee);

            var paymentsDecember = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt.Month == 12 && p.CreatedAt.Year == ThisYear);
            var totalRevenueDecember = paymentsDecember.Sum(p => p.Fee);

            var reportData = new ReportByMonthDto()
            {
                DataInJanuary = totalRevenueJanuary,
                DataInFebruary = totalRevenueFebruary,
                DataInMarch = totalRevenueMarch,
                DataInApril = totalRevenueApril,
                DataInMay = totalRevenueMay,
                DataInJune = totalRevenueJune,
                DataInJuly = totalRevenueJuly,
                DataInAugust = totalRevenueAugust,
                DataInSeptember = totalRevenueSeptember,
                DataInOctober = totalRevenueOctober,
                DataInNovember = totalRevenueNovember,
                DataInDecember = totalRevenueDecember,

            };
            return Success(reportData);
        }
        catch (Exception ex)
        {
            return Fail<ReportByMonthDto>(ex.Message);
        }
    }
}
