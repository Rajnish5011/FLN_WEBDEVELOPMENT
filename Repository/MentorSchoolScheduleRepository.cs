using Dapper;
using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Dto.Mentor;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class MentorSchoolScheduleRepository : IMentorSchoolScheduleRepository
    {
        private readonly DapperContext _context;
        public MentorSchoolScheduleRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MentorSchoolSchedule>> GetMentorSchoolScheduleByMentorId(int MentorID)
        {
            var procedureName = "GetMentorSchoolUpcomingSchedule";
            var parameters = new DynamicParameters();
            parameters.Add("MentorId", MentorID, DbType.Int16, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<MentorSchoolSchedule>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }

        public async Task<MentorProfile> GetMentorProfileByMentorID(int MentorID)
        {
            var procedureName = "GetMentorProfileByMentorId";
            var parameters = new DynamicParameters();
            parameters.Add("MentorID", MentorID, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<MentorProfile>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.SingleOrDefault();
            }
        }
        public async Task<int> CreateMentorSchoolSchedule(CreateMentorSchoolScheduleDto request)
        {
            var ReturnVal= 0;
            var procedureName = "CreateMentorSchoolSchedule";
            var parameters = new DynamicParameters();
            parameters.Add("School_Id", request.School_Id, DbType.Int32);
            parameters.Add("Mentor_Id", request.Mentor_Id, DbType.Int32);
            parameters.Add("Visit_Date", request.Visit_Date, DbType.DateTime);
            parameters.Add("Visit_Time_Start", request.Visit_Time_Start, DbType.String);
            parameters.Add("Visit_Time_End", request.Visit_Time_End, DbType.String);
            parameters.Add("Created_By", request.Mentor_Id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }
        public async Task<int> UpdateMentorSchoolSchedule(int MentorSchoolScheduleId, UpdateMentorSchoolScheduleDto request)
        {
            var ReturnVal = 0;
            var procedureName = "UpdateMentorSchoolSchedule";

            var parameters = new DynamicParameters();
            parameters.Add("Mentor_School_Schedule_Id", MentorSchoolScheduleId, DbType.Int32);
            parameters.Add("School_Id", request.School_Id, DbType.Int32);
            parameters.Add("Visit_Date", request.Visit_Date, DbType.DateTime);
            parameters.Add("Visit_Time_Start", request.Visit_Time_Start, DbType.String);
            parameters.Add("Visit_Time_End", request.Visit_Time_End, DbType.String);
            parameters.Add("Modified_By", request.Modified_By, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);                
            }
            return ReturnVal;
       }
        public async Task<int> MentorSchoolScheduleVisitEnd(int MentorSchoolScheduleId)
        {
            var ReturnVal = 0;
            var procedureName = "MentorSchoolScheduleVisitEnd";
            var parameters = new DynamicParameters();
            parameters.Add("MentorSchoolScheduleId", MentorSchoolScheduleId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }

        public async Task<MentorMonthlyTarget> MentorMonthlyTargetByMentorId(int MentorID,DateTime GetMonthFromDate)
        {
            var procedureName = "GetMentorTargetByMentorID";
            var parameters = new DynamicParameters();
            parameters.Add("MentorID", MentorID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("GetMonthYear", GetMonthFromDate, DbType.Date, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<MentorMonthlyTarget>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.SingleOrDefault();
            }
        }
    }
}
