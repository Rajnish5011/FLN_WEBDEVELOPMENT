using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Entities;
using System.Linq;
using ASPNetCoreFLN_APIs.Dto.Mentor;
using static ASPNetCoreFLN_APIs.Repository.MentorClassObervationRepository;
using Azure;
using Dapper;
using System.Collections.Generic;
using System.Data;

namespace ASPNetFLN_TestAPI.Controllers
{
    [Route("api/Mentor")]
    [ApiController]
    
    public class MentorClassObservationController : ControllerBase
    {
        private readonly IMentorClassObservationRepository _mentorSchoolScheduleRepo;

        public MentorClassObservationController(IMentorClassObservationRepository mentorSchoolScheduleRepo)
        {
            _mentorSchoolScheduleRepo = mentorSchoolScheduleRepo;
        }

        [HttpPost("CreateMentorClassObervation")]
        public async Task<IActionResult> CreateMentorClassObervation(MentorClassObervationDto mentorObservation)
        {
            try
            {
                var TeacherObservationId = await _mentorSchoolScheduleRepo.CreateMentorClassObervation(mentorObservation);
                if (TeacherObservationId > 0)
                    return StatusCode(200, new { status = true, message = "Class observation done successfully", response = 1 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data entered!", response = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetClassObservationByMentorSchoolVisitId/{MentorSchoolVisitId}")]
        public async Task<IActionResult> GetClassObservationByMentorSchoolVisitId(int MentorSchoolVisitId)
        {
             try
            {
                var response = (await _mentorSchoolScheduleRepo.GetClassObservationByMentorSchoolVisitId(MentorSchoolVisitId)).ToList();
                var data = response.GroupBy(item => item.Mentor_School_Visit_Id)
                  .Select(g => new
                  {
                      Mentor_School_Visit_Id = g.Key,
                      Mentor_Class_Teacher_Observed_Id = g.Select(x => x.Mentor_Class_Teacher_Observed_Id).First(),
                      Mentor_Id = g.Select(x => x.Mentor_Id).First(),
                      Teacher_Id = g.Select(x => x.Teacher_Id).First(),
                      Class_Id = g.Select(x => x.Class_Id).First(),
                      Section_Id = g.Select(x => x.Section_Id).First(),
                      Subject_Id = g.Select(x => x.Subject_Id).First(),
                      School_Name = g.Select(x => x.School_Name).First(),
                      Teacher_Name = g.Select(x => x.Teacher_Name).First(),
                      Class_Name = g.Select(x => x.Class_Name).First(),
                      Section_Name = g.Select(x => x.Section_Name).First(),
                      Subject_Name = g.Select(x => x.Subject_Name).First(),
                      Created_On = g.Select(x => x.Created_On).First(),
                      Is_ObservationDone = g.Select(x => x.Is_ObservationDone).First(),
                      Total_Student_Count = g.Select(x => x.Total_Student_Count).First(),
                      Total_Spot_Done = g.Select(x => x.Total_Spot_Done).First(),
                      Time_Taken = g.Select(x => x.Time_Taken).First(),
                      Feedback_Date = g.Select(x => x.Feedback_Date).First(),
                      Observation_Feedback = g.Select(x => x.Observation_Feedback).First(),
                      Observation_Remark = g.Select(x => x.Observation_Remark).First(),
                      Mentor_Name = g.Select(x => x.Mentor_Name).First(),
                      Present_Student_Count = g.Select(x => x.Present_Student_Count).First(),
                      StudentSpotAssessment= g.Select(item => new
                      {
                          Student_Id = item.Student_Id,
                          Student_Name = item.Student_Name,
                          Srn_No = item.Srn_No
                          //Word_Read_Per_Minute = item.Word_Read_Per_Minute
                      })
                  });
                if (response.Count==0)
                    return StatusCode(200, new { status = false, message = "Class Observation Not Found!", response = data.FirstOrDefault() });
                else
                    return StatusCode(200, new
                    {
                        status = true,
                        message = "Class Observation Found Successfully.",
                        response = data.FirstOrDefault()
                    });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetClassObservationQuestion")]
        public async Task<IActionResult> GetClassObservationQuestion()
        {
            try
            {
                var result = (await _mentorSchoolScheduleRepo.GetClassObservationQuestion()).ToList();
                if (result.Count == 0)
                    return StatusCode(200, new { status = false, message = "Observation Question Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Observation Question Found Successfully!", response = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetClassObservationQuestions/{MentorId}/{SubjectId}")]
        public async Task<IActionResult> GetClassObservationQuestions(int MentorId, byte SubjectId)
        {
            try
            {
                var response = (await _mentorSchoolScheduleRepo.GetClassObservationQuestions(MentorId, SubjectId)).ToList();              
                var data = response.GroupBy(Ins => Ins.Question_Scope_Id).Select(selector: g => new
                {
                    Question_Scope_Id = g.Key,
                    Question_Scope = g.Select(x => x.Question_Scope).FirstOrDefault(),

                    Questions = g.GroupBy(Qitem => Qitem).Select(selector: Q => new                    
                    {
                        Observation_Question_Id = Q.Key,
                        Observation_Question = Q.Select(hx => hx.Observation_Question.ToString()).FirstOrDefault().ToString(),
                        Question_Number = Q.Select(hx => hx.Question_Number).FirstOrDefault().ToString(),
                        Response_Type = Q.Select(hx => hx.Response_Type).FirstOrDefault().ToString(),
                        Is_Multiple_Choice = Q.Select(hx => hx.Is_Multiple_Choice).FirstOrDefault(),
                        Is_Dependent = Q.Select(hx => hx.Is_Dependent).FirstOrDefault(),                       
                        //Dependent_Question_ID = Q.Select(hx => hx.Dependent_Question_ID).FirstOrDefault(),
                        //Dependent_Question_Option_Id = Q.Select(hx => hx.Dependent_Question_Option_Id).FirstOrDefault(),
                        Is_Multiple_Dependent_Option = Q.Select(hx => hx.Is_Multiple_Dependent_Option).FirstOrDefault(),
                        //Multiple_Dependent_Options = Q.Select(hx => hx.Multiple_Dependent_Options).FirstOrDefault().ToString(),
                        
                        QuestionsOptions = Q.Select(optitem => new
                        {                            
                            Observation_Question_Option_Id = optitem.Observation_Question_Option_Id,
                            Observation_Question_Option = optitem.Observation_Question_Option.ToString(),
                            Is_Only_Selectable = optitem.Is_Only_Selectable,
                            Is_Open_Questions = optitem.Is_Open_Questions,
                            Is_Error_Message_Required = optitem.Is_Error_Message_Required,
                            //Error_Message = optitem.Error_Message.ToString(),

                        }).ToList()
                    })
                }).ToList();
                 //});

                if (response.Count == 0)
                    return StatusCode(200, new { status = false, message = "Questions Not Found!", response = response });

                else
                    return StatusCode(200, new
                    {
                        status = true,
                        message = "Questions Found Successfully.",
                        response = data
                    });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetMentorTeacherClassObservationFeedBackToView/{Mentor_Id}/{SelectedOptionIDs}")]
        public async Task<IActionResult> GetMentorClassTeacherObservationFeedBack(int Mentor_Id, string SelectedOptionIDs)
        {
            try
            {
                var result = (await _mentorSchoolScheduleRepo.GetMentorClassTeacherObservationFeedBack(Mentor_Id, SelectedOptionIDs)).ToList();
                if (result.Count == 0)
                    return StatusCode(200, new { status = false, message = "No Feedback Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Feedback Found Successfully!", response = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
    }
}
