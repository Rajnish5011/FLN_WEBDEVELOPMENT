using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto.PeriodicAssessment;
using ASPNetCoreFLN_APIs.Entities;
using ASPNetCoreFLN_APIs.TeacherTrainingDto;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class ContentRepository : IContentRepository
    {
        private readonly DapperContext _context;
        public ContentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContentRepositoryCategory>> GetContentRepositoryCategory()
        {
            var query = "GetContentRepositoryCategory";

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<ContentRepositoryCategory>(query);
                return (IEnumerable<ContentRepositoryCategory>)data.ToList();
            }
        }
        public async Task<IEnumerable<AppContentRepository>> GetAppContentRepositoryByContentForRoleID(int ContentForRoleId, int currentPageNumber, int pageSize)
        {
            var procedureName = "GetAppContentRepositoryByContentForRoleID";
            var parameters = new DynamicParameters();
            parameters.Add("ContentForRoleId", ContentForRoleId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("currentPageNumber", currentPageNumber, DbType.Int32, ParameterDirection.Input);
            parameters.Add("pageSize", pageSize, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<AppContentRepository>(procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<int> AddContentForRoleSave(AddContentForRoleDto request)
        {
            var procedureName = "CreateContentRepository";
            var ReturnId = 0;
            var parameters = new DynamicParameters();
            parameters.Add("@Content_Category_Id", request.Content_Category_Id, DbType.Int32);
            parameters.Add("@Content_For_Role_Id", request.Content_For_Role_Id, DbType.Byte);
            parameters.Add("@Content_Title", request.Content_Title, DbType.String);
            parameters.Add("@Content_Description", request.Content_Description, DbType.String);
            parameters.Add("@Content_Url", request.Content_Url, DbType.String);
            parameters.Add("@User_Id", request.User_Id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                ReturnId = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnId;
        }
    }
}
