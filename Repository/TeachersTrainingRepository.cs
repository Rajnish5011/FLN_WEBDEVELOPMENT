using Dapper;
using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.TeacherTrainingDto;
using Microsoft.AspNetCore.Mvc.Formatters;
using ASPNetCoreFLN_APIs.Dto;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class TeachersTrainingRepository : ITeachersTrainingRepository
    {
        private readonly DapperContext _context;
        public TeachersTrainingRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TeacherTrainingSchedule>> GetTeacherTrainingScheduleByBlockAdmin(int Block_Admin_User_Id)
        {
            var procedureName = "GetTeachersTrainingScheduleByBlockAdmin";
            var parameters = new DynamicParameters();
            parameters.Add("Block_Admin_User_Id", Block_Admin_User_Id, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var data = await connection.QueryAsync<TeacherTrainingSchedule>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<TeacherTraningQuestions>> GetTeacherTrainingTestQuestionsByStateAdmin(int ScheduleHeaderTestId)
        {
            var procedureName = "GetTeacherTrainingQuestionsByStateAdmin";
            var parameters = new DynamicParameters();
            parameters.Add("@ScheduleHeaderTestId", ScheduleHeaderTestId, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var data = await connection.QueryAsync<TeacherTraningQuestions>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<TeacherTrainingSchedule>> GetTeacherTrainingScheduleByScheduleId(int TeacherTrainingScheduleId, int BlockAdminUserId)
        {
            var procedureName = "GetTeachersTrainingScheduleByBlockAdminAndScheduleId";
            var parameters = new DynamicParameters();
            parameters.Add("@TeacherTrainingScheduleId", TeacherTrainingScheduleId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@BlockAdminUserId", BlockAdminUserId, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var data = await connection.QueryAsync<TeacherTrainingSchedule>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<TrainingScheduleAttendance>> GetTeacherTrainingScheduleAttendance(int TeacherTrainingScheduleId, int BlockAdminUserId)
        {
            var procedureName = "GetTrainingScheduleAttendance";
            var parameters = new DynamicParameters();
            parameters.Add("@Teacher_Training_Schedule_Id", TeacherTrainingScheduleId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Block_Admin_User_Id", BlockAdminUserId, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)                
                    connection.Open();
                
                var data = await connection.QueryAsync<TrainingScheduleAttendance>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);
                
                if (connection.State == ConnectionState.Open)                
                    connection.Close();
                
                return data.ToList();
            }
        }
        
        public async Task<IEnumerable<TrainingScheduleTeachersAttendance>> GetTrainingScheduleTeachersAttendance(int TrainingScheduleAttendanceId, int BlockAdminUserId)
        {
            var procedureName = "GetTrainingScheduleTeachersAttendance";
            var parameters = new DynamicParameters();
            parameters.Add("@Training_Schedule_Attendance_Id", TrainingScheduleAttendanceId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Block_Admin_User_Id", BlockAdminUserId, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var data = await connection.QueryAsync<TrainingScheduleTeachersAttendance>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                if (connection.State == ConnectionState.Open)
                    connection.Close();

                return data.ToList();
            }
        }
        public async Task<IEnumerable<TrainingScheduleHeader>> GetTrainingScheduleHeader(bool Is_Current_Header)
        {
            var procedureName = "GetTrainingScheduleHeaderDetail";
            var parameters = new DynamicParameters();
            parameters.Add("Is_Current_Header", Is_Current_Header, DbType.Boolean, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var data = await connection.QueryAsync<TrainingScheduleHeader>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<TrainingScheduleHeader>> GetTrainingScheduleHeaderByScheduleHeaderId(int Training_Schedule_Header_Id)
        {
            var procedureName = "GetTrainingScheduleHeaderDetailByScheduleHeaderId";
            var parameters = new DynamicParameters();
            parameters.Add("@Training_Schedule_Header_Id", Training_Schedule_Header_Id, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var data = await connection.QueryAsync<TrainingScheduleHeader>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<BlockTeacherList>> GetBlockTeacherListForTrainingByBlockID(int BlockID)
        {
            var procedureName = "GetTeacherListForTraining";
            var parameters = new DynamicParameters();
            parameters.Add("Block_Id", BlockID, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var data = await connection.QueryAsync<BlockTeacherList>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }

        public async Task<int> CreateTeacherTrainingSchedule(CreateTeacherTrainingScheduleDto request)
        {
            var ReturnVal = 0;
            int Teacher_Training_Schedule_Id = 0;
            int Teacher_Training_Schedule_Test_Id = 0;
            bool Is_Inseted = true;

            var procedureName = "CreateTeachersTrainingSchedule";
            var parameters = new DynamicParameters();
            parameters.Add("Training_Schedule_Header_Id", request.Training_Schedule_Header_Id, DbType.Int32);
            parameters.Add("Block_Admin_User_Id", request.Block_Admin_User_Id, DbType.Int32);
            parameters.Add("Block_Id", request.Block_Id, DbType.Int16);
            parameters.Add("Training_Title", request.Training_Title, DbType.String);
            parameters.Add("Training_Start_Date", request.Training_Start_Date, DbType.Date);
            parameters.Add("Training_End_Date", request.Training_End_Date, DbType.Date);
            parameters.Add("Training_Place", request.Training_Place, DbType.String);
            parameters.Add("Training_Start_Time", request.Training_Start_Time, DbType.String);
            parameters.Add("Training_End_Time", request.Training_End_Time, DbType.String);
            parameters.Add("Training_Description", request.Training_Description, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (var trans = connection.BeginTransaction())
                {
                    Teacher_Training_Schedule_Id = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: trans, commandType: CommandType.StoredProcedure);
                    ReturnVal = Teacher_Training_Schedule_Id;

                    if (Teacher_Training_Schedule_Id > 0)
                    {
                        foreach (var item in request.TeacherTest)
                        {
                            procedureName = "CreateTeachersTrainingScheduleTest";
                            parameters = new DynamicParameters();
                            parameters.Add("Teacher_Training_Schedule_Id", Teacher_Training_Schedule_Id, DbType.Int32);
                            parameters.Add("Schedule_Header_Test_Id", item.Schedule_Header_Test_Id, DbType.Int32);
                            parameters.Add("Block_Admin_User_Id", request.Block_Admin_User_Id, DbType.Int32);

                            Teacher_Training_Schedule_Test_Id = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: trans, commandType: CommandType.StoredProcedure);

                            if (Teacher_Training_Schedule_Test_Id == 0)
                            {
                                Is_Inseted = false;
                                ReturnVal = -4;
                            }                            
                        }
                    }
                    else
                        Is_Inseted = false;

                    try
                    {
                        if (Is_Inseted)
                            trans.Commit();
                        else
                            trans.Rollback();
                    }
                    catch (Exception ex) { }
                }
            }

            return ReturnVal;
        }
        public async Task<int> UpdateTeacherTrainingScheduleHeader(UpdateTeacherTrainingScheduleHeaderDto  request)
        {
            var ReturnVal = 0;
            string TrainingTestIds = string.Empty;

            var procedureName = "UpdateTeacherTrainingScheduleHeader";
            var parameters = new DynamicParameters();
            parameters.Add("@Training_Schedule_Header_Id", request.Training_Schedule_Header_Id, DbType.Int32);
            parameters.Add("@State_Admin_User_Id", request.State_User_Id, DbType.Int32);
            parameters.Add("@Schedule_Header_Title", request.Schedule_Header_Title, DbType.String);
            parameters.Add("@Start_Date", request.Start_Date, DbType.Date);
            parameters.Add("@End_Date", request.End_Date, DbType.Date);
            parameters.Add("@Description", request.Description, DbType.String);

            foreach (var test in request.HeaderTest)
            {
                TrainingTestIds += test.Teacher_Training_Test_Id + ",";
            }
            parameters.Add("@TrainingTestIds", TrainingTestIds, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State==ConnectionState.Closed)                
                    connection.Open();
                
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }
        public async Task<int> UpdateBlockTeacherTrainingSchedule(UpdateTeacherTrainingScheduleDto request)
        {
            var ReturnVal = 0;
            var procedureName = "UpdateTeacherTrainingSchedule";
            var parameters = new DynamicParameters();
            parameters.Add("@Teacher_Training_Schedule_Id", request.Teacher_Training_Schedule_Id, DbType.Int32);
            parameters.Add("@Block_Admin_User_Id", request.Block_Admin_User_Id, DbType.Int32);
            parameters.Add("@Training_Title", request.Training_Title, DbType.String);
            parameters.Add("@Training_Start_Date", request.Training_Start_Date, DbType.Date);
            parameters.Add("@Training_End_Date", request.Training_End_Date, DbType.Date);
            parameters.Add("@Training_Start_Time", request.Training_Start_Time, DbType.String);
            parameters.Add("@Training_End_Time", request.Training_End_Time, DbType.String);
            parameters.Add("@Training_Place", request.Training_Place, DbType.String);
            parameters.Add("@Training_Description", request.Training_Description, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }
        public async Task<int> ChangeTeacherTrainingTestStatus(int Teacher_Training_Schedule_Test_Id, bool Is_Active)
        {
            var ReturnVal = 0;
            var procedureName = "UpdateChangeTeacherTrainingTestStatus";
            var parameters = new DynamicParameters();
            parameters.Add("@Teacher_Training_Schedule_Test_Id", Teacher_Training_Schedule_Test_Id, DbType.Int32);
            parameters.Add("@Is_Active", Is_Active, DbType.Boolean);


            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }
        public async Task<int> CreateTrainingScheduleTeacherSelection(TrainingScheduleTeacherSelectionDto request)
        {
            var ReturnVal = 0;
            var procedureName = "CreateTrainingScheduleTeacherSelection";

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                foreach (var item in request.TeacherSelection)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Teacher_Training_Schedule_Id", request.Teacher_Training_Schedule_Id, DbType.Int32);
                    parameters.Add("@Block_Admin_User_Id", request.Block_Admin_User_Id, DbType.Int32);
                    parameters.Add("@Teacher_Id", item.Teacher_Id, DbType.Int32);

                    ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    if (ReturnVal > 0)
                    {
                        ReturnVal = 1;
                    }
                }
            }
            return ReturnVal;
        }        
        public async Task<int> UpdateTeacherTrainingQuestionsGroupByStateAdmin(UpdateTeacherTrainingQuestionsGroup request)
        {
            var ReturnVal = 0;
            var procedureName = "UpdateTeacherTrainingQuestionsGroupByStateAdmin";
            var parameters = new DynamicParameters();
            parameters.Add("@QuestionGroupId", request.Question_Group_Id, DbType.Int32);
            parameters.Add("@StateAdminUserId", request.State_Admin_User_Id, DbType.Int32);
            parameters.Add("@QuestionGroupName", request.Question_Group_Name, DbType.String);            
            parameters.Add("@IsActive", request.Is_Active, DbType.Boolean);


            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }
        public async Task<int> DeleteTeacherTrainingQuestionsGroupByStateAdmin(int QuestionGroupId, int StateAdminUserId)
        {
            var ReturnVal = 0;
            var procedureName = "DeleteTeacherTrainingQuestionsGroupByStateAdmin";
            var parameters = new DynamicParameters();
            parameters.Add("@QuestionGroupId", QuestionGroupId, DbType.Int32);
            parameters.Add("@StateAdminUserId", StateAdminUserId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }        
        public async Task<int> UpdateTeacherTrainingQuestionByStateAdmin(UpdateTeacherTrainingQuestions request)
        {
            var ReturnVal = 0;
            var procedureName = "UpdateTeacherTrainingQuestionByStateAdmin";
            var parameters = new DynamicParameters();
            parameters.Add("@QuestionId", request.Question_Id, DbType.Int32);
            parameters.Add("@StateAdminUserId", request.State_Admin_User_Id, DbType.Int32);
            parameters.Add("@QuestionText", request.Question_Text, DbType.String);
            parameters.Add("@QuestionTypeId", request.Question_Type_Id, DbType.Byte);
            parameters.Add("@MediaTypeId", request.Media_Type_Id, DbType.Byte);
            parameters.Add("@QuestionMarks", request.Question_Marks, DbType.Byte);
            parameters.Add("@IsActive", request.Is_Active, DbType.Boolean);

            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }
        public async Task<int> UpdateTeacherTrainingQuestionOptionByStateAdmin(UpdateTeacherTrainingQuestionOption request)
        {
            var ReturnVal = 0;
            var procedureName = "UpdateTeacherTrainingQuestionOptionByStateAdmin";
            var parameters = new DynamicParameters();
            parameters.Add("@QuestionOptionId", request.Question_Option_Id, DbType.Int32);
            parameters.Add("@StateAdminUserId", request.State_Admin_User_Id, DbType.Int32);
            parameters.Add("@OptionText", request.Option_Text, DbType.String);            
            parameters.Add("@IsCorrect", request.Is_Correct, DbType.Boolean);
            parameters.Add("@IsActive", request.Is_Active, DbType.Boolean);

            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }
        public async Task<int> DeleteTeacherTrainingQuestionByStateAdmin(int QuestionId, int StateAdminUserId)
        {
            var ReturnVal = 0;
            var procedureName = "DeleteTeacherTrainingQuestionByStateAdmin";
            var parameters = new DynamicParameters();
            parameters.Add("@QuestionId", QuestionId, DbType.Int32);
            parameters.Add("@StateAdminUserId", StateAdminUserId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }
        public async Task<int> DeleteTeacherTrainingQuestionOption(int QuestionOptionId, int StateAdminUserId)
        {
            var ReturnVal = 0;
            var procedureName = "DeleteTeacherTrainingQuestionOptionByStateAdmin";
            var parameters = new DynamicParameters();
            parameters.Add("@QuestionOptionId", QuestionOptionId, DbType.Int32);
            parameters.Add("@StateAdminUserId", StateAdminUserId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }
        public async Task<int> DeleteTrainingScheduleHeaderTest(int TrainingScheduleHeaderId, int StateAdminUserId, byte Subject_Id)
        {
            var ReturnVal = 0;
            var procedureName = "DeleteTrainingScheduleHeaderTestByStateAdmin";
            var parameters = new DynamicParameters();
            parameters.Add("@TrainingScheduleHeaderId", TrainingScheduleHeaderId, DbType.Int32);
            parameters.Add("@StateAdminUserId", StateAdminUserId, DbType.Int32);
            parameters.Add("@SubjectId", Subject_Id, DbType.Byte);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }
        public async Task<int> DeleteTeacherFromScheduledTraining(int Training_Schedule_Teacher_Id, int Block_Admin_User_Id)
        {
            var ReturnVal = 0;
            var procedureName = "DeleteTeacherFromScheduledTraining";
            var parameters = new DynamicParameters();
            parameters.Add("@Training_Schedule_Teacher_Id", Training_Schedule_Teacher_Id, DbType.Int32);
            parameters.Add("@Block_Admin_User_Id", Block_Admin_User_Id, DbType.Int32);


            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }

        public async Task<int> DeleteTeacherTrainingScheduleTest(int Training_Schedule_Teacher_Id, int Block_Admin_User_Id, byte Subject_Id)
        {
            var ReturnVal = 0;
            var procedureName = "DeleteTeacherTrainingScheduleTest";
            var parameters = new DynamicParameters();
            parameters.Add("@TrainingScheduleTeacherId", Training_Schedule_Teacher_Id, DbType.Int32);
            parameters.Add("@BlockAdminUserId", Block_Admin_User_Id, DbType.Int32);
            parameters.Add("@SubjectId", Subject_Id, DbType.Byte);


            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }
        
        public async Task<int> DeleteTeacherTrainingSchedule(int TeacherTrainingScheduleId, int Block_Admin_User_Id)
        {
            var ReturnVal = 0;
            var procedureName = "DeleteTeacherTrainingScheduleByBlockAdmin";
            var parameters = new DynamicParameters();
            parameters.Add("@TeacherTrainingScheduleId", TeacherTrainingScheduleId, DbType.Int32);
            parameters.Add("@BlockAdminUserId", Block_Admin_User_Id, DbType.Int32);


            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return ReturnVal;
        }
        public async Task<int> CreateTeacherAttendanceForScheduledTraining(TeacherTrainingAttendanceDto request)
        {
            var Training_Schedule_Attendance_Id = 0;
            var Teacher_Attendance_Id = 0;
            var ReturnVal = 0;
            var Is_Inserted = true;

            var procedureName = "CreateTrainingScheduleAttendance";
            var parameters = new DynamicParameters();
            parameters.Add("@Teacher_Training_Schedule_Id", request.Teacher_Training_Schedule_Id, DbType.Int32);
            parameters.Add("@Block_Admin_User_Id", request.Block_Admin_User_Id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (var trans = connection.BeginTransaction())
                {
                    Training_Schedule_Attendance_Id = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: trans, commandType: CommandType.StoredProcedure);
                    ReturnVal = Training_Schedule_Attendance_Id;

                    if (Training_Schedule_Attendance_Id > 0)
                    {
                        procedureName = "CreateTrainingScheduleTeacherAttendance";
                        foreach (var item in request.TeacherSelection)
                        {
                            parameters = new DynamicParameters();
                            parameters.Add("@Training_Schedule_Attendance_Id", Training_Schedule_Attendance_Id, DbType.Int32);
                            parameters.Add("@Training_Schedule_Teacher_Id", item.Training_Schedule_Teacher_Id, DbType.Int32);
                            parameters.Add("@Is_Present", item.Is_Present, DbType.Boolean);
                            parameters.Add("@Block_Admin_User_Id", request.Block_Admin_User_Id, DbType.Int32);

                            Teacher_Attendance_Id = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: trans, commandType: CommandType.StoredProcedure);

                            if (Teacher_Attendance_Id == 0)
                            {
                                ReturnVal = -2;
                                Is_Inserted = false;
                            }
                        }
                        if (Is_Inserted)
                            trans.Commit();
                        else
                            trans.Rollback();

                    }
                    if (Training_Schedule_Attendance_Id == 0)
                        trans.Rollback();

                }
            }
            return ReturnVal;
        }

        public async Task<IEnumerable<GetSelectedTeachers>> GetSelectedTeachersForTeacherTrainingByBlockAdmin(int BlockAdminUserId, int TeacherTrainingScheduleId)
        {
            var procedureName = "GetSelectedTeachersForTeacherTrainingByBlockAdmin";
            var parameters = new DynamicParameters();
            parameters.Add("BlockAdminUserId", BlockAdminUserId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("TeacherTrainingScheduleId", TeacherTrainingScheduleId, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var data = await connection.QueryAsync<GetSelectedTeachers>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }

        public async Task<IEnumerable<TeacherTrainingSchedule>> GetTeacherTrainingScheduleByTeacherId(int Teacher_Id, bool Is_Upcoming)
        {
            var procedureName = "GetTeachersTrainingScheduleByTeacherId";
            var parameters = new DynamicParameters();
            parameters.Add("Teacher_Id", Teacher_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Is_Upcoming", Is_Upcoming, DbType.Boolean, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var data = await connection.QueryAsync<TeacherTrainingSchedule>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }

        public async Task<IEnumerable<TeacherTrainingSchedule>> GetTeacherTrainingScheduleDetail(int TeacherTrainingScheduleId, int TrainingScheduleTeacherId)
        {
            var procedureName = "GetTeacherTrainingScheduleByTeacherTrainingScheduleId";
            var parameters = new DynamicParameters();
            parameters.Add("@TeacherTrainingScheduleId", TeacherTrainingScheduleId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@TrainingScheduleTeacherId", TrainingScheduleTeacherId, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var data = await connection.QueryAsync<TeacherTrainingSchedule>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<TeacherTrainingTestList>> GetTeacherTrainingHeaderTestList()
        {
            var query = "GetTeacherTrainingHeaderTestList";

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var data = await connection.QueryAsync<TeacherTrainingTestList>(query);
                return (IEnumerable<TeacherTrainingTestList>)data.ToList();
            }
        }
        public async Task<int> CreateTeachersTrainingScheduleHeader(TeachersTrainingScheduleHeaderDto request)
        {
            var ReturnVal = 0;
            int Training_Schedule_Header_Id = 0;
            int Schedule_Header_Test_Id = 0;
            bool Is_Inserted = true;

            var procedureName = "CreateTeachersTrainingScheduleHeader";
            var parameters = new DynamicParameters();
            parameters.Add("State_User_Id", request.State_User_Id, DbType.Int32);
            parameters.Add("Schedule_Header_Title", request.Schedule_Header_Title, DbType.String);
            parameters.Add("Start_Date", request.Start_Date, DbType.Date);
            parameters.Add("End_Date", request.End_Date, DbType.Date);
            parameters.Add("Description", request.Description, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (var trans = connection.BeginTransaction())
                {
                    Training_Schedule_Header_Id = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: trans, commandType: CommandType.StoredProcedure);
                    ReturnVal = Training_Schedule_Header_Id;
                    if (Training_Schedule_Header_Id > 0)
                    {
                        foreach (var item in request.HeaderTest)
                        {
                            procedureName = "CreateTeachersTrainingScheduleHeaderTest";
                            parameters = new DynamicParameters();
                            parameters.Add("Training_Schedule_Header_Id", Training_Schedule_Header_Id, DbType.Int32);
                            parameters.Add("Teacher_Training_Test_Id", item.Teacher_Training_Test_Id, DbType.Byte);
                            parameters.Add("State_User_Id", request.State_User_Id, DbType.Int32);

                            Schedule_Header_Test_Id = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: trans, commandType: CommandType.StoredProcedure);

                            if (Schedule_Header_Test_Id == 0)
                            {
                                Is_Inserted = false;
                            }
                        }
                    }
                    else
                        Is_Inserted = false;

                    try
                    {
                        if (Is_Inserted)
                            trans.Commit();
                        else
                        {
                            trans.Rollback();
                        }
                    }
                    catch (Exception ex) { }
                }
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return ReturnVal;
        }

        public async Task<IEnumerable<TeacherTraningQuestions>> GetTeacherTrainingTestQuestions(int TeacherTrainingScheduleTestId)
        {
            var procedureName = "GetTeacherTrainingQuestionsByTeacherTrainingScheduleTestId";
            var parameters = new DynamicParameters();
            parameters.Add("TeacherTrainingScheduleTestId", TeacherTrainingScheduleTestId, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var data = await connection.QueryAsync<TeacherTraningQuestions>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public string GetBase64StringImageByPath(string mediatype, string webRootPath, string ImagePath)
        {
            var base64imgSrc = "";
            string Imagename = "", ImageFullPath = "";

            if (mediatype.ToLower().Trim() == "image" || mediatype.ToLower().Trim() == "text/image")
            {
                if (ImagePath != "")
                {
                    try
                    {
                        if (ImagePath.Contains("\\"))
                        {
                            int ImagePathLastIdx = ImagePath.LastIndexOf("\\");
                            ImagePathLastIdx = ImagePathLastIdx + 1;
                            Imagename = ImagePath.Substring(ImagePathLastIdx, ImagePath.Length - ImagePathLastIdx);
                        }
                        else
                            Imagename = ImagePath;

                    }
                    catch { Imagename = ImagePath; }

                    try
                    {
                        ImageFullPath = webRootPath + Imagename;
                        byte[] imagebytes = System.IO.File.ReadAllBytes(ImageFullPath);
                        var base64 = Convert.ToBase64String(imagebytes);
                        base64imgSrc = string.Format("data:image/jpeg;base64,{0}", base64);
                    }
                    catch { base64imgSrc = ""; }
                }
            }
            return base64imgSrc;
        }

        public async Task<int> TeacherTrainingAssessmentQuestionsSave(TrainingScheduleTeacherAssessmentDto request)
        {
            var ReturnVal = 0;
            int Schedule_Teacher_Assessment_Id = 0;
            int Teacher_Attempted_Question_Id = 0;
            int Attempted_Question_Option_Id = 0;
            bool Is_AssessmentInserted = true;
            bool Is_QuestionInserted = true;
            bool Is_OptionInserted = true;

            var procedureName = "TrainingScheduleTeacherAssessmentSave";
            var parameters = new DynamicParameters();
            parameters.Add("@Teacher_Training_Schedule_Test_Id", request.Teacher_Training_Schedule_Test_Id, DbType.Int32);
            parameters.Add("@Training_Schedule_Teacher_Id", request.Training_Schedule_Teacher_Id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed) { connection.Open(); }

                using (var trans = connection.BeginTransaction())
                {
                    Schedule_Teacher_Assessment_Id = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: trans, commandType: CommandType.StoredProcedure);
                    ReturnVal = Schedule_Teacher_Assessment_Id;

                    if (Schedule_Teacher_Assessment_Id > 0)
                    {
                        foreach (var item in request.Questions)
                        {
                            procedureName = "TeacherAssessmentQuestionAttemptedSave";
                            parameters = new DynamicParameters();
                            parameters.Add("@Schedule_Teacher_Assessment_Id", Schedule_Teacher_Assessment_Id, DbType.Int32);
                            parameters.Add("@Question_Id", item.Question_Id, DbType.Int32);
                            Teacher_Attempted_Question_Id = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: trans, commandType: CommandType.StoredProcedure);

                            if (Teacher_Attempted_Question_Id == 0)
                            {
                                Is_QuestionInserted = false;
                            }
                            foreach (var option in item.Options)
                            {
                                procedureName = "TeacherAssessmentQuestionAnswerAttemptedSave";
                                parameters = new DynamicParameters();
                                parameters.Add("@Teacher_Attempted_Question_Id", Teacher_Attempted_Question_Id, DbType.Int32);
                                parameters.Add("@Question_Option_Id", option.Question_Option_Id, DbType.Int32);

                                Attempted_Question_Option_Id = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: trans, commandType: CommandType.StoredProcedure);

                                if (Attempted_Question_Option_Id == 0)
                                    Is_OptionInserted = false;
                            }
                        }
                    }
                    else
                    {
                        Is_AssessmentInserted = false;
                    }
                    if (Is_AssessmentInserted == false)
                    {
                        if (ReturnVal == 0)
                            ReturnVal = -2;
                    }
                    if (Is_QuestionInserted == false)
                        ReturnVal = -3;

                    if (Is_OptionInserted == false)
                        ReturnVal = -4;
                    try
                    {
                        if (Is_AssessmentInserted == true & Is_QuestionInserted == true & Is_OptionInserted == true)
                            trans.Commit();
                        else
                            trans.Rollback();
                    }
                    catch (Exception ex) { }
                }
                if (connection.State == ConnectionState.Open) { connection.Close(); }
            }
            return ReturnVal;
        }

        public async Task<int> UpdateLatestTeacherAttendance(UpdateTeacherTrainingAttendanceDto request)
        {
            int ReturnVal = 0;

            var procedureName = "UpdateLatestTrainingScheduleTeacherAttendance";
            string TrainingScheduleTeacherIds = "";
            string Attendance = "";

            foreach (var item in request.TeacherSelection)
            {
                TrainingScheduleTeacherIds += item.Training_Schedule_Teacher_Id + ",";
                Attendance += item.Is_Present + ",";
            }
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TrainingScheduleAttendanceId", request.Training_Schedule_Attendance_Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@BlockAdminUserId", request.Block_Admin_User_Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@TrainingScheduleTeacherIds", TrainingScheduleTeacherIds, DbType.String, ParameterDirection.Input);
                parameters.Add("@Attendance", Attendance, DbType.String, ParameterDirection.Input);

                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                
                return ReturnVal;
            }
        }
    }
}
