using EduLingual.Domain.Common;
using EduLingual.Domain.Dtos.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLingual.Application.Service;

public interface IDashboardService
{
    Task<Result<ReportDataDto>> GetRevenueInWeek();
    Task<Result<ReportByMonthDto>> GetRevenueByMonth();
    Task<Result<ReportDataDto>> GetTeacherInWeek();
    Task<Result<ReportDataDto>> GetUserInWeek();
    Task<Result<ReportDataTodayDto>> GetReportDataToday();
}
