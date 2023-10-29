using ASPNetCoreFLN_APIs.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using ASPNetCoreFLN_APIs.Dto.StudentTeacherCommunication;
using System.Linq;

namespace ASPNetCoreFLN_APIs.Controllers
{
    [Route("api/StudentTeacherCommunication")]
    [ApiController]
    public class StudentTeacherCommunicationController : ControllerBase
    {
        private IStudentTeacherCommunicationRepository _communication;
        public StudentTeacherCommunicationController(IStudentTeacherCommunicationRepository communication)
        {
            _communication= communication;
        }

        [HttpPost("StudentTeacherCommunication")]
        public async Task<IActionResult> StudentTeacherCommunication(StudentTeacherCommunicationDto  communicationDto)
        {
            try
            {
                var ReturnVal = await _communication.StudentTeacherCommunication(communicationDto);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Communication In Process", response = communicationDto });
                return StatusCode(404, new { status = false, message = "Critical error occured!", response = 404 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Critical error occured! " + ex.Message, response = 500 });
            }
        }
        [HttpGet("PreviousChats")]
        public async Task<IActionResult> StudentTeacherPreviousChats(int sender_Id, int receiver_Id, int pageNumber, int pageSize)
        {
            try
            {
                var ReturnVal = await _communication.GetStudentTeacherChats(sender_Id , receiver_Id, pageNumber, pageSize);
                return StatusCode(200, new { status = true, message = "PreviousChats Present", response = ReturnVal });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Critical error occured! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetTeacherToChat")]
        public async Task<IActionResult> GetTeacherToCommunicate(int class_Id, int section_Id, int school_Id)
        {
            try
            {
                var ReturnVal = await _communication.GetTecherListToCommunication(class_Id, section_Id, school_Id);


                if (ReturnVal.Count == 0)
                    return StatusCode(200, new { status = false, message = "Teacher Not Found!", response = ReturnVal });

                else
                    return StatusCode(200, new
                    {
                        status = true,
                        message = "Teacher Found Successfully.",
                        response = ReturnVal
                    });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }



        [HttpGet("GetStudentsToChat")]
        public async Task<IActionResult> GetStudentsToCommunicate(int class_Id, int section_Id, int school_Id)
        {
            try
            {
                var result = await _communication.GetStudentListToCommunication(class_Id,  section_Id, school_Id);
                if (result.ToList().Count == 0)

                    return StatusCode(200, new { status = false, message = "Students Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Students Found successfully.", response = result });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
    }
}
