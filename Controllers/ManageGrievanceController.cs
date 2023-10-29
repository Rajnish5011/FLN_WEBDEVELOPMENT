using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Entities;
using System.Linq;
using ASPNetCoreFLN_APIs.Dto.Mentor;
using ASPNetCoreFLN_APIs.Dto.Grievance;

namespace ASPNetFLN_TestAPI.Controllers
{
    [Route("api/Grievance")]
	[ApiController]

	public class ManageGrievanceController : ControllerBase
	{
		private readonly IManageGrievanceRepository _manageGrievanceRepo;

		public ManageGrievanceController(IManageGrievanceRepository manageGrievanceRepo)
		{
            _manageGrievanceRepo = manageGrievanceRepo;
		}

		[HttpPost("CreateGrievanceTicketByUser")]
		public async Task<IActionResult> CreateGrievanceTicketByUser(CreateGrievanceDto Grievance)
		{
			try
			{
                var ReturnVal = await _manageGrievanceRepo.CreateGrievanceTicketByUser(Grievance);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Grievance Ticket created Successfully", response = ReturnVal });
                else if (ReturnVal==-1)
                    return StatusCode(403, new { status = false, message = "Invalid Category Selected!", response = 403 });
                else if (ReturnVal == -2)
                    return StatusCode(403, new { status = false, message = "Can't create Ticket! Invalid Category Selected!", response = 403 });

                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
			{
				//log error
                return StatusCode(500, new { status = false, message = "Failed!"+ ex.Message, response = 500 });
                
            }
		}

        //[HttpPost("AddNewGrievanceCategory")]
        //public async Task<IActionResult> CreateNewGrievanceCategory(CreateGrievanceCategoryDto request)
        //{
        //    try
        //    {
        //        var ReturnVal = await _manageGrievanceRepo.CreateNewGrievanceCategory(request);
        //        if (ReturnVal > 0)
        //            return StatusCode(200, new { status = true, message = "Grievance Category created Successfully", response = ReturnVal });
        //        else if (ReturnVal == -1)
        //            return StatusCode(403, new { status = false, message = "Choose correct option to create New Category!", response = 403 });
                
        //        else
        //            return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
        //    }
        //    catch (Exception ex)
        //    {
        //        //log error
        //        return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

        //    }
        //}


        [HttpPut("ResolveGrievanceTicket/{TicketId}/{HelpDeskUserId}/{GrievanceReply}")]
		public async Task<ActionResult> GrievanceTicketReplyByHelpDeskUser(int TicketId, int HelpDeskUserId, string GrievanceReply)
        {
			try
			{
				var ScheduleEnd = await _manageGrievanceRepo.GrievanceTicketReplyByHelpDeskUser(TicketId, HelpDeskUserId, GrievanceReply);
				if (ScheduleEnd == 2)
					return StatusCode(200, new { status = true, message = "Grievance Reply submitted & Ticket closed Successfully!", response = 200 });
				if (ScheduleEnd == -2)
					return StatusCode(403, new { status = false, message = "This Grievance already closed! Cannot change anything.", response = 403 });
				
				else
					return StatusCode(403, new { status = false, message = "Invalid data!", response = 403 });
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, new { status = false, message = "Failed to End Schedule! " + ex.Message, response = 500 });
			}
		}

		[HttpGet("GetGrievanceCategoriesByGrievanceForRoleID/{GrievanceForRoleID}")]
		public async Task<IActionResult> GetGrievanceCategoriesByGrievanceForRoleID(int GrievanceForRoleID)
		{
			try
			{
				var result = await _manageGrievanceRepo.GetGrievanceCategoriesByGrievanceForRoleID(GrievanceForRoleID);
				if (result.ToList().Count == 0)
					return StatusCode(200, new { status = false, message = "Category Not Found!", response = result });
				else
					return StatusCode(200, new { status = true, message = "Category Found Successfully.", response = result });

			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
			}
		}

        [HttpGet("GetGrievanceDetailsByTicketID/{TicketID}")]
        public async Task<IActionResult> GetGrievanceDetailsByTicketID(int TicketID)
        {
            try
            {
                var result = await _manageGrievanceRepo.GetGrievanceDetailsByTicketID(TicketID);
                if (result.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Grievance Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Grievance Found Successfully.", response = result });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetGrievanceTicketsRaisedByUsers/{IsClosed}")]
        public async Task<IActionResult> GetGrievanceTicketsRaisedByUsers(bool IsClosed)
        {
            try
            {
                var result = await _manageGrievanceRepo.GetGrievanceTicketsRaisedByUsers(IsClosed);
                if (result.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Ticket Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Ticket Found Successfully.", response = result });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("GetGrievanceTicketListDetailRaisedByUser/{UserId}/{RoleId}")]
        public async Task<IActionResult> GetGrievanceTicketListDetailRaisedByUser(int UserId, byte RoleId)
        {
            try
            {
                var result = await _manageGrievanceRepo.GetGrievanceTicketListDetailRaisedByUser(UserId, RoleId);
                if (result.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Ticket(s) Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Ticket(s) Found Successfully.", response = result });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
    }
}
