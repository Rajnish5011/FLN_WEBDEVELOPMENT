using Dapper;
using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using ASPNetCoreFLN_APIs.Entities;
using System.Linq;
using ASPNetCoreFLN_APIs.Dto.Mentor;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class MentorSchoolScheduleVisitRepository : IMentorSchoolScheduleVisitRepository
	{
		private readonly DapperContext _context;

		public MentorSchoolScheduleVisitRepository(DapperContext context)
		{
			_context = context;
		}
        public async Task<int> CreateMentorSchoolScheduleVisit(MentorSchoolScheduleVisitDto request)
		{
			var ReturnVal = 0;
			var procedureName = "CreateMentorSchoolScheduleVisit";
			var parameters = new DynamicParameters();
			parameters.Add("Mentor_School_Schedule_Id", request.Mentor_School_Schedule_Id, DbType.Int32);
			parameters.Add("Mentor_Id", request.Mentor_Id, DbType.Int32);			
			parameters.Add("Class_Id", request.Class_Id, DbType.Byte);
			parameters.Add("Section_Id", request.Section_Id, DbType.Byte);
			parameters.Add("Teacher_Id", request.Teacher_Id, DbType.Int32);
			parameters.Add("Subject_Id", request.Subject_Id, DbType.Byte);

			using (var connection = _context.CreateConnection())
			{
				connection.Open();

				using (var transaction = connection.BeginTransaction())
                {
					ReturnVal =  await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: transaction,commandType: CommandType.StoredProcedure);
					transaction.Commit();										

					if (ReturnVal == 0)
					{
						transaction.Rollback();
					}					
				}				
			}
			return ReturnVal;
		}
        public async Task<IEnumerable<MentorPastVisitBySchoolSheduleId>> GetMentorPastVisitByMentorSchoolScheduleId(int MentorSchoolScheduleId)
        {
			var procedureName = "GetMentorPastVisitByMentorSchoolScheduleId";
			var parameters = new DynamicParameters();
			parameters.Add("MentorSchoolScheduleId", MentorSchoolScheduleId, DbType.Int32, ParameterDirection.Input);
			using (var connection = _context.CreateConnection())
			{
				var data = await connection.QueryAsync<MentorPastVisitBySchoolSheduleId>(procedureName, parameters, commandType: CommandType.StoredProcedure);
				return data.ToList();
			}
		}
		public async Task<int> MentorSchoolClassVisitEnd(int MentorSchoolVisitId)
		{
			var ReturnVal = 0;
			var procedureName = "MentorSchoolScheduledClassVisitEnd";
			var parameters = new DynamicParameters();
			parameters.Add("MentorSchoolVisitId", MentorSchoolVisitId, DbType.Int32);
            using (var connection = _context.CreateConnection())
			{
				ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
			}
			return ReturnVal;
		}
		public async Task<int> UpdatePresentStudentCount(int MentorSchoolVisitId,UpdatePresentStudentCountDto request)
		{
			var ReturnVal = 0;
			var procedureName = "UpdateMentorSchoolScheduleVisitByMentorSchoolScheduleVisitId";
			var parameters = new DynamicParameters();
			parameters.Add("Mentor_School_Visit_Id", MentorSchoolVisitId, DbType.Int32);
			parameters.Add("Present_Male_Student", request.Present_Male_Student, DbType.Int16);
			parameters.Add("Present_Female_Student", request.Present_Female_Student, DbType.Int16);
			using (var connection = _context.CreateConnection())
			{
				ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
			}
			return ReturnVal;
		}
		public async Task<IEnumerable<MentorPastVisit>> GetMentorPastVisitBySchoolId(int SchoolId, int currentPageNumber, int pageSize)
		{
			var procedureName = "GetMentorPastVisitBySchoolId";
			var parameters = new DynamicParameters();
			parameters.Add("SchoolId", SchoolId, DbType.Int32, ParameterDirection.Input);
			parameters.Add("currentPageNumber", currentPageNumber, DbType.Int32, ParameterDirection.Input);
			parameters.Add("pageSize", pageSize, DbType.Int32, ParameterDirection.Input);
			using (var connection = _context.CreateConnection())
			{
				var data = await connection.QueryAsync<MentorPastVisit>(procedureName, parameters, commandType: CommandType.StoredProcedure);
				return data.ToList();
			}
		}
        public async Task<IEnumerable<MentorPastVisitByMentorID>> GetMentorPastVisitByMentorId(int MentorId, int currentPageNumber, int pageSize)
        {
            var procedureName = "GetMentorPastVisitByMentorId";
            var parameters = new DynamicParameters();
            parameters.Add("MentorId", MentorId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("currentPageNumber", currentPageNumber, DbType.Int32, ParameterDirection.Input);
            parameters.Add("pageSize", pageSize, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<MentorPastVisitByMentorID>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }

    }
}
