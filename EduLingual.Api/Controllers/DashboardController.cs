using EduLingual.Application.Service;
using EduLingual.Domain.Common;
using EduLingual.Domain.Constants;
using EduLingual.Domain.Dtos.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduLingual.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : BaseController<DashboardController>
    {
        private readonly IDashboardService _dashboardSerivce;
        public DashboardController(ILogger<DashboardController> logger, IDashboardService dashboardService) : base(logger)
        {
            _dashboardSerivce = dashboardService;
        }
        [HttpGet(ApiEndPointConstant.Dashboard.DashboardFinance)]
        [ProducesResponseType(typeof(Result<ReportDataDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<ReportDataDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardFinance()
        {
            var result = await _dashboardSerivce.GetRevenueInWeek();
            return Ok(result);
        }
        [HttpGet(ApiEndPointConstant.Dashboard.DashboardTeacher)]
        [ProducesResponseType(typeof(Result<ReportDataDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<ReportDataDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardTeacher()
        {
            var result = await _dashboardSerivce.GetTeacherInWeek();
            return Ok(result);
        }
        [HttpGet(ApiEndPointConstant.Dashboard.DashboardUser)]
        [ProducesResponseType(typeof(Result<ReportDataDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<ReportDataDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardUser()
        {
            var result = await _dashboardSerivce.GetUserInWeek();
            return Ok(result);
        }

        [HttpGet(ApiEndPointConstant.Dashboard.DashdoardEndpoint)]
        [ProducesResponseType(typeof(Result<ReportDataTodayDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<ReportDataTodayDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTotalDashboard()
        {
            var result = await _dashboardSerivce.GetReportDataToday();
            return Ok(result);
        }

        [HttpGet(ApiEndPointConstant.Dashboard.DashboardFinanceByMonth)]
        [ProducesResponseType(typeof(Result<ReportDataTodayDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<ReportDataTodayDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRevenueByMonth()
        {
            var result = await _dashboardSerivce.GetRevenueByMonth();
            return Ok(result);
        }
    }
}
