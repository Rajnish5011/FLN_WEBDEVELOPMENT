using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Models;
using System.Buffers.Text;
using ASPNetCoreFLN_APIs.Entities;
using ASPNetCoreFLN_APIs.Dto.Mentor;

namespace ASPNetCoreFLN_APIs.Controllers
{
    [ApiController]
    [Route("api/Mentor")]
    public class MentorSchoolScheduleStartController : Controller
    {
        IMentorSchoolScheduleStartRepository _MentorSchoolScheduleStartRepo;

        public MentorSchoolScheduleStartController(IMentorSchoolScheduleStartRepository MentorSchoolScheduleStartRepo)
        {
            _MentorSchoolScheduleStartRepo = MentorSchoolScheduleStartRepo;
        }

        //[HttpPost, Route("MentorSchoolScheduleStart/{MentorSchoolScheduleId:int}/{MentorId:int}")]
        [HttpPost, Route("MentorSchoolScheduleStart/{MentorSchoolScheduleId}/{MentorId}")]
        public async Task<ActionResult> MentorSchoolScheduleStart(int MentorSchoolScheduleId, int MentorId, [FromBody] MentorSchoolScheduleStartDto MentorScheduleStart)
        {
            try
            {
                int ReturnVal = await _MentorSchoolScheduleStartRepo.MentorSchoolScheduleStart(MentorSchoolScheduleId, MentorId, MentorScheduleStart);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Image Uploaded & Schedule started Successfully", response = new { MentorScheduleStartId = ReturnVal } });                    
                else
                    return StatusCode(200, new { status = false, message = "Image Upload failed!", response = new { MentorScheduleStartId = 0 } });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed! "+ex.Message, response = 500});
            }
        }
       
        [HttpGet("GetMentorImage/{MentorSchoolScheduleStartId}")]
        public async Task<IActionResult> GetMentorImage(int MentorSchoolScheduleStartId)
        { 
            byte[] ImageData = await _MentorSchoolScheduleStartRepo.GetMentorImage(MentorSchoolScheduleStartId);

            try
            {          
            if (ImageData == null)
                return StatusCode(200, new { status = false, message = "Image Not Found!", response = ImageData });
            else
                return StatusCode(200, new { status = true, message = "Image Found Successfully.", response = ImageData });

        }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
      
    }
}
