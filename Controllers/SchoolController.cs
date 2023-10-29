using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private ISchoolRepository _schoolRepo;

        public SchoolController(ISchoolRepository schoolRepo)
        {
            _schoolRepo = schoolRepo;
        }

        [HttpGet("GetClusterSchoolListByBlockId/{BlockId}")]
        public async Task<IActionResult> GetClusterSchoolListByBlockId(short BlockId)
        {
            try
            {
                var data = await _schoolRepo.GetClusterSchoolListByBlockId(BlockId);

                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "School(s) Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "School(s) Found Successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetSchoolListByClusterSchoolId/{ClusterSchoolId}")]
        public async Task<IActionResult> GetSchoolListByClusterSchoolId(int ClusterSchoolId)
        {
            try
            {
                var data = await _schoolRepo.GetSchoolListByClusterSchoolId(ClusterSchoolId);

                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "School(s) Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "School(s) Found Successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
        
        [HttpGet("GetSchoolListByClusterUDISECode/{ClusterUDISECode}")]
        public async Task<IActionResult> GetSchoolListByClusterSchoolUdiseCode(string ClusterUDISECode)
        {
            try
            {
                var data = await _schoolRepo.GetSchoolListByClusterSchoolUdiseCode(ClusterUDISECode);

                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "School List Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "School List Found Successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetSchoolListByMentorId/{MentorId}")]
        public async Task<IActionResult> GetSchoolByMentorId(int MentorId)
        {
            try
            {
                var data = await _schoolRepo.GetSchoolByMentorId(MentorId);

                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "School Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "School Found Successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpPost("Principal/TeacherToClassSectionAllocationSave")]
        public async Task<IActionResult> TeacherToClassSectionAllocationSave(AllocateTeacherToClassSectionSave AllocateTeacher)
        {
            try
            {
                var ReturnVal = await _schoolRepo.TeacherToClassSectionAllocationSave(AllocateTeacher);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Teacher Allocation done successfully!", response = 1 });
                
                else if (ReturnVal == -1)
                    return StatusCode(200, new { status = false, message = "Teacher Allocation already done!", response = 200 });                            
                
                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 200 });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Critical error!"+ex.Message, response = 500 });
            }
        }

        //[HttpPut("Principal/TeacherToClassSectionAllocationUpdate/{TeacherClassSectionId:int}")]
        [HttpPut("Principal/TeacherToClassSectionAllocationUpdate/{TeacherClassSectionId}")]
        public async Task<IActionResult> TeacherToClassSectionAllocationUpdate(int TeacherClassSectionId, AllocateTeacherToClassSectionUpdate UpdateAllocateTeacher)
        {
            try
            {
                var ReturnVal = await _schoolRepo.TeacherToClassSectionAllocationUpdate(TeacherClassSectionId, UpdateAllocateTeacher);
                if (ReturnVal == 2)
                    return StatusCode(200, new { status = true, message = "Teacher Allocation updated successfully!", response = 2 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 200 });
            }
            catch (Exception ex)
            {   
                return StatusCode(500, new { status = false, message = "Critical error!" + ex.Message, response = 500 });
            }
        }

        //[HttpDelete("DeleteTeacherClassSectionByTeacherClassSectionId/{TeacherClassSectionId:int}")]
        [HttpDelete("DeleteTeacherClassSectionByTeacherClassSectionId/{TeacherClassSectionId}")]
        public async Task<IActionResult> DeleteTeacherClassSection(int TeacherClassSectionId)
        {
            try
            {
                var ReturnVal = await _schoolRepo.DeleteTeacherClassSection(TeacherClassSectionId);
                if (ReturnVal == 3)
                    return StatusCode(200, new { status = true, message = "Teacher allocation deleted successfully.", response = 200 });
                else if (ReturnVal == -3)
                    return StatusCode(200, new { status = false, message = "This Teacher allocation does not exists!", response = 200 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid Allocation Id!", response = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
        [HttpGet("SearchSchoolByMentorSchoolCode/{MentorId}/{SchoolCode}")]
        public async Task<IActionResult> SearchSchoolforStateLevelMonitorByMentorIdSchoolCode(int MentorId, string SchoolCode)
        {
            try
            {
                var data = await _schoolRepo.SearchSchoolforStateLevelMonitorByMentorIdSchoolCode(MentorId, SchoolCode);

                if (data.First().ReturnValue == -1)
                    return StatusCode(200, new { status = false, message = "Monitor Not Authorized For This!", response = new List<object>() });
                else if (data.First().ReturnValue == -2)
                    return StatusCode(200, new { status = false, message = "Invalid School Code!", response = new List<object>() });
                else if (data.First().ReturnValue == 1)
                    return StatusCode(200, new { status = true, message = "School Found Successfully.", response = data });
                else
                    return StatusCode(200, new { status = false, message = "Unknown Error!", response = new List<object>() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }



    }
}

