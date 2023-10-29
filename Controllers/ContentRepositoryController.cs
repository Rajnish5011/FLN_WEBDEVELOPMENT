using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto.PeriodicAssessment;
using ASPNetCoreFLN_APIs.Entities;
using ASPNetCoreFLN_APIs.Helper;
using ASPNetCoreFLN_APIs.TeacherTrainingDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ASPNetCoreFLN_APIs.Controllers
{
    [Route("api/ContentRepository")]
    [ApiController]
    public class ContentRepositoryController : ControllerBase
    {
        private readonly IContentRepository _contentRepo;
        public ContentRepositoryController(IContentRepository contentRepo)
        {
            _contentRepo = contentRepo;
        }


        [HttpGet("GetContentRepositoryCategory")]
        public async Task<IActionResult> GetContentRepositoryCategory()
        {
            try
            {
                var data = await _contentRepo.GetContentRepositoryCategory();
                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Category Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Category Found successfully ", response = data });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });
            }
        }
        [HttpGet("GetAppContentRepositoryByContentForRoleID/{ContentForRoleId}/{currentPageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAppContentRepositoryByContentForRoleID(int ContentForRoleId, int currentPageNumber, int pageSize)
        {
            int maxPageSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPageSize) ? pageSize : maxPageSize;
            int skip = (currentPageNumber) * pageSize;
            int take = pageSize;
            try
            {
                var data = await _contentRepo.GetAppContentRepositoryByContentForRoleID(ContentForRoleId, currentPageNumber, pageSize);
                int Totalcount = Convert.ToInt32(data.GroupBy(item => item.TotalRecords).First().Select(g => g.TotalRecords).First());
                List<AppContentRepository> allTodos = data.ToList<AppContentRepository>().ToList();

                var result = new PagingResponseModel<List<AppContentRepository>>(allTodos, Totalcount, currentPageNumber, pageSize);
                if (data.Count() == 0)
                    return StatusCode(200, new { status = false, message = "Content Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Content Found Successfully.", response = result });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpPost("AddContentForRoles")]
        public async Task<IActionResult> AddContentForRoleSave(AddContentForRoleDto request)
        {
            try
            {
                var ContentRepositoryId = await _contentRepo.AddContentForRoleSave(request);
                if (ContentRepositoryId > 0)
                    return StatusCode(200, new { status = true, message = "Content saved successfully.", response = 1 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data entered!", response = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed to save content." + ex.Message, response = 500 });
            }
        }
    }
}