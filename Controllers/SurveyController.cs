using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Dapper;
using System.Data;
using ASPNetCoreFLN_APIs.Entities;
using ASPNetCoreFLN_APIs.Dto.SurveyDTO;
using System.Linq;

namespace ASPNetCoreFLN_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyRepository _surveyRepository;
        public SurveyController(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        [HttpPost("CreateSurevy")]
        public async Task<IActionResult> CreateSurevy(SurveyDto survey)
        {
            try
            {
                var ReturnVal = await _surveyRepository.CreateSurveyForm(survey);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "survey created Successfully", response = ReturnVal });
                return StatusCode(404, new { status = false, message = "Survey not created ", response = 404 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Critical error occured! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetSurevyById")]
        public async Task<IActionResult> GetSurevyById(string surevy_Id)
        {
            try
            {
                var ReturnVal = await _surveyRepository.GetSurveyForm(surevy_Id);
                if (ReturnVal != null)
                    return StatusCode(200, new { status = true, message = "get survey ", response = ReturnVal });
                return StatusCode(404, new { status = false, message = "error ", response = 404 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Critical error occured! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetAllSurevyList")]
        public async Task<IActionResult> GetAllSurevyList(string mentor_Id)
        {
            try
            {
                var ReturnVal = await _surveyRepository.GetAllSurveyForm(mentor_Id);
                if (ReturnVal.Any())
                    return StatusCode(200, new { status = true, message = "get all survey ", response = ReturnVal });
                return StatusCode(404, new { status = false, message = "error ", response = 404 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Critical error occured! " + ex.Message, response = 500 });
            }
        }
        [HttpPost("SaveSurvey")]
        public async Task<IActionResult> SaveSurvey(SurveyAnswers answers)
        {
            try
            {
                var ReturnVal = await _surveyRepository.SaveSurveyForm(answers);
                if (ReturnVal == 1)
                    return StatusCode(200, new { status = true, message = "Survey Data Saved", response = ReturnVal });
                return StatusCode(404, new { status = false, message = "error ", response = 404 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Critical error occured! " + ex.Message, response = 500 });
            }
        }
    }
}
