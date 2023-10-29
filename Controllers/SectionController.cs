using ASPNetCoreFLN_APIs.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ASPNetCoreFLN_APIs.Controllers
{
    [Route("api/Masters")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionRepository _sectionRepo;
        public SectionController(ISectionRepository sectionRepo)
        {
            _sectionRepo = sectionRepo;
        }


        [HttpGet("GetSection")]
        public async Task<IActionResult> GetSection()
        {
            try
            {
                var data = await _sectionRepo.GetSection();
                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Section Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Section Found successfully ", response = data });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetSectionBySchoolClass/{SchoolId}/{ClassId}", Name = "ClassId")]
        public async Task<IActionResult> GetSectionByClass(int SchoolId, byte ClassId)
        {
            try
            {
                var result = await _sectionRepo.GetSectionBySchoolClass(SchoolId, ClassId);
                if (result.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Section Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Section Found successfully ", response = result });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });
            }
        }

    }
}
