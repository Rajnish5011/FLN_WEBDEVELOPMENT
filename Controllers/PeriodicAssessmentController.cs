using ASPNetCoreFLN_APIs.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using ASPNetCoreFLN_APIs.Dto.PeriodicAssessment;

namespace ASPNetCoreFLN_APIs.Controllers
{
    [Route("api/PeriodicAssessment/[controller]")]
    [ApiController]
    public class PeriodicAssessmentController : ControllerBase
    {
        private readonly IPeriodicAssessmentRepository _periodicAssessmentRepo;
        public PeriodicAssessmentController(IPeriodicAssessmentRepository periodicAssessmentRepo)
        {
            _periodicAssessmentRepo = periodicAssessmentRepo;
        }

        [HttpGet("GetLastPeriodicAssessment")]
        public async Task<IActionResult> GetPeriodicAssessmentSchedule()
        {
            try
            {
                var response = (await _periodicAssessmentRepo.GetPeriodicAssessmentSchedule()).ToList();
                var data = response.GroupBy(item => item.Periodic_Assessment_Schedule_Id)
                  .Select(g => new
                  {
                      Periodic_Assessment_Schedule_Id = g.Key,
                      Periodic_Assessment_Id = g.Select(x => x.Periodic_Assessment_Id).First(),
                      Class_Id = g.Select(x => x.Class_Id).First(),
                      Subject_Id = g.Select(x => x.Subject_Id).First(),
                      Class_Name = g.Select(x => x.Class_Name).First(),
                      Subject_Name = g.Select(x => x.Subject_Name).First(),
                      Number_Of_Questions = g.Select(x => x.Number_Of_Questions).First(),
                      StartDate = g.Select(x => x.Start_Date).First(),
                      EndDate = g.Select(x => x.End_Date).First(),
                      Assessment_Type = g.Select(x => x.Assessment_Type).First(),
                      SheduleCompetancy = g.Select(item => new
                      {
                          Periodic_Assessment_Schedule_Competancy_Id = item.Periodic_Assessment_Schedule_Competancy_Id,
                          Competancy_Id = item.Competancy_Id,
                          Competancy = item.Competancy,
                      })
                  });
                if (response.Count == 0)
                    return StatusCode(200, new { status = false, message = "Assessment Not Found!", response = data });
                else
                    return StatusCode(200, new
                    {
                        status = true,
                        message = "Periodic Assessment Found.",
                        response = data
                    });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetPeriodicAssessmentQuestions/{PeriodicAssessmentId}/{ClassId}")]
        public async Task<IActionResult> GetPeriodicAssessmentQuestions(byte PeriodicAssessmentId, byte ClassId)
        {
            try
            {
                var response = (await _periodicAssessmentRepo.GetPeriodicAssessmentQuestions(PeriodicAssessmentId, ClassId)).ToList();
                var data = response.GroupBy(item => item.Question_Id)
                  .Select(g => new
                  {
                      Question_Id = g.Key,                      
                      Question_Type_Id = g.Select(x => x.Question_Type_Id).First(),
                      Assessment_Type_Id = g.Select(x => x.Assessment_Type_Id).First(),                      
                      Question_Text = g.Select(x => x.Question_Text).First(),
                      Question_Instruction = g.Select(x => x.Question_Instruction).First(),
                      Assessment_Type = g.Select(x => x.Assessment_Type).First(),
                      Question_Type = g.Select(x => x.Question_Type).First(),
                      Media_Type = g.Select(x => x.Media_Type).First(),
                      Media_Url = g.Select(x => x.Media_Url).First(),
                      Base64QuestionImage = _periodicAssessmentRepo.GetBase64StringImageByPath(g.Select(x => x.Media_Type).First(), g.Select(x => x.Media_Url).First()),
                      Is_Draggable = g.Select(x => x.Is_Draggable).First(),                      
                      Option_Media_Type = g.Select(x => x.Option_Media_Type).First(),

                      QuestionsOptions = g.Select(item => new
                      {
                          Base64OptionImage = _periodicAssessmentRepo.GetBase64StringImageByPath(g.Select(x => x.Option_Media_Type).First(), Convert.ToString(item.Option_Media_Url.ToString())),
                          Question_Option_Id = item.Question_Option_Id,
                          Option_Text = item.Option_Text,
                          Option_Media_Url = Convert.ToString(item.Option_Media_Url),
                          Is_Correct = item.Is_Correct                          
                      })
                  });
                
                if (response.Count == 0)
                    return StatusCode(200, new { status = false, message = "Questions Not Found!", response = data });
                else
                    return StatusCode(200, new
                    {
                        status = true,
                        message = "Assessment Questions Found.", 
                        response = data
                    });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
        [HttpGet("GetPeriodicAssessmentQuestions/{PeriodicAssessmentId}")]
        public async Task<IActionResult> GetPeriodicAssessmentQuestionsByPeriodicAssessmentId(byte PeriodicAssessmentId)
        {
            try
            {
                var response = (await _periodicAssessmentRepo.GetPeriodicAssessmentQuestionsByPeriodicAssessmentId(PeriodicAssessmentId)).ToList();

                if (response.Count == 0)
                    return StatusCode(200, new { status = false, message = "Questions Not Found!", response = new List<object>() });

                var groupedQuestions = response
                    .GroupBy(item => new { item.Competency_Id, item.Competancy })
                    .Select(competencyGroup => new
                    {
                        Competency_Id = competencyGroup.Key.Competency_Id,
                        Competency = competencyGroup.Key.Competancy,
                        Questions = competencyGroup
                            .GroupBy(question => question.Question_Id)
                            .Select(questionGroup => new
                            {
                                Question_Id = questionGroup.Key,
                                Question_Type_Id = questionGroup.First().Question_Type_Id,
                                Assessment_Type_Id = questionGroup.First().Assessment_Type_Id,
                                Question_Text = questionGroup.First().Question_Text,
                                Question_Instruction = questionGroup.First().Question_Instruction,
                                Assessment_Type = questionGroup.First().Assessment_Type,
                                Question_Type = questionGroup.First().Question_Type,
                                Media_Type = questionGroup.First().Media_Type,
                                Media_Url = questionGroup.First().Media_Url,
                                Base64QuestionImage = _periodicAssessmentRepo.GetBase64StringImageByPath(questionGroup.First().Media_Type, questionGroup.First().Media_Url),
                                Is_Draggable = questionGroup.First().Is_Draggable,
                                Option_Media_Type = questionGroup.First().Option_Media_Type,
                                Marks = questionGroup.First().Marks,
                                QuestionsOptions = questionGroup.Select(item => new
                                {
                                    Base64OptionImage = _periodicAssessmentRepo.GetBase64StringImageByPath(item.Option_Media_Type, item.Option_Media_Url),
                                    Question_Option_Id = item.Question_Option_Id,
                                    Option_Text = item.Option_Text,
                                    Option_Media_Url = item.Option_Media_Url.ToString(),
                                    Is_Correct = item.Is_Correct
                                })
                            })
                    });

                return StatusCode(200, new
                {
                    status = true,
                    message = "Assessment Questions Found.",
                    response = groupedQuestions
                });
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
        [HttpGet("GetPeriodicAssessmentQuestions/{PeriodicAssessmentId}/{ClassId}/{SubjectId}/{CompetencyId}")]
        public async Task<IActionResult> GetPeriodicQuestionByAssessmentClassSubjectCompetencyId(int PeriodicAssessmentId, int ClassId, int SubjectId, Int16 CompetencyId)
        {
            try
            {
                var response = (await _periodicAssessmentRepo.GetPeriodicQuestionByAssessmentClassSubjectCompetencyId(PeriodicAssessmentId, ClassId,SubjectId, CompetencyId)).ToList();

                if (response.Count == 0)
                    return StatusCode(200, new { status = false, message = "Questions Not Found!", response = new List<object>() });

                var groupedQuestions = response
                    .GroupBy(item => new { item.Competency_Id, item.Competancy })
                    .Select(competencyGroup => new
                    {
                        Competency_Id = competencyGroup.Key.Competency_Id,
                        Competency = competencyGroup.Key.Competancy,
                        Questions = competencyGroup
                            .GroupBy(question => question.Question_Id)
                            .Select(questionGroup => new
                            {
                                Question_Id = questionGroup.Key,
                                Question_Type_Id = questionGroup.First().Question_Type_Id,
                                Assessment_Type_Id = questionGroup.First().Assessment_Type_Id,
                                Question_Text = questionGroup.First().Question_Text,
                                Question_Instruction = questionGroup.First().Question_Instruction,
                                Assessment_Type = questionGroup.First().Assessment_Type,
                                Question_Type = questionGroup.First().Question_Type,
                                Media_Type = questionGroup.First().Media_Type,
                                Media_Url = questionGroup.First().Media_Url,
                                Base64QuestionImage = _periodicAssessmentRepo.GetBase64StringImageByPath(questionGroup.First().Media_Type, questionGroup.First().Media_Url),
                                Is_Draggable = questionGroup.First().Is_Draggable,
                                Option_Media_Type = questionGroup.First().Option_Media_Type,
                                Marks = questionGroup.First().Marks,
                                QuestionsOptions = questionGroup.Select(item => new
                                {
                                    Base64OptionImage = _periodicAssessmentRepo.GetBase64StringImageByPath(item.Option_Media_Type, item.Option_Media_Url),
                                    Question_Option_Id = item.Question_Option_Id,
                                    Option_Text = item.Option_Text,
                                    Option_Media_Url = item.Option_Media_Url.ToString(),
                                    Is_Correct = item.Is_Correct
                                })
                            })
                    });

                return StatusCode(200, new
                {
                    status = true,
                    message = "Assessment Questions Found.",
                    response = groupedQuestions
                });
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
        [HttpGet("GetPeriodicAssessmentQuestionsBySchedule/{PeriodicAssessmentScheduleId}")]
        public async Task<IActionResult> GetPeriodicAssessmentQuestionsByPeriodicAssessmentScheduleId(int PeriodicAssessmentScheduleId)
        {
            try
            {
                var response = (await _periodicAssessmentRepo.GetPeriodicAssessmentQuestionsByPeriodicAssessmentScheduleId(PeriodicAssessmentScheduleId)).ToList();

                if (response.Count == 0)
                    return StatusCode(200, new { status = false, message = "Questions Not Found!", response = new List<object>() });

                var groupedQuestions = response.GroupBy(item => new { item.Competency_Id, item.Competancy });

                var result = groupedQuestions.Select(group =>
                {
                    var firstItem = group.First();
                    return new
                    {
                        Competency_Id = firstItem.Competency_Id,
                        Competency_1 = firstItem.Competancy,
                        Questions = group.Select(q => new
                        {
                            Question_Id = q.Question_Id,
                            Question_Text = q.Question_Text,
                            Question_Instruction = q.Question_Instruction,
                            Assessment_Type_Id = q.Assessment_Type_Id,
                            Question_Type_Id = q.Question_Type_Id,
                            Media_Type = q.Media_Type,
                            Media_Url = q.Media_Url,
                            Base64QuestionImage = _periodicAssessmentRepo.GetBase64StringImageByPath(q.Media_Type, q.Media_Url),
                            Is_Draggable = q.Is_Draggable,
                            Option_Media_Type = q.Option_Media_Type,
                            Marks = q.Marks,
                            QuestionsOptions = group.Where(optitem => optitem.Question_Id == q.Question_Id).Select(opt => new
                            {
                                Base64OptionImage = _periodicAssessmentRepo.GetBase64StringImageByPath(opt.Option_Media_Type, opt.Option_Media_Url),
                                Question_Option_Id = opt.Question_Option_Id,
                                Option_Text = opt.Option_Text,
                                Option_Media_Url = opt.Option_Media_Url,
                                Is_Correct = opt.Is_Correct
                            })
                        })
                    };
                });

                return StatusCode(200, new
                {
                    status = true,
                    message = "Assessment Questions Found.",
                    response = result
                });
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpPost("CreatePeriodicAssessmentSchedule")]
        public async Task<IActionResult> CreatePeriodicAssessmentSchedule(PeriodicAssessmentScheduleDto request)
        {
            try
            {
                var createdSchedule = await _periodicAssessmentRepo.CreatePeriodicAssessmentSchedule(request);
                if (createdSchedule > 0)
                    return StatusCode(200, new { status = true, message = "Assessment schedule created successfully", response = 1 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data entered!", response = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpPost("StudentPeriodicAssessmentSave")]
        public async Task<IActionResult> StudentPeriodicAssessmentSave(StudentPeriodicAssessmentSave PeriodicAssessmentStart)
        {
            try
            {
                var PeriodicAssessmentStartId = await _periodicAssessmentRepo.StudentPeriodicAssessmentSave(PeriodicAssessmentStart);
                if (PeriodicAssessmentStartId > 0)
                    return StatusCode(200, new { status = true, message = "Periodic Assessment saved successfully.", response = 1 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data entered!", response = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        //[HttpDelete("DeletePeriodicAssessmentSchedule/{PeriodicAssessmentScheduleId:int}")]
        [HttpDelete("DeletePeriodicAssessmentSchedule/{PeriodicAssessmentScheduleId}")]
        public async Task<IActionResult> DeletePeriodicAssessmentSchedule(int PeriodicAssessmentScheduleId)
        {
            try
            {
                var ReturnVal = await _periodicAssessmentRepo.DeletePeriodicAssessmentSchedule(PeriodicAssessmentScheduleId);
                if (ReturnVal == 3)
                    return StatusCode(200, new { status = true, message = "Schedule deleted successfully.", response = 3 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid Periodic Assessment ScheduleId Id!", response = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
       
        [HttpPut("UpdatePeriodicAssessmentSchedule/{PeriodicAssessmentScheduleId}")]
        public async Task<IActionResult> UpdatePeriodicAssessmentSchedule(short PeriodicAssessmentScheduleId, UpdatePeriodicAssessmentScheduleDto request)
        {
            try
            {
                var ReturnVal = await _periodicAssessmentRepo.UpdatePeriodicAssessmentSchedule(PeriodicAssessmentScheduleId, request);
                if(ReturnVal == 2)                
                    return StatusCode(200, new { status = true, message = "Schedule updated successfully.", response = 2 });                
                else
                    return StatusCode(200, new { status = false, message = "Invalid data entered.", response = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
    }
}
