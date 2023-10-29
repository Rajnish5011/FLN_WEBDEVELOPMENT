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
    public class TeachersMeetingRepository : ITeachersMeetingRepository
    {
        private readonly DapperContext _context;
        public TeachersMeetingRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ClusterTeacherMeetingSchedule>> GetClusterTeacherMeetingScheduleByMentorId(int MentorID)
        {
            var procedureName = "GetClusterTeacherMeetingScheduleByMentorId";
            var parameters = new DynamicParameters();
            parameters.Add("MentorId", MentorID, DbType.Int16, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<ClusterTeacherMeetingSchedule>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }   
        public async Task<int> CreateClusterTeacherMeetingSchedule(CreateClusterMeetingScheduleDto request)
        {
            var ReturnVal= 0;
            var procedureName = "CreateClusterTeacherMeetingSchedule";
            var parameters = new DynamicParameters();
            parameters.Add("Mentor_Id", request.Mentor_Id, DbType.Int32);
            parameters.Add("Meeting_Date", request.Meeting_Date, DbType.DateTime);
            parameters.Add("Meeting_Time", request.Meeting_Time, DbType.String);
            parameters.Add("Meeting_Place", request.Meeting_Place, DbType.String);
            parameters.Add("Meeting_Agenda", request.Meeting_Agenda, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }      
        public async Task<int> ClusterTeachersMeetingScheduleEnd(int ClusterMeetingScheduleId,int MentorId,string Remarks)
        {
            var ReturnVal = 0;
            var procedureName = "UpdateEndClusterTeacherMeetingSchedule";
            var parameters = new DynamicParameters();
            parameters.Add("ClusterMeetingScheduleId", ClusterMeetingScheduleId, DbType.Int32);
            parameters.Add("MentorId", MentorId, DbType.Int32);
            parameters.Add("Remarks", Remarks, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }
        public async Task<int> UpdateClusterTeachersMeetingSchedule(int ClusterMeetingScheduleId, int MentorId,UpdateClusterMeetingScheduleDto request)
        {
            var ReturnVal = 0;
            var procedureName = "UpdateClusterTeacherMeetingSchedule";
            var parameters = new DynamicParameters();
            parameters.Add("ClusterMeetingScheduleId", ClusterMeetingScheduleId, DbType.Int32);
            parameters.Add("MentorId", MentorId, DbType.Int32);
            parameters.Add("Meeting_Date", request.Meeting_Date, DbType.DateTime);           
            parameters.Add("Meeting_Time", request.Meeting_Time, DbType.String);
            parameters.Add("Meeting_End_Date", request.Meeting_End_Date, DbType.DateTime);
            parameters.Add("Meeting_Agenda", request.Meeting_Agenda, DbType.String);
            parameters.Add("Meeting_Place", request.Meeting_Place, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }
        public async Task<int> DeleteClusterTeachersMeetingSchedule(int ClusterMeetingScheduleId, int MentorId)
        {
            var ReturnVal = 0;
            var procedureName = "DeleteClusterTeacherMeetingSchedule";

            var parameters = new DynamicParameters();
            parameters.Add("@ClusterMeetingScheduleId", ClusterMeetingScheduleId, DbType.Int32);
            parameters.Add("@MentorId", MentorId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);

            }
            return ReturnVal;
        }
    }
}
