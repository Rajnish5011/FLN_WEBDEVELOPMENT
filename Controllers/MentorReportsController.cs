using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace ASPNetCoreFLN_APIs.Controllers
{
    [Route("api/MentorReports")]
    [ApiController]
    public class MentorReportsController : ControllerBase
    {
        private IMentorReportRepository _mentorReport;
        public MentorReportsController(IMentorReportRepository mentorReport)
        {
            _mentorReport = mentorReport;
        }

        [HttpGet("GetMentorMonthlyReports")]
        public async Task<IActionResult> StudentAttendanceByTeacherSave(DateTime month_Year, string monitor_Id)
        {
            try
            {
                var ReturnVal = await _mentorReport.GetMentorMonthlyReports(month_Year, monitor_Id);
                return StatusCode(200, new { status = true, message = "Percentage Found", response = ReturnVal });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Critical error occured! " + ex.Message, response = 500 });
            }
        }
    }
}
