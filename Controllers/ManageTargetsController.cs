using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto;
using ASPNetCoreFLN_APIs.Dto.PeriodicAssessment;
using ASPNetCoreFLN_APIs.Entities;
using ASPNetCoreFLN_APIs.Helper;
using ASPNetCoreFLN_APIs.TeacherTrainingDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASPNetCoreFLN_APIs.Controllers
{
    [Route("api/ManageTargets")]
    [ApiController]
    public class ManageTargetsController : ControllerBase
    {
        private readonly IManageTargetsRepository _TargetsRepo;
        public ManageTargetsController(IManageTargetsRepository TargetsRepo)
        {
            _TargetsRepo = TargetsRepo;
        }

        [HttpPost("AddMonthlyMentorsTarget")]
        public async Task<IActionResult>AddMonthlyTargetForMentorOrMonitor(MonthlyTarget request)
        {
            try
            {
                var TargetId = await _TargetsRepo.AddMonthlyTargetForMentorOrMonitor(request);
                if (TargetId > 0)
                    return StatusCode(200, new { status = true, message = "Monthly Target saved successfully.", response = 1 });
                else if (TargetId == -1)
                    return StatusCode(200, new { status = false, message = "Target for this month/year already exists.", response = 1 });
                else if (TargetId == -2)
                    return StatusCode(200, new { status = false, message = "Please provide correct values to create target.", response = 1 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data entered!", response = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed! something went wrong." + ex.Message, response = 500 });
            }
        }
        
        [HttpPut("UpdateMonthlyMentorsTarget")]
        public async Task<IActionResult> UpdateMonthlyMentorsTarget(UpdateMonthlyTarget request)
        {
            try
            {
                var TargetId = await _TargetsRepo.UpdateMonthlyMentorsTarget(request);
                if (TargetId > 0)
                    return StatusCode(200, new { status = true, message = "Target updated successfully.", response = 1 });
                else if (TargetId == -1)
                    return StatusCode(200, new { status = false, message = "Invalid data! This record does not exist.", response = 1 });
                else if (TargetId == -2)
                    return StatusCode(200, new { status = false, message = "Please provide valid values and try again.", response = 1 });
                else if (TargetId == -3)
                    return StatusCode(200, new { status = false, message = "You are not authorised to update.", response = 1 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data entered!", response = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed! something went wrong." + ex.Message, response = 500 });
            }
        }
        [HttpGet("GetMonthlyMentorsTarget/{IsCurrentYear}/{RoleId}")]
        public async Task<IActionResult> GetMonthlyMentorsTarget(bool IsCurrentYear, [DefaultValue(0)] int RoleId)
        {
            try
            {
                var data = await _TargetsRepo.GetMonthlyMentorsTarget(IsCurrentYear, RoleId);
                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Target(s) Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Target(s) Found successfully ", response = data });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });
            }
        }
    }
}