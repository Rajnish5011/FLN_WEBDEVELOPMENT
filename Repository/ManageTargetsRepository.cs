using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto;
using ASPNetCoreFLN_APIs.Dto.PeriodicAssessment;
using ASPNetCoreFLN_APIs.Entities;
using ASPNetCoreFLN_APIs.TeacherTrainingDto;
using Azure.Core;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class ManageTargetsRepository : IManageTargetsRepository
    {
        private readonly DapperContext _context;
        public ManageTargetsRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MonthlyTargetList>> GetMonthlyMentorsTarget(bool IsCurrentYear,int RoleId)
        {
            var procedureName = "dbo.GetMonthlyTargetList";
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IsCurrentYear", IsCurrentYear, DbType.Boolean);
                parameters.Add("@RoleId", RoleId, DbType.Int32);
                var data = await connection.QueryAsync<MonthlyTargetList>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return (IEnumerable<MonthlyTargetList>)data.ToList();
            }
        }      
        public async Task<int> AddMonthlyTargetForMentorOrMonitor(MonthlyTarget request)
        {
            var procedureName = "CreateMentorMonthlyTarget";
            var ReturnId = 0;
            var parameters = new DynamicParameters();
            parameters.Add("@State_Admin_User_Id", request.State_Admin_User_Id, DbType.Int32);            
            parameters.Add("@Target_Title", request.Target_Title, DbType.String);
            parameters.Add("@Role_Id", request.Role_Id, DbType.Byte);
            parameters.Add("@Designation_Id", request.Designation_Id, DbType.Byte);
            parameters.Add("@Target_Unique_School_Visit", request.Target_Unique_School_Visit, DbType.Byte);
            parameters.Add("@Target_Classroom_Observation", request.Target_Classroom_Observation, DbType.Int16);
            parameters.Add("@Target_Spot_Assessment", request.Target_Spot_Assessment, DbType.Int16);
            parameters.Add("@GetMonthYear", request.GetMonthYear, DbType.Date);

            using (var connection = _context.CreateConnection())
            {
                ReturnId = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnId;
        }
        public async Task<int> UpdateMonthlyMentorsTarget(UpdateMonthlyTarget request)
        {
            var procedureName = "dbo.UpdateMentorMonthlyTarget";
            var ReturnValue = 0;
            var parameters = new DynamicParameters();
            parameters.Add("@Monthly_Target_Id", request.Monthly_Target_Id, DbType.Int32);
            parameters.Add("@State_Admin_User_Id", request.State_Admin_User_Id, DbType.Int32);
            parameters.Add("@Target_Title", request.Target_Title, DbType.String);
            parameters.Add("@Target_Unique_School_Visit", request.Target_Unique_School_Visit, DbType.Byte);
            parameters.Add("@Target_Classroom_Observation", request.Target_Classroom_Observation, DbType.Int16);
            parameters.Add("@Target_Spot_Assessment", request.Target_Spot_Assessment, DbType.Int16);

            using (var connection = _context.CreateConnection())
            {
                ReturnValue = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnValue;
        }
    }
}
