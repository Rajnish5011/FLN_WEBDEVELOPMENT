using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Entities;
using System.Linq;
using ASPNetCoreFLN_APIs.Dto.Mentor;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using ASPNetCoreFLN_APIs.Helper;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using System.Text;
using Newtonsoft.Json;


namespace ASPNetFLN_TestAPI.Controllers
{
    [Route("api/Mentor")]
    [ApiController]

    public class MentorSchoolScheduleController : ControllerBase
    {
        private readonly IMentorSchoolScheduleRepository _mentorSchoolScheduleRepo;

        public MentorSchoolScheduleController(IMentorSchoolScheduleRepository mentorSchoolScheduleRepo)
        {
            _mentorSchoolScheduleRepo = mentorSchoolScheduleRepo;
        }

        [HttpPost("CreateMentorSchoolSchedule")]
        public async Task<IActionResult> CreateMentorSchoolSchedule(CreateMentorSchoolScheduleDto mentorSchoolSchedule)
        {
            try
            {
                var ReturnVal = await _mentorSchoolScheduleRepo.CreateMentorSchoolSchedule(mentorSchoolSchedule);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Schedule created Successfully", response = 1 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 200 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }

        [HttpPut("UpdateMentorSchoolSchedule/{MentorSchoolScheduleID}")]
        public async Task<ActionResult> UpdateMentorSchoolSchedule(int MentorSchoolScheduleID, UpdateMentorSchoolScheduleDto mentorSchoolSchedule)
        {
            try
            {
                var ReturnVal = await _mentorSchoolScheduleRepo.UpdateMentorSchoolSchedule(MentorSchoolScheduleID, mentorSchoolSchedule);

                if (ReturnVal == 2)
                    return StatusCode(200, new { status = true, message = "Schedule Updated Successfully", response = ReturnVal });

                else if (ReturnVal == -2)
                    return StatusCode(200, new { status = true, message = "Schedule Started Already, So Cannot be Updated!", response = ReturnVal });

                else
                    return StatusCode(200, new { status = false, message = "Cannot Enter Invalid data!", response = 200 });

            }
            catch (Exception)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Invalid data, Failed to update! ", response = 500 });
            }
        }

        [HttpPut("MentorSchoolScheduleVisitEnd/{MentorSchoolScheduleId}")]
        public async Task<ActionResult> MentorSchoolScheduleVisitEnd(int MentorSchoolScheduleId)
        {
            try
            {
                var ScheduleEnd = await _mentorSchoolScheduleRepo.MentorSchoolScheduleVisitEnd(MentorSchoolScheduleId);
                if (ScheduleEnd > 0)
                    return StatusCode(200, new { status = true, message = "Schedule Ended Successfully!", response = 1 });
                if (ScheduleEnd == -1)
                    return StatusCode(200, new { status = false, message = "Cannot end schedule, no any visit done!", response = 200 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 200 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed to End Schedule! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetMentorProfileByMentorID/{MentorID}")]
        public async Task<IActionResult> GetMentorProfileByMentorID(int MentorID)
        {
            try
            {
                var result = await _mentorSchoolScheduleRepo.GetMentorProfileByMentorID(MentorID);
                if (result == null)
                    return StatusCode(200, new { status = false, message = "Profile Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Profile Found Successfully.", response = result });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetMentorSchoolScheduleByMentorID/{MentorID}")]
        public async Task<IActionResult> GetMentorSchoolScheduleByMentorId(int MentorID)
        {
            try
            {
                var result = await _mentorSchoolScheduleRepo.GetMentorSchoolScheduleByMentorId(MentorID);
                if (result.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "School Schedule Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "School Schedule Found Successfully.", response = result });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetMentorMonthlyTarget/{MentorID}/{GetMonthFromDate}")]
        public async Task<IActionResult> MentorMonthlyTargetByMentorId(int MentorID, DateTime GetMonthFromDate)
        {
            try
            {
                var result = await _mentorSchoolScheduleRepo.MentorMonthlyTargetByMentorId(MentorID, GetMonthFromDate);
                if (result == null)
                    return StatusCode(200, new { status = false, message = "Record Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Record found Successfully.", response = result });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
    }
}
