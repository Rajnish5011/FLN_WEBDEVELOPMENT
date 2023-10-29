using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Controllers
{
    [Route("api/Teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private ITeacherRepository _techerRepo;

        public TeacherController(ITeacherRepository techerRepo)
        {
            _techerRepo = techerRepo;
        }

        [HttpGet("GetBlockTeacherListByMentorID/{MentorID}")]
        public async Task<IActionResult> GetBlockTeacherListByMentorID(int MentorID)
        {
            try
            {
                var data = await _techerRepo.GetBlockTeacherListByMentorID(MentorID);
                var TeacherId = data.ToList().Select(x => x.Teacher_Id).First();

                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Teacher Not Found!", response = data });

                else if (data.ToList().Count==1 && TeacherId==0)
                    return StatusCode(200, new { status = false, message = "No any block allocated to this Mentor!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Teacher Found Successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }


        [HttpGet("GetTeacherByClusterSchoolCode/{ClusterSchoolCode}")]
        public async Task<IActionResult> GetTecherData(string ClusterSchoolCode)
        {
            try
            {
                var data = await _techerRepo.GetTeacherByClusterSchoolCode(ClusterSchoolCode);

                if (data.ToList().Count == 0)

                    return StatusCode(200, new { status = false, message = "Teacher Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Teacher Found Successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetTeacherListBySchoolID/{SchoolId}")]
        public async Task<IActionResult> GetTeacherListBySchoolId(int SchoolId)
        {
            try
            {
                var data = await _techerRepo.GetTeacherListBySchoolId(SchoolId);
                if (data.ToList().Count > 0)
                {
                    var response = data.GroupBy(item => item.School_Teacher_Id)
                 .Select(g => new
                 {
                     School_Teacher_Id = g.Key,
                     Teacher_Id = g.Select(x => x.Teacher_Id).First(),
                     Employee_Code = g.Select(x => x.Employee_Code).First(),
                     Employee_Name = g.Select(x => x.Employee_Name).First(),
                     Designation = g.Select(x => x.Designation).First(),

                     Allocation = g.Select(item => new
                     {
                         Teacher_Class_Section_Id = item.Teacher_Class_Section_Id,
                         Class_Id = item.Class_Id,
                         Class_Name = item.Class_Name,
                         Section_Id = item.Section_Id,
                         Section_Name = item.Section_Name
                     })
                 });
                    return StatusCode(200, new { status = true, message = "Teacher Found Successfully", response = response });
                }
                else
                    return StatusCode(200, new { status = false, message = "Teacher Not Found!", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Critical error! "+ ex.Message, response = 500 });
            }
        }

        [HttpGet("GetTeacherBySchoolClassSectionId/{SchoolId}/{ClassId}/{SectionId}")]
        public async Task<IActionResult> GetTeacherBySchoolClassSection(int SchoolId, short ClassId, short SectionId)
        {
            try
            {
                var result = await _techerRepo.GetTeacherBySchoolClassSection(SchoolId, ClassId, SectionId);
                if (result.ToList().Count == 0)

                    return StatusCode(200, new { status = false, message = "Teacher Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Teacher Found successfully.", response = result });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetMentorFeedBackToTeacher/{TeacherId}")]
        public async Task<IActionResult> GetMentorFeedBackToTeacher(int TeacherId)
        {
            try
            {
                var result = await _techerRepo.GetMentorFeedBackToTeacher(TeacherId);
                if (result.ToList().Count == 0)

                    return StatusCode(200, new { status = false, message = "Feedback Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Feedback Found Successfully.", response = result });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetTeacherProfileByTeacherId/{TeacherId}")]
        public async Task<IActionResult> GetTeacherProfileByTeacherId(int TeacherId)
        {
            try
            {
                var result = await _techerRepo.GetTeacherProfileByTeacherId(TeacherId);
                if (result == null)

                    return StatusCode(200, new { status = false, message = "Profile Not Fond", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Profile Found Successfully.", response = result });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetTeacherByEmployeeCode/{EmployeeCode}")]
        public async Task<IActionResult> GetTeacherByEmployeeCode(string EmployeeCode)
        {
            try
            {
                var result = await _techerRepo.GetTeacherByEmployeeCode(EmployeeCode);
                if (result == null)

                    return StatusCode(200, new { status = false, message = "Teacher Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Teacher Found successfully.", response = result });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("ClusterMeeting/GetClusterTeacherMeetingScheduleByTeacherId/{TeacherId}")]
        public async Task<IActionResult> GetClusterTeacherMeetingScheduleByTeacherId(int TeacherId)
        {
            try
            {
                var result = await _techerRepo.GetClusterTeacherMeetingScheduleByTeacherId(TeacherId);
                if (result.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Meeting Schedule Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Meeting Schedule Found Successfully.", response = result });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
        [HttpGet("GetBlockTeacherListByBlockID/{BlockID}")]
        public async Task<IActionResult> GetBlockTeacherListByBlockID(int BlockID)
        {
            try
            {
                var data = await _techerRepo.GetBlockTeacherListByBlockID(BlockID);

                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Teacher Not Found!", response = data });

                else
                    return StatusCode(200, new { status = true, message = "Teacher Found Successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
        [HttpGet("GetTeacherListBySrnNo/{Srn_No}")]
        public async Task<IActionResult> GetTeacherListBySrnNo(string Srn_No)
        {
            try
            {
                var result = await _techerRepo.GetTeacherListBySrnNo(Srn_No);
                if (result.ToList().Count == 0)

                    return StatusCode(200, new { status = false, message = "Teacher Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Teacher Found successfully.", response = result });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

    }
}
