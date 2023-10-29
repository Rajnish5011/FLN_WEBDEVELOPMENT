using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Entities;
using ASPNetCoreFLN_APIs.Dto;
using System.Linq;
using ASPNetCoreFLN_APIs.Models;
using System.Collections.Generic;
using ASPNetCoreFLN_APIs.Helper;
using ASPNetCoreFLN_APIs.Dto.Mentor;

namespace ASPNetFLN_TestAPI.Controllers
{
    [Route("api/Mentor")]
    [ApiController]
    public class MentorSchoolScheduleVisitController : ControllerBase
    {
        private readonly IMentorSchoolScheduleVisitRepository _mentorSchoolScheduleRepo;

        public MentorSchoolScheduleVisitController(IMentorSchoolScheduleVisitRepository mentorSchoolScheduleRepo)
        {
            _mentorSchoolScheduleRepo = mentorSchoolScheduleRepo;
        }

        [HttpPost("CreateMentorSchoolScheduleVisit")]
        public async Task<IActionResult> CreateMentorSchoolScheduleVisit(MentorSchoolScheduleVisitDto mentorSchoolSchedule)
        {
            try
            {
                var createdScheduleVisitId = await _mentorSchoolScheduleRepo.CreateMentorSchoolScheduleVisit(mentorSchoolSchedule);

                if (createdScheduleVisitId > 0)
                    return StatusCode(200, new { status = true, message = "School Visit Done Successfully!", response = new { MentorSchoolScheduleVisitId = createdScheduleVisitId } });
                
                else if (createdScheduleVisitId == -1)
                    return StatusCode(200, new { status = false, message = "Cannot create Visit! Teacher School Allocation not done!", response = 200 });
                
                else if (createdScheduleVisitId == -2)
                    return StatusCode(200, new { status = false, message = "Cannot create Visit! Teacher class section allocation not done!", response = 200 });
            
                else if (createdScheduleVisitId == -3)
                    return StatusCode(200, new { status = false, message = "Cannot create Visit! No any student exists in this Class/Section!", response = 200 });

                else
                    return StatusCode(200, new { status = false, message = "School Visit failed!", response = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });
            }
        }

        [HttpPut("MentorSchoolClassVisitEnd/{MentorSchoolVisitId:int}")]
        public async Task<ActionResult> MentorSchoolClassVisitEnd(int MentorSchoolVisitId)
        {
            try
            {
                var ScheduleEnd = await _mentorSchoolScheduleRepo.MentorSchoolClassVisitEnd(MentorSchoolVisitId);
                if (ScheduleEnd == 2)
                    return StatusCode(200, new { status = true, message = "Class visit ended successfully", response = 2 });
                else if (ScheduleEnd == -2)
                    return StatusCode(200, new { status = false, message = "Cannot End Visit Because Spot assessment not done of min 5 students!", response = 200 });
                else if (ScheduleEnd == -3)
                    return StatusCode(200, new { status = false, message = "Cannot End Visit Because Spot assessment not done of PresentStudentCount!", response = 200 });
                else if (ScheduleEnd == -1)
                    return StatusCode(200, new { status = false, message = "Cannot end visit, No any Visit done!", response = 200 });
                else
                    return StatusCode(200, new { status = false, message = "Failed to end visit!", response = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });
            }
        }

        [HttpPut("UpdatePresentStudentCount/{MentorSchoolVisitId:int}")]
        public async Task<ActionResult> UpdatePresentStudentCount(int MentorSchoolVisitId, UpdatePresentStudentCountDto presentStudentCount)
        {
            try
            {
                var ReturnVal = await _mentorSchoolScheduleRepo.UpdatePresentStudentCount(MentorSchoolVisitId, presentStudentCount);

                if (ReturnVal == -3)
                    return StatusCode(200, new { status = false, message = "Cannot Updated Present Student Count, Visit already Completed!", response = ReturnVal });
                else if (ReturnVal == -1)
                    return StatusCode(200, new { status = false, message = "Cannot enter Male > Total Male!", response = ReturnVal });

                else if (ReturnVal == -2)
                    return StatusCode(200, new { status = false, message = "Cannot enter Female > Total Female!", response = ReturnVal });

                else 
                    return StatusCode(200, new { status = true, message = "Present Student Count Updated Successfully.", response = ReturnVal });

            }
            catch (Exception)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Invalid data, Failed to update! ", response = 500 });
            }
        }
        
        [HttpGet("GetMentorPastVisitByMentorSchoolScheduleId/{MentorSchoolScheduleId}")]
        public async Task<IActionResult> GetMentorPastVisitByMentorSchoolScheduleId(int MentorSchoolScheduleId)
        {
            try
            {
                var data = await _mentorSchoolScheduleRepo.GetMentorPastVisitByMentorSchoolScheduleId(MentorSchoolScheduleId);
                if (data.ToList().Count == 0)

                    return StatusCode(200, new { status = false, message = "Visit Not Found!!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Visit Found Successfully.", response = data });

            }
            catch (Exception ex) 
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetMentorPastVisitBySchoolId/{SchoolId}/{currentPageNumber}/{pageSize}")]
        public async Task<IActionResult> GetMentorPastVisitBySchoolId(int SchoolId, int currentPageNumber, int pageSize)
        {
            try
            {
                var data = await _mentorSchoolScheduleRepo.GetMentorPastVisitBySchoolId(SchoolId, currentPageNumber, pageSize);
                int Totalcount = 0;
                List<MentorPastVisit> allTodos = data.ToList<MentorPastVisit>().ToList();
                
                if (data.Count() == 0)
                {
                    var result = new PagingResponseModel<List<MentorPastVisit>>(allTodos, Totalcount, currentPageNumber, pageSize);
                    return StatusCode(200, new { status = false, message = "Visit Not Found!", response = result });
                }
                else
                {
                    Totalcount = Convert.ToInt32(data.GroupBy(item => item.TotalRecords).First().Select(g => g.TotalRecords).First());
                    var result = new PagingResponseModel<List<MentorPastVisit>>(allTodos, Totalcount, currentPageNumber, pageSize);
                    return StatusCode(200, new { status = true, message = "Visit Found Successfully.", response = result });
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetMentorPastVisitByMentorId/{MentorId}/{currentPageNumber}/{pageSize}")]
        public async Task<IActionResult> GetMentorPastVisitByMentorId(int MentorId, int currentPageNumber, int pageSize)
        {           
            try
            {
                int Totalcount = 0;
                var data = await _mentorSchoolScheduleRepo.GetMentorPastVisitByMentorId(MentorId, currentPageNumber, pageSize);
                List<MentorPastVisitByMentorID> allTodos = data.ToList<MentorPastVisitByMentorID>().ToList();
                if (data.Count() == 0)
                {                    
                    var result = new PagingResponseModel<List<MentorPastVisitByMentorID>>(allTodos, 0, currentPageNumber, pageSize);
                    return StatusCode(200, new { status = false, message = "Visit Not Found!", response = result });
                }
                else
                {
                    Totalcount = Convert.ToInt32(data.GroupBy(item => item.TotalRecords).First().Select(g => g.TotalRecords).First());
                    var result = new PagingResponseModel<List<MentorPastVisitByMentorID>>(allTodos, Totalcount, currentPageNumber, pageSize);

                    return StatusCode(200, new { status = true, message = "Visit Found Successfully.", response = result });
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
    }
}
