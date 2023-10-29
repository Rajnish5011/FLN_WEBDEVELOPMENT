using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto;


namespace ASPNetCoreFLN_APIs.Controllers
{
    [Route("api/Masters/[controller]")]
    [ApiController]

    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepo;

        public SubjectController(ISubjectRepository subjectRepo)
        {
            _subjectRepo = subjectRepo;
        }

        [HttpGet("GetAllSubjects")]
        public async Task<IActionResult> GetSubjects()
        {
            try
            {
                var data = await _subjectRepo.GetSubject();

                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Subjects Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Subjects Found Successfully ", response = data });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });
            }
        }

        [HttpGet("SubjectTopicBySubjectId/{SubjectId}")]
        public async Task<IActionResult> GetSubjectTopicBySubjectId(byte SubjectId)
        {
            try
            {
                var data = await _subjectRepo.GetSubjectTopicBySubjectId(SubjectId);
                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Subjects Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Subjects Found successfully ", response = data });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });
            }
        }
        [HttpGet("GetSubjectCompetancy/{SubjectId}/{SubjectTopicId}")]
        public async Task<IActionResult> GetSubjectCompetancy(byte SubjectId, short SubjectTopicId)
        {
            try
            {
                var data = await _subjectRepo.GetSubjectCompetancy(SubjectId, SubjectTopicId);


                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Competancy Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Competancy Found Successfully ", response = data });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });
            }
        }
        [HttpGet("GetCompetancyByClassSubjectId/{ClassId}/{SubjectId}")]
        public async Task<IActionResult> GetCompetancyByClassSubjectId(byte ClassId, byte SubjectId)
        {
            try
            {
                var data = await _subjectRepo.GetCompetancyByClassSubjectId(ClassId, SubjectId);

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

        [HttpGet("GetSpotCompetancyByMonthClassSubjectId/{GetMonthFromDate}/{ClassId}/{SubjectId}")]
        public async Task<IActionResult> GetSpotCompetancyByMonthClassSubjectId(DateTime GetMonthFromDate, byte ClassId, byte SubjectId)
        {
            try
            {
                var data = await _subjectRepo.GetSpotCompetancyByMonthClassSubjectId(GetMonthFromDate,ClassId, SubjectId);

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
        /*
        [HttpGet("SubjectByClassId/{id}")]       
        public async Task<IActionResult> GetSubjectByClassId(short id)
        {
            try
            {
                var data = await _subjectRepo.GetSubjects();
                
                if (data == null)
                    return NotFound();

                return Ok(data);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        */
    }
}
