using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Entities;
using System.Linq;
using ASPNetCoreFLN_APIs.Dto.Mentor;

namespace ASPNetFLN_TestAPI.Controllers
{
    [Route("api/Mentor")]
	[ApiController]

	public class TeachersMeetingController : ControllerBase
	{
		private readonly ITeachersMeetingRepository _meetingScheduleRepo;

		public TeachersMeetingController(ITeachersMeetingRepository meetingScheduleRepo)
		{
            _meetingScheduleRepo = meetingScheduleRepo;
		}

		[HttpPost("TeachersMeeting/CreateClusterTeachersMeetingSchedule")]
		public async Task<IActionResult> CreateClusterTeacherMeetingSchedule(CreateClusterMeetingScheduleDto ClusterMeetingSchedule)
		{
			try
			{
                var ReturnVal = await _meetingScheduleRepo.CreateClusterTeacherMeetingSchedule(ClusterMeetingSchedule);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Meeting Schedule created Successfully", response = ReturnVal });
                else if (ReturnVal==-1)
                    return StatusCode(200, new { status = false, message = "You are not ABRC Mentors so not authourised to create schedule!", response = 200 });
                else if (ReturnVal == -2)
                    return StatusCode(200, new { status = false, message = "Meeting Schedule already created for this Month!", response = 200 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
			{
				//log error
                return StatusCode(500, new { status = false, message = "Failed!"+ ex.Message, response = 500 });
                
            }
		}
		
		[HttpPut("TeachersMeeting/EndClusterTeachersMeetingSchedule/{ClusterMeetingScheduleId}/{MentorId}/{Remarks}")]
		public async Task<ActionResult> ClusterTeachersMeetingScheduleEnd(int ClusterMeetingScheduleId, int MentorId, string Remarks)
		{
			try
			{
				var ScheduleEnd = await _meetingScheduleRepo.ClusterTeachersMeetingScheduleEnd(ClusterMeetingScheduleId, MentorId, Remarks);
				if (ScheduleEnd == 2)
                    return StatusCode(200, new { status = true, message = "Meeting Ended Successfully!", response = 200 });
                if (ScheduleEnd == -1)
                    return StatusCode(403, new { status = false, message = "This Meeting schedule does not exists!", response = 403 });
                if (ScheduleEnd == -2)
                    return StatusCode(200, new { status = false, message = "Please Mark Teacher Attendance to End Meeting!", response = 200 });
                else
                    return StatusCode(403, new { status = false, message = "Invalid data!", response = 403 });
            }
            catch (Exception ex)
			{
                //log error
                return StatusCode(500, new { status = false, message = "Failed to End Schedule! "+ex.Message, response = 500 });
            }
		}
		[HttpPut("TeachersMeeting/UpdateClusterTeachersMeetingSchedule/{ClusterMeetingScheduleId}/{MentorId}")]
		public async Task<ActionResult> UpdateClusterTeachersMeetingSchedule(int ClusterMeetingScheduleId, int MentorId,UpdateClusterMeetingScheduleDto request)
		{
			try
			{
				var data = await _meetingScheduleRepo.UpdateClusterTeachersMeetingSchedule(ClusterMeetingScheduleId, MentorId,request);
				if (data == 1)
					return StatusCode(200, new { status = true, message = "Schedule updated successfully!", response = 200 });
				if (data == -1)
					return StatusCode(200, new { status = false, message = "Cluster Meeting ScheduleId is incorrect!", response = 200 });
				if (data == -2)
					return StatusCode(200, new { status = false, message = "MentorId is incorrect!", response = 200 });
				if (data == -4)
					return StatusCode(200, new { status = false, message = "Attendance Already done You can not update Schedule !", response = 200 });
				if (data == -3)
					return StatusCode(200, new { status = false, message = "Both parameters are missing!", response = 200 });
				else
					return StatusCode(403, new { status = false, message = "Invalid data!", response = 403 });
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, new { status = false, message = "Failed to End Schedule! " + ex.Message, response = 500 });
			}
		}
		[HttpDelete("DeleteClusterTeacherMeetingSchedule/{ClusterMeetingScheduleId}/{MentorId}")]
		public async Task<IActionResult> DeleteClusterTeacherMeetingSchedule(int ClusterMeetingScheduleId, int MentorId)
		{
			try
			{
				var ReturnVal = await _meetingScheduleRepo.DeleteClusterTeachersMeetingSchedule(ClusterMeetingScheduleId, MentorId);
				if (ReturnVal == 1)
					return StatusCode(200, new { status = true, message = "Schedule deleted successfully.", response = 1 });
				else if (ReturnVal == -1)
					return StatusCode(200, new { status = false, message = "ScheduleId is wrong.", response = 200 });
				else if (ReturnVal == -2)
					return StatusCode(200, new { status = false, message = "MentorId is wrong.", response = 200 });
				else if (ReturnVal == -3)
					return StatusCode(200, new { status = false, message = "Both are wrong.", response = 200 });
				else if (ReturnVal == -4)
					return StatusCode(200, new { status = false, message = " Attendance Already done You can not deleted Schedule .", response = 200 });
				else
					return StatusCode(200, new { status = false, message = "Invalid ScheduleId Id!", response = 200 });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
			}
		}
		[HttpGet("TeachersMeeting/GetClusterTeacherMeetingScheduleByMentorId/{MentorID}")]
		public async Task<IActionResult> GetClusterTeacherMeetingScheduleByMentorId(int MentorID)
		{
			try
			{
				var result = await _meetingScheduleRepo.GetClusterTeacherMeetingScheduleByMentorId(MentorID);
				if (result.ToList().Count == 0)
					return StatusCode(200, new { status = false, message = "Meeting Schedule Not Found!", response = result });
				else
					return StatusCode(200, new { status = true, message = "Meeting Schedule Found Successfully.", response = result });

			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
			}
		}

	}
}
