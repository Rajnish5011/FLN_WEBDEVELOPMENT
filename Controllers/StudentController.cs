using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto;
using ASPNetCoreFLN_APIs.Entities;
using ASPNetCoreFLN_APIs.Helper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepo;
        public StudentController(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        } 
        [HttpGet("GetStudentByClusterUDISECode/{ClusterUDISECode}/{currentPageNumber}/{pageSize}")]
        public async Task<IActionResult> GetStudentByClusterUDISECode(string ClusterUDISECode, int currentPageNumber, int pageSize)
        {
            int maxPageSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPageSize) ? pageSize : maxPageSize;
            int skip = (currentPageNumber) * pageSize;
            int take = pageSize;
            try
            {
                var data = await _studentRepo.GetStudentByClusterUDISECode(ClusterUDISECode, currentPageNumber, pageSize);
                int Totalcount = Convert.ToInt32(data.GroupBy(item => item.TotalRecords).First().Select(g => g.TotalRecords).First());
                List<StudentByClusterUdiseCode> allTodos = data.ToList<StudentByClusterUdiseCode>().ToList();

                var result = new PagingResponseModel<List<StudentByClusterUdiseCode>>(allTodos, Totalcount, currentPageNumber, pageSize);
                if (data.Count() == 0)
                    return StatusCode(200, new { status = false, message = "Student Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Student Found Successfully.", response = result });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetStudentDataBySchoolClassSectionID/{SchoolId}/{ClassId}/{SectionId}")]
        public async Task<IActionResult> GetStudentData(int SchoolId, byte ClassId, byte SectionId)
        {
            try
            {
                var data = await _studentRepo.GetStudentData(SchoolId, ClassId, SectionId);

                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Student(s) Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Student(s) Found Successfully ", response = data });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });
            }
        }

        //[HttpGet("GetStudentCountBySchoolClassSectionID/{SchoolId}/{ClassId}/{SectionId}")]
        //public async Task<IActionResult> GetStudentCountBySchoolClassSectionID(int SchoolId, byte ClassId, byte SectionId)
        //{
        //    try
        //    {
        //        int  Count = await _studentRepo.GetStudentCountBySchoolClassSectionID(SchoolId, ClassId, SectionId);

        //        if (Count == 0)
        //            return StatusCode(400, new { status = false, message = "Student Not Found!", response = new { TotalCount= Count } });
        //        else
        //            return StatusCode(200, new { status = true, message = "Student Count Found successfully ", response = new { TotalCount = Count } });
        //    }
        //    catch (Exception ex)
        //    {
        //        //log error
        //        return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });
        //    }
        //}
        [HttpGet("GetStudentCountByMentorSchoolVisitId/{MentorSchoolVisitId}")]
        public async Task<IActionResult> GetStudentCountByMentorSchoolVisitId(int MentorSchoolVisitId)
        {
            try
            {
                var data = await _studentRepo.GetStudentCountByMentorSchoolVisitId(MentorSchoolVisitId);
                if (data.ToList().Count==0)
                    return StatusCode(200, new { status = false, message = "Student Not Found!", response = data.FirstOrDefault() });
                else
                    return StatusCode(200, new { status = true, message = "Student Count Found successfully ", response = data.FirstOrDefault() });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetStudentListWithPeriodicAssessmentStatus/{PeriodicAssessmentScheduleId}/{SchoolId}/{ClassId}/{SectionId}")]
        public async Task<IActionResult> GetStudentListByPeriodicStatus(int PeriodicAssessmentScheduleId, int SchoolId, byte ClassId, byte SectionId)
        { 
            try
            {
                var data = await _studentRepo.GetStudentListByPeriodicStatus(PeriodicAssessmentScheduleId, SchoolId, ClassId, SectionId);

                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Student(s) Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Student(s) Found successfully ", response = data });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });
            }
        }

        [HttpGet("SpotAssessment/GetStudentListSpotAssessmentWiseBySchoolClassSectionId/{SchoolId}/{ClassId}/{SectionId}")]
        public async Task<IActionResult> GetStudentListSpotAssessmentWiseBySchoolClassSectionId(int SchoolId, byte ClassId, byte SectionId)
        {
            try
            {
                var data = await _studentRepo.GetStudentListSpotAssessmentWiseBySchoolClassSectionId(SchoolId, ClassId, SectionId);

                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Student(s) Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Student(s) Found Successfully.", response = data });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("SpotAssessment/GetStudentListSpotAssessmentWiseByMentorSchoolVisitId/{MentorSchoolVisitId}")]
        public async Task<IActionResult> GetStudentListSpotAssessmentWiseByMentorSchoolVisitId(int MentorSchoolVisitId)
        {
            try
            {
                var data = await _studentRepo.GetStudentListSpotAssessmentWiseByMentorSchoolVisitId(MentorSchoolVisitId);
                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Student(s) Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Student(s) Found Successfully.", response = data });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("SpotAssessment/GetStudentListMasteryStatusWiseByMentorSchoolVisitId/{MentorSchoolVisitId}")]
        public async Task<IActionResult> GetStudentMasteryStatusByMentorSchoolVisitId(int MentorSchoolVisitId)
        {
            try
            {
                var response = (await _studentRepo.GetStudentMasteryStatusByMentorSchoolVisitId(MentorSchoolVisitId)).ToList();
                var data = response.GroupBy(item => item.Competency_Id)
                  .Select(g => new
                  {
                      Competency_Id = g.Key,
                      Competency = g.Select(x => x.Competency).First(),

                      QuestionGroup = g.GroupBy(Qgrp => Qgrp.Question_Group_Instruction_Id).Select(h => new
                      {
                          Question_Group_Instruction_Id = h.Key,
                          Question_Group_Instruction = h.Select(x => x.Question_Group_Instruction).First(),
                          Mastery_Criteria = h.Select(x => x.Mastery_Criteria).First(),
                          Assessment_Week = h.Select(x => x.Assessment_Week).First(),

                          Class_Name = h.Select(x => x.Class_Name).First(),
                          Section_Name = h.Select(x => x.Section_Name).First(),
                          Subject_Name = h.Select(x => x.Subject_Name).First(),

                          Students = h.Select(item => new
                          {
                              Student_Id = item.Student_Id,
                              Srn_No = item.Srn_No,
                              Student_Name = item.Student_Name,
                              Total_Correct_Questions = item.Total_Correct_Questions,
                              Word_Read_Per_Minute= item.Word_Read_Per_Minute,
                              Mastery_Status = item.Mastery_Status
                          })
                      })
                  });
                if (response.Count == 0)
                    return StatusCode(200, new { status = false, message = "Student Not Found!", response = response });

                else
                    return StatusCode(200, new
                    {
                        status = true,
                        message = "Student(s) Mastery Status Found Successfully.",
                        response = data
                    });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }

        }
    }
}

