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
    private int ThisMonth = DateTime.Now.Month;
    private int ThisYear = DateTime.Now.Year;
    private int LastMonth = DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1;
    private int YearForLastMonth = DateTime.Now.Month == 1 ? DateTime.Now.Year - 1 : DateTime.Now.Year;

    public DashboardService(IUnitOfWork.IUnitOfWork<ApplicationDbContext> unitOfWork, ILogger<ReportDataDto> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public Task<Result<ReportDataDto>> GetExamInMonth()
    {
        // TODO: Get exam for this month
        throw new NotImplementedException();
    }

    public async Task<Result<ReportDataDto>> GetFinanceInMonth()
    {
        try
        {
            var listPaymentCurrent = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt.Month == ThisMonth && p.CreatedAt.Year == ThisYear);
            var totalFinanceCurrent = listPaymentCurrent.Sum(p => p.Fee);

            var listPaymentLast = await _unitOfWork.GetRepository<Payment>().GetListAsync(predicate: p => p.CreatedAt.Month == LastMonth  && p.CreatedAt.Year == YearForLastMonth);
            var totalFinaceLast = listPaymentLast.Sum(p => p.Fee);

            var reportData = new ReportDataDto()
            {
                DataThisMonth = totalFinanceCurrent,
                DataLastMonth = totalFinaceLast,
            };
            return Success(reportData);
        } catch (Exception ex)
        {
            return Fail<ReportDataDto>(ex.Message);
        }
    }

    public async Task<Result<ReportDataDto>> GetTeacherInMonth()
    {
        try
        {
            string roleName = Enum.GetName(typeof(RoleEnum), RoleEnum.Teacher);
            var listTeacherCurrent = await _unitOfWork.GetRepository<User>().GetListAsync(include: u => u.Include(u => u.Role), predicate: t => t.CreatedAt.Month == ThisMonth && t.CreatedAt.Year == ThisYear && t.Role.RoleName == roleName);
            var numberTeacherCurrent = listTeacherCurrent.Count;

            var listTeacherLast = await _unitOfWork.GetRepository<User>().GetListAsync(include: u => u.Include(u => u.Role), predicate: t => t.CreatedAt.Month == LastMonth && t.CreatedAt.Year == YearForLastMonth && t.Role.RoleName == roleName);
            var numberTeacherLast = listTeacherLast.Count;
            var reportData = new ReportDataDto()
            {
                DataThisMonth = numberTeacherCurrent,
                DataLastMonth = numberTeacherLast,
            };
            return Success(reportData);
        } catch (Exception ex)
        {
            return Fail<ReportDataDto>(ex.Message);
        }
    }

    public async Task<Result<ReportDataDto>> GetUserInMonth()
    {
        try
        {
            string roleName = Enum.GetName(typeof(RoleEnum), RoleEnum.User);
            var listUserCurrent = await _unitOfWork.GetRepository<User>().GetListAsync(include: u => u.Include(u => u.Role), predicate: t => t.CreatedAt.Month == ThisMonth && t.CreatedAt.Year == ThisYear && t.Role.RoleName == roleName);
            var numberUserCurrent = listUserCurrent.Count;

            var listUserLast = await _unitOfWork.GetRepository<User>().GetListAsync(include: u => u.Include(u => u.Role), predicate: t => t.CreatedAt.Month == LastMonth && t.CreatedAt.Year == YearForLastMonth && t.Role.RoleName == roleName);
            var numberUserLast = listUserLast.Count;
            var reportData = new ReportDataDto()
            {
                DataThisMonth = numberUserCurrent,
                DataLastMonth = numberUserLast,
            };
            return Success(reportData);
        }
        catch (Exception ex)
        {
            return Fail<ReportDataDto>(ex.Message);
        }
    }
}
