using ASPNetCoreFLN_APIs.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Repository;

namespace ASPNetCoreFLN_APIs.Controllers
{
    [Route("api/Reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsRepository _reportsRepo;
        public ReportsController(IReportsRepository reportsRepoRepo)
        {
            _reportsRepo = reportsRepoRepo;
        }

        [HttpGet("Dashboard/GetVisitComplianceReport/{DistrictId}")]
        public async Task<IActionResult> GetVisitComplianceReport(short DistrictId, short BlockId, int ClusterSchoolId, int SchoolId)
        {
            try
            {
                var data = await _reportsRepo.GetVisitComplianceReport(DistrictId, BlockId, ClusterSchoolId, SchoolId);

                if (data.ToList().Count > 0)
                    return StatusCode(200, new { status = true, message = "Record(s) Found successfully.", response = data });
                else
                    return StatusCode(200, new { status = false, message = "Record(s) Not Found!", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
        [HttpGet("Dashboard/GetVisitComplianceReportDistrictWise/{GetMonthYear}")]
        public async Task<IActionResult> GetVisitComplianceReportDistrictWise(DateTime GetMonthYear)
        {
            try
            {
                var data = await _reportsRepo.GetVisitComplianceReportDistrictWise(GetMonthYear);

                if (data.ToList().Count > 0)
                    return StatusCode(200, new { status = true, message = "Record(s) Found successfully.", response = data });
                else
                    return StatusCode(200, new { status = false, message = "Record(s) Not Found!", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("Dashboard/GetClassroomObservationByMentorReport/{DistrictId}")]
        public async Task<IActionResult> GetClassroomObservationReport(short DistrictId, short BlockId, int ClusterSchoolId, int SchoolId)
        {
            try
            {
                var data = await _reportsRepo.GetClassroomObservationReport(DistrictId,BlockId,ClusterSchoolId,SchoolId);

                if (data.ToList().Count > 0)
                    return StatusCode(200, new { status = true, message = "Record(s) Found successfully.", response = data });
                else
                    return StatusCode(200, new { status = false, message = "Record(s) Not Found!", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
        
        [HttpGet("Dashboard/GetMentorWiseVisitSummaryByDate/{GetMonthYear}")]
        public async Task<IActionResult> GetMentorWiseVisitSummary(DateTime GetMonthYear)
        {
            try
            {
                var data = await _reportsRepo.GetMentorWiseVisitSummary(GetMonthYear);

                if (data.ToList().Count > 0)
                    return StatusCode(200, new { status = true, message = "Record(s) Found successfully.", response = data.ToList() });
                else
                    return StatusCode(200, new { status = false, message = "Record(s) Not Found!", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("Dashboard/GetMentorWiseVisitSummaryByMonth/{GetMonthYear}")]
        public async Task<IActionResult> GetMentorWiseSchoolVisitSummaryByMonth(DateTime GetMonthYear)
        {
            try
            {
                var data = await _reportsRepo.GetMentorWiseSchoolVisitSummaryByMonth(GetMonthYear);

                if (data.ToList().Count > 0)
                    return StatusCode(200, new { status = true, message = "Record(s) Found successfully.", response = data.ToList() });
                else
                    return StatusCode(200, new { status = false, message = "Record(s) Not Found!", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
        [HttpGet("Dashboard/GetClassroomObservationReportHindi/{GetMonthYearFromDate}")]
        public async Task<IActionResult> GetClassroomObservationReportHindi(DateTime GetMonthYearFromDate)
        {
            try
            {
                var data = await _reportsRepo.GetClassroomObservationReportHindi(GetMonthYearFromDate);

                if (data.ToList().Count > 0)
                    return StatusCode(200, new { status = true, message = "Record(s) Found successfully.", response = data.ToList() });
                else
                    return StatusCode(200, new { status = false, message = "Record(s) Not Found!", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
       
        [HttpGet("Dashboard/GetClassroomObservationReportMath/{GetMonthYearFromDate}")]
        public async Task<IActionResult> GetClassroomObservationReportMath(DateTime GetMonthYearFromDate)
        {
            try
            {
                var data = await _reportsRepo.GetClassroomObservationReportMath(GetMonthYearFromDate);

                if (data.ToList().Count > 0)
                    return StatusCode(200, new { status = true, message = "Record(s) Found successfully.", response = data.ToList() });
                else
                    return StatusCode(200, new { status = false, message = "Record(s) Not Found!", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("Dashboard/GetClassroomObservationSchoolWiseReportHindi/{GetMonthYearFromDate}")]
        public async Task<IActionResult> GetClassroomObservationSchoolWiseReportHindi(DateTime GetMonthYearFromDate)
        {
            try
            {
                var data = await _reportsRepo.GetClassroomObservationSchoolWiseReportHindi(GetMonthYearFromDate);

                if (data.ToList().Count > 0)
                    return StatusCode(200, new { status = true, message = "Record(s) Found successfully.", response = data.ToList() });
                else
                    return StatusCode(200, new { status = false, message = "Record(s) Not Found!", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("Dashboard/GetClassroomObservationSchoolWiseReportMath/{GetMonthYearFromDate}")]
        public async Task<IActionResult> GetClassroomObservationSchoolWiseReportMath(DateTime GetMonthYearFromDate)
        {
            try
            {
                var data = await _reportsRepo.GetClassroomObservationSchoolWiseReportMath(GetMonthYearFromDate);

                if (data.ToList().Count > 0)
                    return StatusCode(200, new { status = true, message = "Record(s) Found successfully.", response = data.ToList() });
                else
                    return StatusCode(200, new { status = false, message = "Record(s) Not Found!", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        } 
        
        [HttpGet("Dashboard/GetClassroomObservationPercentageReport/{GetMonthYearFromDate}")]
        public async Task<IActionResult> GetClassroomObservationPercentageReport(DateTime GetMonthYearFromDate, short DistrictId, short BlockId)
        {
            try
            {
                var data = await _reportsRepo.GetClassroomObservationPercentageReport(GetMonthYearFromDate, DistrictId, BlockId);

                if (data.ToList().Count > 0)
                    return StatusCode(200, new { status = true, message = "Record(s) Found successfully.", response = data.ToList() });
                else
                    return StatusCode(200, new { status = false, message = "Record(s) Not Found!", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("Dashboard/GetSchoolVisitPercentageReport/{GetMonthYearFromDate}")]
        public async Task<IActionResult> GetSchoolVisitPercentageReport(DateTime GetMonthYearFromDate, short DistrictId, short BlockId)
        {
            try
            {
                var data = await _reportsRepo.GetSchoolVisitPercentageReport(GetMonthYearFromDate, DistrictId, BlockId);

                if (data.ToList().Count > 0)
                    return StatusCode(200, new { status = true, message = "Record(s) Found successfully.", response = data.ToList() });
                else
                    return StatusCode(200, new { status = false, message = "Record(s) Not Found!", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("Dashboard/GetSchoolVisitPercentageReportByFilter/{GetMonthYearFromDate}")]
        public async Task<IActionResult> GetSchoolVisitPercentageReportByFilter(DateTime GetMonthYearFromDate, short DistrictId, short BlockId)
        {
            try
            {
                var data = await _reportsRepo.GetSchoolVisitPercentageReportByFilter(GetMonthYearFromDate, DistrictId, BlockId);

                if (data.ToList().Count > 0)
                    return StatusCode(200, new { status = true, message = "Record(s) Found successfully.", response = data.ToList() });
                else
                    return StatusCode(200, new { status = false, message = "Record(s) Not Found!", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("Dashboard/GetClassroomObservationPercentageReportByFilter/{GetMonthYearFromDate}")]
        public async Task<IActionResult> GetClassroomObservationPercentageReportByFilter(DateTime GetMonthYearFromDate, short DistrictId, short BlockId)
        {
            try
            {
                var data = await _reportsRepo.GetClassroomObservationPercentageReportByFilter(GetMonthYearFromDate, DistrictId, BlockId);

                if (data.ToList().Count > 0)
                    return StatusCode(200, new { status = true, message = "Record(s) Found successfully.", response = data.ToList() });
                else
                    return StatusCode(200, new { status = false, message = "Record(s) Not Found!", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
    }
}

