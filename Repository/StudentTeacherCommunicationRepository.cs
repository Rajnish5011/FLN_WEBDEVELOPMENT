using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto;
using ASPNetCoreFLN_APIs.Dto.StudentTeacherCommunication;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class StudentTeacherCommunicationRepository : IStudentTeacherCommunicationRepository
    {

        private readonly DapperContext _context;
        public StudentTeacherCommunicationRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<StudentTeacherMessagesDto>> GetStudentTeacherChats(int sender_Id, int receiver_Id, int pageNumber, int pageSize)
        {
            try
            {
                var procedureName = "GetStudentTeacherChats";
                var parameters = new DynamicParameters();
                parameters.Add("@SENDER_ID", sender_Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@RECEIVER_ID", receiver_Id, DbType.Int32, ParameterDirection.Input);

                using var connection = _context.CreateConnection();
                var data = await connection.QueryAsync<StudentTeacherMessagesDto>(
                    procedureName,
                    parameters,
                    commandType: CommandType.StoredProcedure);
                var paginatedData = data
                    .OrderByDescending(item => item.CreateDate)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var paginatedResult = new PaginatedResult<StudentTeacherMessagesDto>
                {
                    Items = paginatedData,
                    TotalCount = data.Count(),
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
                return paginatedResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> StudentTeacherCommunication(StudentTeacherCommunicationDto communicationDto)
        {
            try
            {
                var procedureName = "StudentTeacherCommunication_Insert";
                var parameters = new DynamicParameters();
                parameters.Add("@SENDER_ID", communicationDto.Sender_Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@RECEIVER_ID", communicationDto.Receiver_Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@ROLE_ID", communicationDto.Role_Id, DbType.Int16, ParameterDirection.Input);
                parameters.Add("@CREATEDATE", communicationDto.CreateDate, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@MESSAGE_TEXT", communicationDto.Message_Text, DbType.String, ParameterDirection.Input);
                parameters.Add("@RETURNVALUE", dbType: DbType.Int32, direction: ParameterDirection.Output); // Add an output parameter
                using var connection = _context.CreateConnection();
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
                // Retrieve the return value from the output parameter
                var returnValue = parameters.Get<int>("@RETURNVALUE");
                return returnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ChatTeacherResponse>> GetTecherListToCommunication(int class_Id, int section_Id, int school_Id)
        {
            try
            {
                var procedureName = "GetTecherForChat";
                var parameters = new DynamicParameters();
                parameters.Add("@CLASS_ID", class_Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@SECTION_ID", section_Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@SCHOOL_ID", school_Id, DbType.Int32, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    var data = await connection.QueryAsync<ChatTeacherResponse>
                      (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return data.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task<List<ChatStudentResponse>> GetStudentListToCommunication(int class_Id,  int section_Id, int school_Id)
        {
            var procedureName = "GetStudentDetailsToCommunication";
            var parameters = new DynamicParameters();
            parameters.Add("@CLASS_ID", class_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@SECTION_ID", section_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@SCHOOL_ID", school_Id, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<ChatStudentResponse>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }
    }
}
