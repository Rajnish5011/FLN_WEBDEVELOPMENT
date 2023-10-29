using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto;
using ASPNetCoreFLN_APIs.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly DapperContext _context;
        public AttendanceRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<StudentAttendance>> GetStudentAttendanceByDateSchoolClassSection(string AttendanceDate, int SchoolId, byte ClassId, byte SectionId)
        {
            var procedureName = "GetStudentAttendanceByDateSchoolClassSection";
            var parameters = new DynamicParameters();
            
            parameters.Add("Attendance_Date", AttendanceDate, DbType.String, ParameterDirection.Input);
            parameters.Add("School_Id", SchoolId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Class_Id", ClassId, DbType.Byte, ParameterDirection.Input);
            parameters.Add("Section_Id", SectionId, DbType.Byte, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<StudentAttendance>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<StudentsAttedanceByTeacher>> GetStudentAttendanceByTeacherId(int TeacherId)
        {
            var procedureName = "GetStudentAttendanceByTeacherId";
            var parameters = new DynamicParameters();
            parameters.Add("TeacherId", TeacherId, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<StudentsAttedanceByTeacher>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }
        public async Task<int> StudentAttendanceByTeacherSave(StudentAttendanceDto request)
        {
            int ReturnVal = 0;
            string AllStudentIDs = "";
            string AllPresense = "";
            string StudentIDs = "";
            string Presense = "";
            if (request.Attendance.ToList().Count > 0)
            {
                foreach (var item in request.Attendance)
                {
                    AllStudentIDs = AllStudentIDs + Convert.ToString(item.Student_Id) + ",";
                    AllPresense = AllPresense + Convert.ToString(item.Is_Present) + ",";
                }
                try
                {
                    StudentIDs = AllStudentIDs.Remove(AllStudentIDs.Length - 1);
                    Presense = AllPresense.Remove(AllPresense.Length - 1);
                }
                catch
                {
                    StudentIDs = AllStudentIDs;
                    Presense = AllPresense;
                }
                var procedureName = "ClassStudentAttendanceByTeacherSave";
                var parameters = new DynamicParameters();
                parameters.Add("Attendance_Date", request.Attendance_Date, DbType.Date);
                parameters.Add("School_Id", request.School_Id, DbType.Int32);
                parameters.Add("Class_Id", request.Class_Id, DbType.Byte);
                parameters.Add("Section_Id", request.Section_Id, DbType.Byte);
                parameters.Add("Teacher_Id", request.Teacher_Id, DbType.Int32);
                parameters.Add("Student_Ids", StudentIDs, DbType.String);
                parameters.Add("Student_Presence", Presense, DbType.String);

                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                }
                return ReturnVal;
            }
            else
                return -2;
        }
        public async Task<int> ClusterTeachersAttendanceByMentor(ClusterTeachersAttendanceDto request)
        {
            int ReturnVal = 0;
            string AllTeacherIDs = "";
            string AllPresense = "";
            string TeacherIDs = "";
            string Presense = "";

            if (request.Attendance.ToList().Count > 0)
            {
                foreach (var item in request.Attendance)
                {
                    AllTeacherIDs = AllTeacherIDs + Convert.ToString(item.Teacher_Id) + ",";
                    AllPresense = AllPresense + Convert.ToString(item.Is_Present) + ",";
                }
                try
                {
                    TeacherIDs = AllTeacherIDs.Remove(AllTeacherIDs.Length - 1);
                    Presense = AllPresense.Remove(AllPresense.Length - 1);
                }
                catch
                {
                    TeacherIDs = AllTeacherIDs;
                    Presense = AllPresense;
                }
                var procedureName = "CreateClusterTeacherMeetingAttendanceByMentor";
                var parameters = new DynamicParameters();                
                parameters.Add("Cluster_Meeting_Schedule_Id", request.Cluster_Meeting_Schedule_Id, DbType.Int32);
                parameters.Add("Mentor_Id", request.Mentor_Id, DbType.Int32);
                parameters.Add("Teacher_Ids", TeacherIDs, DbType.String);
                parameters.Add("Attendance", Presense, DbType.String);

                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                }
                return ReturnVal;
            }
            else
                return -2;
        }
        public async Task<IEnumerable<StudentAttendanceBySrn>> GetStudentAttendanceBySrnDateWise(string srn, string FromDate, string ToDate)
        {
            var procedureName = "GetStudentAttendanceBySrnDateWise";
            var parameters = new DynamicParameters();
            parameters.Add("srn", srn, DbType.String, ParameterDirection.Input);
            parameters.Add("FromDate", FromDate, DbType.String, ParameterDirection.Input);
            parameters.Add("ToDate", ToDate, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<StudentAttendanceBySrn>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }
    }
}

