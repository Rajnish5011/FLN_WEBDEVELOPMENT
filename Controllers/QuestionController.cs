using ASPNetCoreFLN_APIs.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Dto;
using Microsoft.AspNetCore.Hosting;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting.Server;
using System.IO;
using Microsoft.Extensions.FileProviders;
using System.Net.Http;

namespace ASPNetCoreFLN_APIs.Controllers
{
    [Route("api/StateAdmin/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _webRootPath;
        public QuestionController(IQuestionRepository QuestionRepo, IWebHostEnvironment webHostEnvironment)
        {
            _questionRepo = QuestionRepo;
            _webHostEnvironment = webHostEnvironment;
            _webRootPath = _webHostEnvironment.WebRootPath;   
        }

        [HttpGet("GetMediaTypeForQuestionOrOption/{IsForOption}")]
        public async Task<IActionResult> GetMediaType(IsForOption IsForOption)        
        {
            try
            {
                var data = await _questionRepo.GetMediaType(IsForOption);
                if (data.ToList().Count > 0)
                    return StatusCode(200, new { status = true, message = "Media Type Found.", response = data });
                else
                    return StatusCode(200, new { status = false, message = "Media Type Not Found!", response = data });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        /*   
       [HttpGet("GetGetQuestionImage/{IsForOption}")]
       public async Task<IActionResult> GetQuestionImage(IsForOption IsForOption)
       {
           try
           {
               var provider = new PhysicalFileProvider(_webHostEnvironment.WebRootPath);
               var contents = provider.GetDirectoryContents(Path.Combine("Uploads", "QuestionImages"));
               var objFiles = contents.OrderBy(m => m.LastModified).First();
               var image = System.IO.File.OpenRead(objFiles.PhysicalPath);
               byte[] imagebytes = System.IO.File.ReadAllBytes(objFiles.PhysicalPath);

               var base64 =  Convert.ToBase64String(imagebytes);
               var imgSrc = string.Format("data:image/jpeg;base64,{0}", base64);

               var QuestionImage = File(imagebytes, "image/png");

               return QuestionImage;
           }
           catch (Exception ex)
           {
               //log error
               return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
           }
       }

       [HttpPost("CreateImageQuestionToQuestionBankSave")]
       public async Task<ActionResult> CreateImageQuestionToQuestionBankSave([FromForm] IFormFile QuestionImage, IFormFile OptionImage, ImageQuestion question)
       {
           try
           {
               var ReturnVal = await _questionRepo.CreateImageQuestionToQuestionBankSave(QuestionImage, OptionImage, _webRootPath, question);
               if (ReturnVal > 0)
                   ReturnVal = 1;

               return StatusCode(ReturnVal);
           }
           catch (Exception)
           {
               return StatusCode(500);
           }
       }
       */
        [HttpPost("CreateQuestionToQuestionBankSave")]
        public async Task<ActionResult> CreateQuestionToQuestionBankSave(Base64ImageQuestion question)
        {
            try
            {
                var ReturnVal = await _questionRepo.CreateQuestionToQuestionBankSave(question, _webRootPath);

                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Question created successfully!", response = 1 });

                else if (ReturnVal == -145)
                    return StatusCode(400, new { status = false, message = "Question Text cannot be empty! Try again.", response = 400 });

                else if (ReturnVal == -500)
                    return StatusCode(400, new { status = false, message = "Question Image must be < 500kb! Try again.", response = 400 });
                
                else if (ReturnVal == -24)
                    return StatusCode(400, new { status = false, message = "Question Media Url cannot be empty! Try again.", response = 400 });
                
                else if (ReturnVal == -499)
                    return StatusCode(400, new { status = false, message = "Option Image must be < 500kb! Try again.", response = 400 });
                
                else if (ReturnVal == -1)
                    return StatusCode(400, new { status = false, message = "You must have Question Image in Base64 format! Try again.", response = 400 });

                else if (ReturnVal == -2)
                    return StatusCode(400, new { status = false, message = "You must have Option Image in Base64 format! Try again.", response = 400 });
                else if (ReturnVal == -3)
                    return StatusCode(400, new { status = false, message = "Question Marks Can Not be Zero.", response = 400 });
                else if (ReturnVal == -144)
                    return StatusCode(400, new { status = false, message = "Option Text cannot be empty! Try again.", response = 400 });
                else
                    return StatusCode(500, new { status = false, message = "Failed! Wrong data", response = 500 });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed to create Question! " + ex.Message, response = 500 });

            }
        }

        [HttpPut("UpdatePeriodicAssessmentQuestion")]
        public async Task<ActionResult> UpdatePeriodicAssessmentQuestion(UpdatePeriodicQuestion question)
        {
            try
            {
                var ReturnVal = await _questionRepo.UpdatePeriodicAssessmentQuestion(question, _webRootPath);

                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Question updated successfully!", response = 1 });

                else if (ReturnVal == -145)
                    return StatusCode(400, new { status = false, message = "Question Text cannot be empty! Try again.", response = 400 });

                else if (ReturnVal == -500)
                    return StatusCode(400, new { status = false, message = "Question Image must be < 500kb! Try again.", response = 400 });

                else if (ReturnVal == -24)
                    return StatusCode(400, new { status = false, message = "Question Media Url cannot be empty! Try again.", response = 400 });

                else if (ReturnVal == -499)
                    return StatusCode(400, new { status = false, message = "Option Image must be < 500kb! Try again.", response = 400 });

                else if (ReturnVal == -1)
                    return StatusCode(400, new { status = false, message = "You must have Question Image in Base64 format! Try again.", response = 400 });

                else if (ReturnVal == -2)
                    return StatusCode(400, new { status = false, message = "You must have Option Image in Base64 format! Try again.", response = 400 });
                else if (ReturnVal == -3)
                    return StatusCode(400, new { status = false, message = "Question Marks Can Not be Zero.", response = 400 });
                else if (ReturnVal == -144)
                    return StatusCode(400, new { status = false, message = "Option Text cannot be empty! Try again.", response = 400 });
                else
                    return StatusCode(500, new { status = false, message = "Failed! Wrong data", response = 500 });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed to update Question! " + ex.Message, response = 500 });

            }
        }

        [HttpDelete("DeletePeriodicAssessmentQuestions/{QuestionId}")]
        public async Task<IActionResult> DeletePeriodicAssessmentQuestions(int QuestionId)
        {
            try
            {
                var ReturnVal = await _questionRepo.DeletePeriodicAssessmentQuestions(QuestionId);
                if (ReturnVal == 3)
                    return StatusCode(200, new { status = true, message = "Question deleted successfully.", response = 3 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid Periodic Assessment Question Id!", response = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpPost("TeachersTraining/CreateTeacherTrainingTestQuestion")]
        public async Task<ActionResult> CreateTeacherTrainingTestQuestion(TeacherTrainingtQuestionDto question)
        {
            try
            {
                var ReturnVal = await _questionRepo.CreateTeacherTrainingTestQuestion(question, _webRootPath);

                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Question created successfully!", response = 1 });

                else if (ReturnVal == -145)
                    return StatusCode(400, new { status = false, message = "Question Text cannot be empty! Try again.", response = 400 });

                else if (ReturnVal == -500)
                    return StatusCode(400, new { status = false, message = "Question Image must be < 500kb! Try again.", response = 400 });

                else if (ReturnVal == -24)
                    return StatusCode(400, new { status = false, message = "Question Media Url cannot be empty! Try again.", response = 400 });

                else if (ReturnVal == -499)
                    return StatusCode(400, new { status = false, message = "Option Image must be < 500kb! Try again.", response = 400 });

                else if (ReturnVal == -1)
                    return StatusCode(400, new { status = false, message = "You must have Question Image in Base64 format! Try again.", response = 400 });

                else if (ReturnVal == -2)
                    return StatusCode(400, new { status = false, message = "You must have Option Image in Base64 format! Try again.", response = 400 });                
                else if (ReturnVal == -3)
                    return StatusCode(400, new { status = false, message = "Group name already exists in this test! Try again.", response = 400 });
                else if (ReturnVal == -144)
                    return StatusCode(400, new { status = false, message = "Option Text cannot be empty! Try again.", response = 400 });
                else
                    return StatusCode(500, new { status = false, message = "Failed! Wrong data", response = 500 });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed to create Question! " + ex.Message, response = 500 });

            }
        }
    }
}
