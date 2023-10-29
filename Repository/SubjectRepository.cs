using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Entities;
using System.Data;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DapperContext _context;
        public SubjectRepository(DapperContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Subject>> GetSubject()
        {
            var procedureName = "GetSubject";
            var parameters = new DynamicParameters();

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<Subject>
                  (procedureName, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<SubjectCompetancy>> GetSubjectCompetancy(byte SubjectId, short SubjectTopicId)
        {
            var procedureName = "GetSubjectCompetancyBySubjectIdSubjectTopicId";
            var parameters = new DynamicParameters();
            parameters.Add("Subject_Id", SubjectId, DbType.Byte, ParameterDirection.Input);
            parameters.Add("Subject_Topic_Id", SubjectTopicId, DbType.Int16, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<SubjectCompetancy>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<SubjectTopic>> GetSubjectTopicBySubjectId(byte id)
        {
            var procedureName = "GetSubjectTopicBySubjectId";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Byte, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<SubjectTopic>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<SubjectCompetancyDetail>> GetCompetancyByClassSubjectId(byte ClassId, byte SubjectId)
        {
            var procedureName = "GetCompetancyBySubjectClassId";
            var parameters = new DynamicParameters();
            parameters.Add("SubjectId", SubjectId, DbType.Byte, ParameterDirection.Input);
            parameters.Add("ClassId", ClassId, DbType.Byte, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<SubjectCompetancyDetail>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<SpotSubjectCompetancy>> GetSpotCompetancyByMonthClassSubjectId(DateTime GetMonthFromDate, byte ClassId, byte SubjectId)
        {
            var procedureName = "GetSpotSubjectCompetancyByMonthClassSubjectId";
            var parameters = new DynamicParameters();
            
            parameters.Add("@GetMonthFromDate", GetMonthFromDate, DbType.Date, ParameterDirection.Input);
            parameters.Add("@SubjectId", SubjectId, DbType.Byte, ParameterDirection.Input);
            parameters.Add("@ClassId", ClassId, DbType.Byte, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<SpotSubjectCompetancy>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }

    }
}
