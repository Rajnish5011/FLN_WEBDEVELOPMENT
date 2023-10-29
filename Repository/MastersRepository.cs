using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Entities;
using Dapper;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class MastersRepository : IMastersRepository
    {
        private readonly DapperContext _context;
        public MastersRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Districts>> GetDistrictByStateId(short id)
        {
            var procedureName = "GetDistrictsByStateID";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int16, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var block = await connection.QueryAsync<Districts>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return block.ToList();
            }
        }
        public async Task<IEnumerable<Blocks>> GetBlocksByDistrictId(short id)
        {
            var procedureName = "GetBlocksByDistrictID";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int16, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<Blocks>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<AssessmentType>> GetAssessmentType()
        {
            var query = "GetAssessmentType";

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<AssessmentType>(query);
                return (IEnumerable<AssessmentType>)data.ToList();

            }
        }
        public async Task<IEnumerable<QuestionType>> GetQuestionType()
        {
            var query = "GetQuestionType";

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<QuestionType>(query);
                return (IEnumerable<QuestionType>)data.ToList();
            }
        }
        public async Task<IEnumerable<Class>> GetClass()
        {
            var query = "GetClass";

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<Class>(query);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<Class>> GetClassBySchoolId(int SchoolId)
        {
            var procedureName = "GetClassBySchoolId";
            var parameters = new DynamicParameters();
            parameters.Add("School_Id", SchoolId, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<Class>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<ClassSection>> GetClassSectionBySchoolId(int SchoolId)
        {
            var procedureName = "GetClassSectionBySchoolId";
            var parameters = new DynamicParameters();
            parameters.Add("SchoolId", SchoolId, DbType.Int16, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<ClassSection>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<Weeks>> GetWeeks()
        {
            var query = "GetWeeks";

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<Weeks>(query);
                return (IEnumerable<Weeks>)data.ToList();

            }
        }
        public async Task<IEnumerable<Months>> GetMonths()
        {
            var query = "GetMonths";

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<Months>(query);
                return (IEnumerable<Months>)data.ToList();

            }
        }
        public async Task<IEnumerable<Roles>> GetAllRoles()
        {
            var query = "GetRole";
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<Roles>(query);
                return data.ToList();

            }
        }
        public async Task<IEnumerable<Designations>> GetDesignationByRole(short RoleId)
        {
            var procedureName = "GetDesignationByRoleId";
            var parameters = new DynamicParameters();
            parameters.Add("RoleId", RoleId, DbType.Int16, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<Designations>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }

    }
}
