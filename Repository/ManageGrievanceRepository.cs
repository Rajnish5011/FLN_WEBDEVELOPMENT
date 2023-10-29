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
using ASPNetCoreFLN_APIs.Dto.Grievance;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class ManageGrievanceRepository : IManageGrievanceRepository
    {
        private readonly DapperContext _context;
        public ManageGrievanceRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<GrievanceCategories>> GetGrievanceCategoriesByGrievanceForRoleID(int GrievanceForRoleID)
        {
            var procedureName = "GetGrievanceCategoriesByGrievanceForRoleID";
            var parameters = new DynamicParameters();
            parameters.Add("GrievanceForRoleID", GrievanceForRoleID, DbType.Int16, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<GrievanceCategories>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<GrievanceTicketDetails>> GetGrievanceDetailsByTicketID(int TicketID)
        {
            var procedureName = "GetGrievanceTicketDetailsByTicketID";
            var parameters = new DynamicParameters();
            parameters.Add("TicketID", TicketID, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<GrievanceTicketDetails>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<GrievanceTicketList>> GetGrievanceTicketsRaisedByUsers(bool IsClosed)
        {
            var procedureName = "GetGrievanceTicketsRaisedByUsers";
            var parameters = new DynamicParameters();
            parameters.Add("Is_Closed", IsClosed, DbType.Boolean, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<GrievanceTicketList>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }

        public async Task<IEnumerable<GrievanceTicketListByUser>> GetGrievanceTicketListDetailRaisedByUser(int UserId, byte RoleId)
        {
            var procedureName = "GetTicketsListDetailRaisedByUser";
            var parameters = new DynamicParameters();
            parameters.Add("UserId", UserId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("RoleId", RoleId, DbType.Byte, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<GrievanceTicketListByUser>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<int> CreateGrievanceTicketByUser(CreateGrievanceDto request)
        {
            var ReturnVal = 0;
            var procedureName = "CreateGrievanceTicketByUser";
            var parameters = new DynamicParameters();
            parameters.Add("User_Id", request.User_Id, DbType.Int32);
            parameters.Add("Grievance_Category_Id", request.Grievance_Category_Id, DbType.Int32);
            parameters.Add("Date_Of_Issue", request.Date_Of_Issue, DbType.DateTime);
            parameters.Add("Grievance_Query", request.Grievance_Query, DbType.String);
            parameters.Add("Contact_Number", request.Contact_Number, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }
        //public async Task<int> CreateNewGrievanceCategory(CreateGrievanceCategoryDto request)
        //{
        //    var ReturnVal= 0;
        //    var procedureName = "CreateGrievanceCategoryByUser";
        //    var parameters = new DynamicParameters();
        //    parameters.Add("Grievance_Category_Id", request.Grievance_Category_Id, DbType.Int32);
        //    parameters.Add("User_Id", request.User_Id, DbType.Int32);
        //    parameters.Add("Category_Name", request.Category_Name, DbType.String);           

        //    using (var connection = _context.CreateConnection())
        //    {
        //        ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
        //    }
        //    return ReturnVal;
        //}
        public async Task<int> GrievanceTicketReplyByHelpDeskUser(int TicketId, int HelpDeskUserId, string GrievanceReply)
        {
            var ReturnVal = 0;
            var procedureName = "UpdateCloseGrievanceTicketWithReplyByHelpDeskUser";
            var parameters = new DynamicParameters();
            parameters.Add("TicketId", TicketId, DbType.Int32);
            parameters.Add("HelpDeskUserId", HelpDeskUserId, DbType.Int32);
            parameters.Add("GrievanceReply", GrievanceReply, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }
    }
}
