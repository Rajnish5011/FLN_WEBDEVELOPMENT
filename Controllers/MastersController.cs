using ASPNetCoreFLN_APIs.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Repository;

namespace ASPNetCoreFLN_APIs.Controllers
{
    [Route("api/Masters")]
    [ApiController]
    public class MastersController : ControllerBase
    {
        private readonly IMastersRepository _masterRepo;
        public MastersController(IMastersRepository assessmentTypeRepo)
        {
            _masterRepo = assessmentTypeRepo;
        }
        [HttpGet("GetDistrictByStateId/{StateID}")]
        public async Task<IActionResult> GetDistrictByStateId(short StateID)
        {
            try
            {
                var data = await _masterRepo.GetDistrictByStateId(StateID);
                if (data.ToList().Count == 0)

                    return StatusCode(200, new { status = false, message = "District Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "District found successfully.", response = data });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("Blocks/BlocksByDistrictId/{id}")]
        public async Task<IActionResult> GetBlocksByDistrictId(short id)
        {
            try
            {
                var data = await _masterRepo.GetBlocksByDistrictId(id);
                if (data.ToList().Count == 0)

                    return StatusCode(200, new { status = false, message = "Blocks Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Blocks Found successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
      
        [HttpGet("GetAssessmentType")]
        public async Task<IActionResult> GetAssessmentType()
        {
            try
            {
                var data = await _masterRepo.GetAssessmentType();
                if (data.ToList().Count == 0)

                    return StatusCode(200, new { status = false, message = "Assessment Type Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Assessment Type Found successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetQuestionType")]
        public async Task<IActionResult> GetQuestionType()
        {
            try
            {
                var data = await _masterRepo.GetQuestionType();
                if (data.ToList().Count == 0)

                    return StatusCode(200, new { status = false, message = "Question Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Question Found Successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetAllClass")]
        public async Task<IActionResult> GetClass()
        {
            try
            {
                var data = await _masterRepo.GetClass();
                if (data.ToList().Count == 0)

                    return StatusCode(200, new { status = false, message = "Class Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Class Found successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }


        }

        //Get Class By SchoolId
        [HttpGet("GetClassBySchoolId/{SchoolId}")]
        public async Task<IActionResult> GetClassBySchoolId(int SchoolId)
        {
            try
            {
                var data = await _masterRepo.GetClassBySchoolId(SchoolId);

                if (data.ToList().Count == 0)

                    return StatusCode(200, new { status = false, message = "Class Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Class Found successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetClassSectionBySchoolId/{SchoolId}")]
        public async Task<IActionResult> GetClassSectionBySchoolId(int SchoolId)
        {
            try
            {
                var data = await _masterRepo.GetClassSectionBySchoolId(SchoolId);

                if (data.ToList().Count == 0)

                    return StatusCode(200, new { status = false, message = "Class Section Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Class Section Found successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
        [HttpGet("GetMonths")]
        public async Task<IActionResult> GetMonths()
        {
            try
            {
                var data = await _masterRepo.GetMonths();
                if (data.ToList().Count == 0)

                    return StatusCode(200, new { status = false, message = "Months Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Months Found successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("SpotAssessment/GetWeeks")]
        public async Task<IActionResult> GetWeeks()
        {
            try
            {
                var data = await _masterRepo.GetWeeks();
                if (data.ToList().Count == 0)

                    return StatusCode(200, new { status = false, message = "Weeks Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Weeks Found successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var data = await _masterRepo.GetAllRoles();

                if (data.ToList().Count == 0)

                    return StatusCode(200, new { status = false, message = "Role Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Role Found successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetDesignationByRole/{RoleId}")]
        public async Task<IActionResult> GetDesignationByRole(short RoleId)
        {
            try
            {
                var data = await _masterRepo.GetDesignationByRole(RoleId);

                if (data.ToList().Count == 0)

                    return StatusCode(200, new { status = false, message = "Designation Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Designation Found successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

    }
}

