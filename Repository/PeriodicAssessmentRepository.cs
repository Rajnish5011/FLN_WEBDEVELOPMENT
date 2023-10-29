using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using ASPNetCoreFLN_APIs.Dto.PeriodicAssessment;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class PeriodicAssessmentRepository : IPeriodicAssessmentRepository
    {
        private readonly DapperContext _context;
        public PeriodicAssessmentRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PeriodicAssessmentSchedule>> GetPeriodicAssessmentSchedule()
        {
            var query = "GetLastPeriordicAssessmentSchedule";
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<PeriodicAssessmentSchedule>(query);
                return data.ToList();
            }
        }

        public string GetBase64StringImageByPath(string mediatype, string ImagePath)
        {
            var base64imgSrc = "";
            if (mediatype.ToLower().Trim() == "image" || mediatype.ToLower().Trim() == "text/image")
            {
                if (ImagePath != "")
                {
                    try
                    {
                        byte[] imagebytes = System.IO.File.ReadAllBytes(ImagePath);
                        var base64 = Convert.ToBase64String(imagebytes);
                        base64imgSrc = string.Format("data:image/jpeg;base64,{0}", base64);
                    }
                    catch { base64imgSrc = ""; }
                }
            }
            return base64imgSrc;
        }
        public async Task<int> CreatePeriodicAssessmentSchedule(PeriodicAssessmentScheduleDto request)
        {
            var AssessmentScheduleId = 0;
            var ReturnVal = 0;
            var procedureName = "CreatePeriodicAssessmentScheduleSave";
            var parameters = new DynamicParameters();
            parameters.Add("@Periodic_Assessment_Id", request.Periodic_Assessment_Id, DbType.Byte);
            parameters.Add("@Class_Id", request.Class_Id, DbType.Byte);
            parameters.Add("@Subject_Id", request.Subject_Id, DbType.Byte);
            parameters.Add("@Total_Number_Of_Questions", request.Total_Number_Of_Questions, DbType.Byte);
            parameters.Add("@Start_Date", request.Start_Date, DbType.Date);
            parameters.Add("@End_Date", request.End_Date, DbType.Date);
            parameters.Add("@Created_By", request.Created_By, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var transactionStart = connection.BeginTransaction())
                {
                    AssessmentScheduleId = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: transactionStart, commandType: CommandType.StoredProcedure);

                    if (AssessmentScheduleId > 0)
                    {
                        foreach (var competency in request.ScheduleCompetancy)
                        {
                            procedureName = "PeriodicAssessmentScheduleCompetancySave";
                            parameters = new DynamicParameters();
                            parameters.Add("@Periodic_Assessment_Schedule_Id", AssessmentScheduleId, DbType.Int32);
                            parameters.Add("@Competancy_Id", competency.Competancy_Id, DbType.Int16);
                            parameters.Add("@Number_Of_Question", competency.Number_Of_Question, DbType.Int32);

                            ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: transactionStart, commandType: CommandType.StoredProcedure);

                            if (ReturnVal <= 0)
                            {
                                transactionStart.Rollback();
                                return ReturnVal;
                            }
                        }

                        if (request.ScheduleORFQuestion.Count()>0)
                        {
                            foreach (var orfQuestion in request.ScheduleORFQuestion)
                            {
                                if (!string.IsNullOrWhiteSpace(orfQuestion.ORF_Question_Text))
                                {
                                    procedureName = "PeriodicAssessmentScheduleORFQuestionSave";
                                    parameters = new DynamicParameters();
                                    parameters.Add("@Periodic_Assessment_Schedule_Id", AssessmentScheduleId, DbType.Int32);
                                    parameters.Add("@ORF_Question_Text", orfQuestion.ORF_Question_Text, DbType.String);
                                    parameters.Add("@Min_Word_Read_Per_Minute", orfQuestion.Min_Word_Read_Per_Minute, DbType.Int32);
                                    parameters.Add("@Max_Seconds_To_Read", orfQuestion.Max_Seconds_To_Read, DbType.String);
                                    ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: transactionStart, commandType: CommandType.StoredProcedure);

                                    if (ReturnVal <= 0)
                                    {
                                        transactionStart.Rollback();
                                        return ReturnVal;
                                    }
                                }
                            }
                        }

                        transactionStart.Commit();
                    }
                    else
                    {
                        transactionStart.Rollback();
                    }
                }
            }
            return ReturnVal;
        }

        public async Task<int> UpdatePeriodicAssessmentSchedule(int PeriodicAssessmentScheduleId, UpdatePeriodicAssessmentScheduleDto request)
        {
            var ReturnVal = 0;
            var procedureName = "UpdateStudentPeriodicAssessmentSchedule";

            var parameters = new DynamicParameters();
            parameters.Add("@Periodic_Assessment_Schedule_Id", PeriodicAssessmentScheduleId, DbType.Int32);            
            parameters.Add("@Periodic_Assessment_Id", request.Periodic_Assessment_Id, DbType.Byte);
            parameters.Add("@Class_Id", request.Class_Id, DbType.Byte);
            parameters.Add("@Subject_Id", request.Subject_Id, DbType.Byte);
            parameters.Add("@Number_Of_Questions", request.Number_Of_Questions, DbType.Byte);
            parameters.Add("@Start_Date", request.Start_Date, DbType.Date);
            parameters.Add("@End_Date", request.End_Date, DbType.Date);
            parameters.Add("@Modified_By", request.Modified_By, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var transactionStart = connection.BeginTransaction())
                {
                    ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: transactionStart, commandType: CommandType.StoredProcedure);

                    if (ReturnVal > 0)
                    {
                        transactionStart.Commit();

                        foreach (var item in request.ScheduleCompetancy)
                        {
                            ReturnVal = 0;
                            procedureName = "UpdateStudentPeriodicAssessmentScheduleCompetancy";
                            parameters = new DynamicParameters();                            
                            parameters.Add("@Periodic_Assessment_Schedule_Competancy_Id", item.Periodic_Assessment_Schedule_Competancy_Id, DbType.Int32);
                            parameters.Add("@Periodic_Assessment_Schedule_Id", PeriodicAssessmentScheduleId, DbType.Int32);
                            parameters.Add("@Competancy_Id", item.Competancy_Id, DbType.Int16);
                            parameters.Add("@Number_Of_Question", item.Number_Of_Question, DbType.Int32);

                            using (var CompetancyTransaction = connection.BeginTransaction())
                            {
                                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: CompetancyTransaction, commandType: CommandType.StoredProcedure);
                                if (ReturnVal > 0)
                                    CompetancyTransaction.Commit();

                                if (ReturnVal == 0)
                                {
                                    transactionStart.Rollback();
                                    CompetancyTransaction.Rollback();
                                }
                            }
                        }
                    }
                }
            }
            return ReturnVal;
        }

        public async Task<int> StudentPeriodicAssessmentSave(StudentPeriodicAssessmentSave request)
        {
            var ReturnId = 0;
            var procedureName = "StudentPeriodicAssessmentStartSave";

            var parameters = new DynamicParameters();
            parameters.Add("@Periodic_Assessment_Schedule_Id", request.Periodic_Assessment_Schedule_Id, DbType.Int32);
            parameters.Add("@School_Id", request.School_Id, DbType.Int32);
            parameters.Add("@Student_Id", request.Student_Id, DbType.Int32);
            parameters.Add("@Teacher_Id", request.Teacher_Id, DbType.Int32);
            parameters.Add("@Class_Id", request.Class_Id, DbType.Byte);
            parameters.Add("@Section_Id", request.Section_Id, DbType.Byte);
            parameters.Add("@Subject_Id", request.Subject_Id, DbType.Byte);
            parameters.Add("@Is_Online_Exam", request.Is_Online_Exam, DbType.Boolean);
            parameters.Add("@ORF_Question_Id", request.ORF_Question_Id, DbType.Int32);
            parameters.Add("@Word_Read_Per_Minute", request.Word_Read_Per_Minute, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                using (var transactionStart = connection.BeginTransaction())
                {
                    ReturnId = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: transactionStart, commandType: CommandType.StoredProcedure);

                    if (ReturnId > 0)
                    {
                        transactionStart.Commit();

                        foreach (var item in request.QuestionAnswers)
                        {
                            var ReturnVal = 0;
                            procedureName = "StudentPeriodicAssessmentAttemptSave";
                            parameters = new DynamicParameters();
                            parameters.Add("@Periodic_Assessment_Start_Id", ReturnId, DbType.Int32);
                            parameters.Add("@Question_Id", item.Question_Id, DbType.Int32);
                            parameters.Add("@Question_Option_Id", item.Question_Option_Id, DbType.Int32);

                            using (var QuestionTransaction = connection.BeginTransaction())
                            {
                                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: QuestionTransaction, commandType: CommandType.StoredProcedure);
                                if (ReturnVal > 0)
                                    QuestionTransaction.Commit();

                                if (ReturnVal == 0)
                                {
                                    transactionStart.Rollback();
                                    QuestionTransaction.Rollback();
                                }
                            }
                        }
                        procedureName = "StudentPeriodicAssessmentStatusDone";
                        parameters = new DynamicParameters();
                        parameters.Add("@Periodic_Assessment_Start_Id", ReturnId, DbType.Int32);
                        ReturnId = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: transactionStart, commandType: CommandType.StoredProcedure);
                    }
                    else
                        transactionStart.Rollback();
                }
            }
            return ReturnId;
        }
        public async Task<int> DeletePeriodicAssessmentSchedule(int PeriodicAssessmentScheduleId)
        {
            var ReturnVal = 0;
            var procedureName = "DeleteStudentPeriodicAssessmentSchedule";

            var parameters = new DynamicParameters();
            parameters.Add("@Periodic_Assessment_Schedule_Id", PeriodicAssessmentScheduleId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);

            }
            return ReturnVal;
        }
       
        public async Task<IEnumerable<PeriodicQuestions>> GetPeriodicAssessmentQuestions(byte PeriodicAssessmentId, byte ClassId)
        {
            var procedureName = "GetPeriodicAssessmentByAssessmentTypeClassId";
            var parameters = new DynamicParameters();
            parameters.Add("@AssessmentTypeId", PeriodicAssessmentId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("@ClassId", ClassId, DbType.Byte, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<PeriodicQuestions>(procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<PeriodicQuestions>> GetPeriodicAssessmentQuestionsByPeriodicAssessmentId(byte PeriodicAssessmentId)
        {
            var procedureName = "GetPeriodicAssessmentQuestionsByCompetencyId";
            var parameters = new DynamicParameters();
            parameters.Add("@AssessmentTypeId", PeriodicAssessmentId, DbType.Int16, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<PeriodicQuestions>(procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<PeriodicQuestions>> GetPeriodicQuestionByAssessmentClassSubjectCompetencyId(int PeriodicAssessmentId, int ClassId, int SubjectId, Int16 CompetencyId)
        {
            var procedureName = "GetPeriodicQuestionByAssessmentClassSubjectCompetencyId";
            var parameters = new DynamicParameters();
            parameters.Add("@PeriodicAssessmentId", PeriodicAssessmentId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@ClassId", ClassId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@SubjectId", SubjectId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@CompetencyId", CompetencyId, DbType.Int16, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<PeriodicQuestions>(procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<PeriodicQuestionsByShedule>> GetPeriodicAssessmentQuestionsByPeriodicAssessmentScheduleId(int PeriodicAssessmentScheduleId)
        {
            var procedureName = "GetPeriodicAssessmentQuestionsByPeriodicAssessmentScheduleId";
            var parameters = new DynamicParameters();
            parameters.Add("@PeriodicAssessmentScheduleId", PeriodicAssessmentScheduleId, DbType.Int64, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<PeriodicQuestionsByShedule>(procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
    }
}
