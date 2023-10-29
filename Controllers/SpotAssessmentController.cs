using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Entities;
using System.Linq;
using System.Reflection.Metadata;
using ASPNetCoreFLN_APIs.Dto.SpotAssessment;
using Microsoft.AspNetCore.Hosting;
using ASPNetCoreFLN_APIs.Dto;

namespace ASPNetFLN_TestAPI.Controllers
{
    [Route("api/School")]
    [ApiController]

    public class SpotAssessmentController : ControllerBase
    {
        private readonly ISpotAssessmentRepository _SpotAssessmentRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _webRootPath;
        public SpotAssessmentController(ISpotAssessmentRepository SpotAssessmentRepo, IWebHostEnvironment webHostEnvironment)
        {
            _SpotAssessmentRepo = SpotAssessmentRepo;
            _webHostEnvironment = webHostEnvironment;
            _webRootPath = _webHostEnvironment.WebRootPath;
        }

        [HttpPost("CreateMentorStudentSpotAssessment")]
        public async Task<ActionResult> CreateMentorStudentSpotAssessment(SpotAssessmentDto request)
        {
            try
            {
                var ReturnVal = await _SpotAssessmentRepo.CreateMentorStudentSpotAssessment(request);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Student Spot Assessment done Successfully", response = 1 });
                else if (ReturnVal == -1)
                    return StatusCode(409, new { status = false, message = "Spot Assessment of this Student is already done!", response = -1 });
                
                else if (ReturnVal == -2)
                    return StatusCode(409, new { status = false, message = "ORF Assessment of this Student is already done!", response = -2 });
                else
                    return StatusCode(403, new { status = false, message = "Please enter valid data!", response = 403 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Critical error occured! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetSpotAssessmentQuestions/{ClassId}/{SubjectId}")]
        public async Task<IActionResult> GetSpotAssessmentQuestions(byte ClassId, byte SubjectId)
        {
            try
            {
                var response = (await _SpotAssessmentRepo.GetSpotAssessmentQuestions(ClassId, SubjectId)).ToList();
                var data = response.GroupBy(item => item.ORF_Question_Id)
                 .Select(selector: f => new
                 {
                     ORF_Question_Id = f.Key,
                     ORF_Question_Text = f.Select(x => x.ORF_Question_Text.ToString()).FirstOrDefault(),
                     Max_Seconds_To_Read = f.Select(x => x.Max_Seconds_To_Read).FirstOrDefault(),
                     Is_ORF_Required=f.Select(x=> x.Is_ORF_Required).FirstOrDefault(),
                     Is_Spot_Required = f.Select(x => x.Is_Spot_Required).FirstOrDefault(),

                     Instruction = f.GroupBy(Ins => Ins.Question_Group_Instruction_Id).Select(selector: g => new
                     {
                         Question_Group_Instruction_Id = g.Key,
                         Question_Group_Instruction = g.Select(x => x.Question_Group_Instruction).FirstOrDefault(),
                         Total_Question = g.Select(x => x.Total_Question).FirstOrDefault(),
                         Min_Attempt_Question = g.Select(x => x.Min_Attempt_Question).FirstOrDefault(),
                         Mastery_Criteria = g.Select(x => x.Mastery_Criteria).FirstOrDefault(),
                         Competency_Id = g.Select(x => x.Competency_Id).FirstOrDefault(),
                         Competency = g.Select(x => x.Competency).FirstOrDefault(),

                         Questions = g.GroupBy(Qitem => Qitem.Question_Id).Select(selector: Q => new
                         {
                             Question_Id = Q.Key,
                             Base64QuestionImage = _SpotAssessmentRepo.GetBase64StringImageByPath(Q.Select(x => x.Question_Media_Type).FirstOrDefault().ToString(), _webRootPath + @"\Uploads\QuestionImages\", Q.Select(x => x.Question_Media_Url).FirstOrDefault().ToString().Trim()),
                             Question_Media_Url = Q.Select(hx => hx.Question_Media_Url.ToString()).FirstOrDefault().ToString(),
                             Question_Type = Q.Select(hx => hx.Question_Type).FirstOrDefault(),
                             Question_Text = Q.Select(hx => hx.Question_Text).FirstOrDefault(),
                             Question_Media_Type = Q.Select(hx => hx.Question_Media_Type).FirstOrDefault(),
                             Is_Draggable = Q.Select(hx => hx.Is_Draggable).FirstOrDefault(),

                             QuestionsOptions = Q.Select(optitem => new
                             {
                                 Base64OptionImage = _SpotAssessmentRepo.GetBase64StringImageByPath(Q.Select(hx => hx.Option_Media_Type).FirstOrDefault().ToString(), _webRootPath + @"\Uploads\OptionImages\", optitem.Option_Media_Url.ToString().Trim()),
                                 Question_Option_Id = optitem.Question_Option_Id,
                                 Option_Media_Type_Id = optitem.Option_Media_Type_Id,
                                 Option_Media_Type = optitem.Option_Media_Type,
                                 Option_Text = optitem.Option_Text,
                                 Option_Media_Url = optitem.Option_Media_Url.ToString(),
                                 Is_Correct = optitem.Is_Correct
                             }).ToList()
                         })
                     }).ToList()
                 });

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

        [HttpGet("Monitor/GetSpotAssessmentQuestionsByCompetency/{CompetencyId}")]
        public async Task<IActionResult> GetSpotAssessmentQuestionsByCompetency(int CompetencyId)
        {
            try
            {
                var response = (await _SpotAssessmentRepo.GetSpotAssessmentQuestionsByCompetency(CompetencyId)).ToList();
                var data = response.GroupBy(item => item.ORF_Question_Id)
                 .Select(selector: f => new
                 {
                     ORF_Question_Id = f.Key,
                     ORF_Question_Text = f.Select(x => x.ORF_Question_Text.ToString()).FirstOrDefault(),
                     Max_Seconds_To_Read = f.Select(x => x.Max_Seconds_To_Read).FirstOrDefault(),
                     Is_ORF_Required = f.Select(x => x.Is_ORF_Required).FirstOrDefault(),
                     Is_Spot_Required = f.Select(x => x.Is_Spot_Required).FirstOrDefault(),

                     Instruction = f.GroupBy(Ins => Ins.Question_Group_Instruction_Id).Select(selector: g => new
                     {
                         Question_Group_Instruction_Id = g.Key,
                         Question_Group_Instruction = g.Select(x => x.Question_Group_Instruction).FirstOrDefault(),
                         Total_Question = g.Select(x => x.Total_Question).FirstOrDefault(),
                         Min_Attempt_Question = g.Select(x => x.Min_Attempt_Question).FirstOrDefault(),
                         Mastery_Criteria = g.Select(x => x.Mastery_Criteria).FirstOrDefault(),
                         Competency_Id = g.Select(x => x.Competency_Id).FirstOrDefault(),
                         Competency = g.Select(x => x.Competency).FirstOrDefault(),

                         Questions = g.GroupBy(Qitem => Qitem.Question_Id).Select(selector: Q => new
                         {
                             Question_Id = Q.Key,
                             Base64QuestionImage = _SpotAssessmentRepo.GetBase64StringImageByPath(Q.Select(x => x.Question_Media_Type).FirstOrDefault().ToString(), _webRootPath + @"\Uploads\QuestionImages\", Q.Select(x => x.Question_Media_Url).FirstOrDefault().ToString().Trim()),
                             Question_Media_Url = Q.Select(hx => hx.Question_Media_Url.ToString()).FirstOrDefault().ToString(),
                             Question_Type = Q.Select(hx => hx.Question_Type).FirstOrDefault(),
                             Question_Text = Q.Select(hx => hx.Question_Text).FirstOrDefault(),
                             Question_Media_Type = Q.Select(hx => hx.Question_Media_Type).FirstOrDefault(),
                             Is_Draggable = Q.Select(hx => hx.Is_Draggable).FirstOrDefault(),

                             QuestionsOptions = Q.Select(optitem => new
                             {
                                 Base64OptionImage = _SpotAssessmentRepo.GetBase64StringImageByPath(Q.Select(hx => hx.Option_Media_Type).FirstOrDefault().ToString(), _webRootPath + @"\Uploads\OptionImages\", optitem.Option_Media_Url.ToString().Trim()),
                                 Question_Option_Id = optitem.Question_Option_Id,
                                 Option_Media_Type_Id = optitem.Option_Media_Type_Id,
                                 Option_Media_Type = optitem.Option_Media_Type,
                                 Option_Text = optitem.Option_Text,
                                 Option_Media_Url = optitem.Option_Media_Url.ToString(),
                                 Is_Correct = optitem.Is_Correct
                             }).ToList()
                         })
                     }).ToList()
                 });

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

        [HttpGet("Monitor/GetSpotCompetancyTillDateByClassSubjectId/{ClassId}/{SubjectId}")]
        public async Task<IActionResult> GetSpotCompetancyTillDateByClassSubjectId(byte ClassId, byte SubjectId)
        {
            try
            {
                var data = await _SpotAssessmentRepo.GetSpotCompetancyTillDateByClassSubjectId(ClassId, SubjectId);

                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Competancy Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Competancy Found successfully ", response = data });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });
            }
        }
    }
}
