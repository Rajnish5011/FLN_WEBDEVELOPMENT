using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto;
using ASPNetCoreFLN_APIs.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DapperContext _context;
        public StudentRepository(DapperContext context)
        {
            _context = context;
        }
        //public async Task<IEnumerable<StudentByClusterUdiseCode>> GetStudentByClusterUDISECode(string ClusterUDISECode, int currentPageNumber, int pageSize)
        //{
        //    var Students = new List<StudentByClusterUdiseCode>();
        //    using (var connection = new SqlConnection(_context.CreateConnection().ConnectionString))
        //    {
        //        await connection.OpenAsync();
        //        using (var command = new SqlCommand("GetA", connection))
        //        {
        //            try
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.AddWithValue("@ClusterUDISECode", ClusterUDISECode);
        //                command.Parameters.AddWithValue("@currentPageNumber", currentPageNumber);
        //                command.Parameters.AddWithValue("@pageSize", pageSize);
        //                using (var reader = await command.ExecuteReaderAsync())
        //                {
        //                    if (reader.HasRows)
        //                    {                                
        //                        while (await reader.ReadAsync())
        //                        {
        //                            Students.Add(new StudentByClusterUdiseCode
        //                            {
        //                                Id = Convert.ToInt32(reader["Id"]),
        //                                Srn = Convert.ToInt64(reader["Srn"]),
        //                                Name = reader["Name"].ToString(),
        //                                TotalRecords = Convert.ToInt32(reader["Id"])
        //                            });
        //                        }
        //                        await reader.CloseAsync();
        //                    }                            
        //                }
        //                await connection.CloseAsync();
        //            }
        //            catch
        //            {
        //                if (connection.State != ConnectionState.Closed)
        //                {
        //                    await connection.CloseAsync();
        //                }
        //            }
        //        }
        //        return Students.ToList();
        //    }          
        //}
        public async Task<IEnumerable<StudentByClusterUdiseCode>> GetStudentByClusterUDISECode(string ClusterUDISECode, int currentPageNumber, int pageSize)
        {
            var procedureName = "GetStudentNameSrNoByClusterUDISECode";
            var parameters = new DynamicParameters();
            parameters.Add("ClusterUDISECode", ClusterUDISECode, DbType.String, ParameterDirection.Input);
            parameters.Add("currentPageNumber", currentPageNumber, DbType.Int32, ParameterDirection.Input);
            parameters.Add("pageSize", pageSize, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<StudentByClusterUdiseCode>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }

        public async Task<IEnumerable<Student>> GetStudentData(int SchoolId, byte ClassId, byte SectionId)
        {
            var procedureName = "GetStudentWithSrnNoBySchoolClassSectionId";
            var parameters = new DynamicParameters();
            parameters.Add("School_Id", SchoolId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Class_Id", ClassId, DbType.Byte, ParameterDirection.Input);
            parameters.Add("Section_Id", SectionId, DbType.Byte, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<Student>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }


        public async Task<IEnumerable<TotalNoOfStudent>> GetStudentCountByMentorSchoolVisitId(int MentorSchoolVisitId)
        {
            var procedureName = "GetStudentCountByMentorSchoolVisitId";
            var parameters = new DynamicParameters();
            parameters.Add("MentorSchoolVisitId", MentorSchoolVisitId, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<TotalNoOfStudent>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }
        public async Task<IEnumerable<StudentPeriodicStatus>> GetStudentListByPeriodicStatus(int Id, int SchoolId, byte ClassId, byte SectionId)
        {
            var procedureName = "GetStudentListingByPeriodicExamStatus";
            var parameters = new DynamicParameters();
            parameters.Add("Periodic_Assessment_Schedule_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("School_Id", SchoolId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Class_Id", ClassId, DbType.Byte, ParameterDirection.Input);
            parameters.Add("Section_Id", SectionId, DbType.Byte, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<StudentPeriodicStatus>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<Student>> GetStudentListSpotAssessmentWiseBySchoolClassSectionId(int SchoolId, byte ClassId, byte SectionId)
        {
            var procedureName = "GetStudentListSpotAssessmentWiseBySchoolClassSectionId";
            var parameters = new DynamicParameters();
            parameters.Add("School_Id", SchoolId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Class_Id", ClassId, DbType.Byte, ParameterDirection.Input);
            parameters.Add("Section_Id", SectionId, DbType.Byte, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<Student>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }

        public async Task<IEnumerable<Student>> GetStudentListSpotAssessmentWiseByMentorSchoolVisitId(int MentorSchoolVisitId)
        {
            var procedureName = "GetStudentListSpotAssessmentWiseByMentorSchoolVisitId";
            var parameters = new DynamicParameters();
            parameters.Add("MentorSchoolVisitId", MentorSchoolVisitId, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<Student>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }

        public async Task<IEnumerable<SpotDoneStudentMasteryStatus>> GetStudentMasteryStatusByMentorSchoolVisitId(int MentorSchoolVisitId)
        {
            var procedureName = "GetStudentMasteryStatusByMentorSchoolVisitId";
            var parameters = new DynamicParameters();
            parameters.Add("MentorSchoolVisitId", MentorSchoolVisitId, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<SpotDoneStudentMasteryStatus>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }

    }
}


