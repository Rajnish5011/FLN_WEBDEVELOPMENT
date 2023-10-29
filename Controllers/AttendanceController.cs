using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Controllers
{
    [Route("api/Attendance")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private IAttendanceRepository _attendanceRepo;

        public AttendanceController(IAttendanceRepository attendanceRepo)
        {
            _attendanceRepo = attendanceRepo;
        }
        
        [HttpPost("StudentAttendanceByTeacherSave")]
        public async Task<IActionResult> StudentAttendanceByTeacherSave(StudentAttendanceDto Attendance)
        {
            try
            {
                var ReturnVal = await _attendanceRepo.StudentAttendanceByTeacherSave(Attendance);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Attendance Marked Successfully", response = 1 });
                else if(ReturnVal== -1)
                    return StatusCode(400, new { status = false, message = "Attendance Already Done!", response = 400 });
                else if (ReturnVal == -2)
                    return StatusCode(400, new { status = false, message = "You can Mark Attendance of same day only!", response = 400 });
                else if (ReturnVal == -3)
                    return StatusCode(403, new { status = false, message = "Teacher is not Authorised to Mark Attendance!", response = 403 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data! Attendance could not be Saved!", response = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Critical error occured! "+ex.Message, response = 500 });
            }
        }

        [HttpGet("GetStudentAttendance/{AttendanceDate}/{SchoolId}/{ClassId}/{SectionId}")]
        public async Task<IActionResult> GetStudentAttendanceByDateSchoolClassSection(string AttendanceDate, int SchoolId, byte ClassId, byte SectionId)
        {
            try
            {
                var result = await _attendanceRepo.GetStudentAttendanceByDateSchoolClassSection(AttendanceDate, SchoolId, ClassId, SectionId);
                if (result.ToList().Count==0)
                    return StatusCode(200, new { status = false, message = "Attendance Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Attendance Found Successfully", response = result });                
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Critical error occured!"+ ex.Message, response = 500 });
            }
        }

        [HttpGet("GetStudentAttendanceClassWiseByTeacherId/{TeacherId}")]
        public async Task<IActionResult> GetStudentAttendanceByTeacherId(int TeacherId)
        {
            try
            {
                var result = await _attendanceRepo.GetStudentAttendanceByTeacherId(TeacherId);               
                if (result.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Attendance Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Mark/View Attendance", response = result });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Critical error occured!" + ex.Message, response = 500 });
            }
        }

        [HttpPost("ClusterTeachers/ClusterTeachersMeetingAttendanceByMentor")]
        public async Task<IActionResult> ClusterTeachersAttendanceByMentor(ClusterTeachersAttendanceDto Attendance)
        {
            try
            {
                var ReturnVal = await _attendanceRepo.ClusterTeachersAttendanceByMentor(Attendance);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Attendance Marked Successfully", response = 1 });
                else if (ReturnVal == -1)
                    return StatusCode(400, new { status = false, message = "This Meeting schedule does not exists!", response = 400 });
                else if (ReturnVal == -2)
                    return StatusCode(400, new { status = false, message = "You can Mark Attendance on Meeting date only!", response = 400 });
                else if (ReturnVal == -3)
                    return StatusCode(400, new { status = false, message = "Cannot mark Attendance again, it is already Marked!", response = 400 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data! Attendance could not be Saved!", response = 200 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Critical error occured! " + ex.Message, response = 500 });
            }
        }
        [HttpGet("GetStudentAttendanceBySrnDateWise/{srn}/{FromDate}/{ToDate}")]
        public async Task<IActionResult> GetStudentAttendanceBySrnDateWise(string srn, string FromDate, string ToDate)
        {
            try
            {
                var result = await _attendanceRepo.GetStudentAttendanceBySrnDateWise(srn, FromDate, ToDate);

                if (result.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Attendance Not Found!", response = result });
                else
                {
                    // Group the attendance data by Student_ID and Student_Name
                    var groupedAttendance = result.GroupBy(r => new { r.Srn_No, r.Student_Name,r.Class_Name,r.Section_Name })
                        .Select(group => new
                        {
                            Srn_No = group.Key.Srn_No,
                            Student_Name = group.Key.Student_Name,
                            Class_Name = group.Key.Class_Name,
                            Section_Name = group.Key.Section_Name,
                            Attendance = group.Select(item => new
                            {
                                Attendance_Date = item.Attendance_Date,
                                Attendance_Status = item.Attendance_Status
                            }).ToArray()
                        }).ToList();

                    return StatusCode(200, new { status = true, message = "Mark/View Attendance", response = groupedAttendance });
                }
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, new { status = false, message = "Critical error occurred! " + ex.Message, response = 500 });
            }
        }


    }
}
