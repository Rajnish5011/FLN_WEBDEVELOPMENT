using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto;
using ASPNetCoreFLN_APIs.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly DapperContext _context;
        public SchoolRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ClusterSchoolList>> GetClusterSchoolListByBlockId(short BlockId)
        {
            var procedureName = "GetClusterSchoolListByBlockId";
            var parameters = new DynamicParameters();
            parameters.Add("BlockId", BlockId, DbType.Int16, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<ClusterSchoolList>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<SchoolList>> GetSchoolListByClusterSchoolId(int ClusterSchoolId)
        {
            var procedureName = "GetSchoolListByClusterSchoolId";
            var parameters = new DynamicParameters();
            parameters.Add("ClusterSchoolId", ClusterSchoolId, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<SchoolList>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<School>> GetSchoolListByClusterSchoolUdiseCode(string ClusterUdiseCode)
        {
            var procedureName = "GetSchoolListByClusterSchoolUdiseCode";
            var parameters = new DynamicParameters();
            parameters.Add("UdiseCode", ClusterUdiseCode, DbType.String, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<School>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();                
            }
        }
        public async Task<IEnumerable<School>> GetSchoolByMentorId(int MentorId)
        {
            var procedureName = "GetSchoolListByMentorId";
            var parameters = new DynamicParameters();
            parameters.Add("MentorId", MentorId, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<School>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<int> TeacherToClassSectionAllocationSave(AllocateTeacherToClassSectionSave request)
        {
            var ReturnVal = 0;
            var procedureName = "TeacherClassSectionAllocationSave";

            var parameters = new DynamicParameters();
            parameters.Add("School_Teacher_Id", request.School_Teacher_Id, DbType.Int32);
            parameters.Add("Class_Id", request.Class_Id, DbType.Byte);
            parameters.Add("Section_Id", request.Section_Id, DbType.Byte);
            parameters.Add("Created_By", request.Section_Id, DbType.Byte);
            
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    //ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                    ReturnVal = await connection.ExecuteAsync(procedureName, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                    
                    if (ReturnVal > 0)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
            }
            return ReturnVal;
        }

        public async Task<int> TeacherToClassSectionAllocationUpdate(int TeacherClassSectionId, AllocateTeacherToClassSectionUpdate request)
        {
            var ReturnVal = 0;
            var procedureName = "TeacherClassSectionAllocationUpdate";

            var parameters = new DynamicParameters();
            parameters.Add("Teacher_Class_Section_Id", TeacherClassSectionId, DbType.Int32);            
            parameters.Add("Class_Id", request.Class_Id, DbType.Byte);
            parameters.Add("Section_Id", request.Section_Id, DbType.Byte);
            parameters.Add("Modified_By", request.Modified_By, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();

                    if (ReturnVal == 0)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return ReturnVal;
        }

        public async Task<int> DeleteTeacherClassSection(int TeacherClassSectionId)
        {
            var ReturnVal = 0;
            var procedureName = "DeleteTeacherClassSectionByTeacherClassSectionId";

            var parameters = new DynamicParameters();
            parameters.Add("TeacherClassSectionId", TeacherClassSectionId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);

            }
            return ReturnVal;
        }
        public async Task<IEnumerable<SchoolListByMentorSchoolCode>> SearchSchoolforStateLevelMonitorByMentorIdSchoolCode(int MentorId, string SchoolCode)
        {
            var procedureName = "SearchSchoolforStateLevelByMentorIdSchoolCode";
            var parameters = new DynamicParameters();
            parameters.Add("MentorId", MentorId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("SchoolCode", SchoolCode, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<SchoolListByMentorSchoolCode>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
    }
}
