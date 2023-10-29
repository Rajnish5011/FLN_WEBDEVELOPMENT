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
    [Route("api/[controller]")]
    [ApiController]
    public class MockAssessmentController : ControllerBase
    {
        private readonly IMockAssessmentRepository _mockAssessmentRepo;
        public MockAssessmentController(IMockAssessmentRepository mockAssessmentRepo)
        {
            _mockAssessmentRepo = mockAssessmentRepo;
        }

        [HttpGet("GetMockAssesmentQuestionsByClassSubjectID/{ClassID}/{SubjectID}")]
        public async Task<IActionResult> GetMockAssesmentQuestionsByClassSubjectID(int ClassID, int SubjectID)
        {
            try
            {
                var response = (await _mockAssessmentRepo.GetMockAssesmentQuestionsByClassSubjectID(ClassID, SubjectID)).ToList();
                if (response.Count == 0)
                    return StatusCode(200, new { status = false, message = "Questions Not Found!", response = new List<object>() });

                var firstItem = response.First();

                var result = new
                {
                    Periodic_ORF_Question_Id = firstItem.Periodic_ORF_Question_Id,
                    ORF_Question_Text = firstItem.ORF_Question_Text,
                    Min_Word_Read_Per_Minute = firstItem.Min_Word_Read_Per_Minute,
                    Max_Seconds_To_Read = firstItem.Max_Seconds_To_Read,
                    Questions = response.GroupBy(item => item.Question_Id).Select(q => new
                    {
                        Question_Id = q.Key,
                        Question_Text = q.Select(x => x.Question_Text).FirstOrDefault(),
                        Question_Instruction = q.Select(x => x.Question_Instruction).FirstOrDefault(),
                        Assessment_Type_Id = q.Select(x => x.Assessment_Type_Id).FirstOrDefault(),
                        Question_Type_Id = q.Select(x => x.Question_Type_Id).FirstOrDefault(),
                        Media_Type = q.Select(x => x.Media_Type).FirstOrDefault(),
                        Media_Url = q.Select(x => x.Media_Url).FirstOrDefault(),
                        Base64QuestionImage = _mockAssessmentRepo.GetBase64StringImageByPath(q.Select(x => x.Media_Type).FirstOrDefault(), q.Select(x => x.Media_Url).FirstOrDefault()),
                        Is_Draggable = q.Select(x => x.Is_Draggable).FirstOrDefault(),
                        Option_Media_Type = q.Select(x => x.Option_Media_Type).FirstOrDefault(),
                        Marks = q.Select(x => x.Marks).FirstOrDefault(),
                        QuestionsOptions = q.GroupBy(optitem => optitem.Question_Option_Id).Select(opt => new
                        {
                            Base64OptionImage = _mockAssessmentRepo.GetBase64StringImageByPath(opt.Select(x => x.Option_Media_Type).FirstOrDefault(), opt.Select(x => x.Option_Media_Url).FirstOrDefault()),
                            Question_Option_Id = opt.Key,
                            Option_Text = opt.Select(x => x.Option_Text).FirstOrDefault(),
                            Option_Media_Url = opt.Select(x => x.Option_Media_Url).FirstOrDefault(),
                            Is_Correct = opt.Select(x => x.Is_Correct).FirstOrDefault()
                        })
                    })
                };

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

        [HttpPost("ParentStudentMockAssessmentQuestionSave")]
        public async Task<IActionResult> StudentMockAssessmentQuestionSave(StudentMockAssessmentSave StudentMockAssessmentStart)
        {
            try
            {
                var MockAssessmentStartId = await _mockAssessmentRepo.StudentMockAssessmentQuestionSave(StudentMockAssessmentStart);
                if (MockAssessmentStartId > 0)
                    return StatusCode(200, new { status = true, message = "Mock Assessment saved successfully.", response = 1 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data entered!", response = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpPost("TeacherStudentMockAssessmentQuestionSave")]
        public async Task<IActionResult> TeacherStudentMockAssessmentQuestionSave(TeacherStudentMockAssessmentSave TeacherStudentMockAssessmentStart)
        {
            try
            {
                var MockAssessmentStartId = await _mockAssessmentRepo.TeacherStudentMockAssessmentQuestionSave(TeacherStudentMockAssessmentStart);
                if (MockAssessmentStartId > 0)
                    return StatusCode(200, new { status = true, message = "Mock Assessment saved successfully.", response = 1 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data entered!", response = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

    }
}
