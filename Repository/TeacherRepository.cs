using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto;
using ASPNetCoreFLN_APIs.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly DapperContext _context;
        public TeacherRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BlockTeacherList>> GetBlockTeacherListByMentorID(int MentorId)
        {
            var procedureName = "GetBlockTeacherListByMentorID";
            var parameters = new DynamicParameters();
            parameters.Add("MentorID", MentorId, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<BlockTeacherList>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }
        public async Task<IEnumerable<BlockTeacherList>> GetBlockTeacherListByBlockID(int BlockID)
        {
            var procedureName = "GetTeacherListByBlockID";
            var parameters = new DynamicParameters();
            parameters.Add("BlockID", BlockID, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<BlockTeacherList>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }
        public async Task<IEnumerable<TeacherListByClusterSchoolCode>> GetTeacherByClusterSchoolCode(string ClusterSchoolCode)
        {
            var procedureName = "GetTecherByClusterSchoolCode";
            var parameters = new DynamicParameters();
            parameters.Add("Cluster_School_Code", ClusterSchoolCode, DbType.String, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<TeacherListByClusterSchoolCode>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }
        public async Task<IEnumerable<TeacherClassSection>> GetTeacherListBySchoolId(int SchoolId)
        {
            var procedureName = "GetTeacherListBySchoolID";
            var parameters = new DynamicParameters();
            parameters.Add("SchoolId", SchoolId, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<TeacherClassSection>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<TeacherListBySchool>> GetTeacherBySchoolClassSection(int SchoolId, short ClassId, short SectionId)
        {
            var procedureName = "GetTeacherBySchoolClassSectionID";
            var parameters = new DynamicParameters();
            parameters.Add("SchoolId", SchoolId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("ClassId", ClassId, DbType.Byte, ParameterDirection.Input);
            parameters.Add("SectionId", SectionId, DbType.Byte, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<TeacherListBySchool>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<MentorFeedBackToTeacher>> GetMentorFeedBackToTeacher(int TeacherId)
        {
            var procedureName = "GetMentorFeedBackToTeacher";
            var parameters = new DynamicParameters();
            parameters.Add("TeacherId", TeacherId, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<MentorFeedBackToTeacher>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<TeacherByEmployeeCode> GetTeacherByEmployeeCode(string EmployeeCode)
        {
            var procedureName = "GetTeacherByEmployeeCode";
            var parameters = new DynamicParameters();
            parameters.Add("EmployeeCode", EmployeeCode, DbType.String, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<TeacherByEmployeeCode>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList().SingleOrDefault();
            }
        }
        public async Task<TeacherProfile> GetTeacherProfileByTeacherId(int TeacherId)
        {
            var procedureName = "GetTeacherInformationByTeacherId";
            var parameters = new DynamicParameters();
            parameters.Add("TeacherId", TeacherId, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<TeacherProfile>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList().SingleOrDefault();
            }
        }
        public async Task<IEnumerable<ClusterTeacherMeetingScheduleForTeacher>> GetClusterTeacherMeetingScheduleByTeacherId(int TeacherID)
        {
            var procedureName = "GetClusterTeacherMeetingScheduleByTeacherId";
            var parameters = new DynamicParameters();
            parameters.Add("TeacherId", TeacherID, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<ClusterTeacherMeetingScheduleForTeacher>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<TeacherListBySrnNo>> GetTeacherListBySrnNo(string Srn_No)
        {
            var procedureName = "GetTeacherListBySrnNo";
            var parameters = new DynamicParameters();
            parameters.Add("Srn_No", Srn_No, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<TeacherListBySrnNo>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }

    }
}

