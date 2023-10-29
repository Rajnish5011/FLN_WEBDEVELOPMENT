using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class SectionRepository : ISectionRepository
    {
        private readonly DapperContext _context;
        public SectionRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Section>> GetSection()
        {
            var query = "GetSection";

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<Section>(query);
                return (IEnumerable<Section>)data.ToList();
            }
        }
        public async Task<IEnumerable<Section>> GetSectionBySchoolClass(int SchoolId,byte ClassId)
        {
            var procedureName = "GetSectionBySchoolClassId";
            var parameters = new DynamicParameters();
            parameters.Add("SchoolId", SchoolId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("ClassId", ClassId, DbType.Byte, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<Section>
                 (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();

            }
        }


    }
}
