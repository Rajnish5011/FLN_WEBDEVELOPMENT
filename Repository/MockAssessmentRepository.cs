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
    public class MockAssessmentRepository : IMockAssessmentRepository
    {
        private readonly DapperContext _context;
        public MockAssessmentRepository(DapperContext context)
        {
            _context = context;
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
        public async Task<IEnumerable<MockQuestions>> GetMockAssesmentQuestionsByClassSubjectID(int ClassID, int SubjectID)
        {
            var procedureName = "GetMockAssesmentQuestionsByClassSubjectID";
            var parameters = new DynamicParameters();
            parameters.Add("ClassID", ClassID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("SubjectID", SubjectID, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<MockQuestions>(procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }

        public async Task<int> StudentMockAssessmentQuestionSave(StudentMockAssessmentSave request)
        {
            var procedureName = "StudentMockAssessmentQuestionSave";
            var ReturnId = 0;
            bool IsInserted = true;
            var parameters = new DynamicParameters();
            parameters.Add("Student_Id", request.Student_Id, DbType.Int32);
            parameters.Add("Class_Id", request.Class_Id, DbType.Int32);
            parameters.Add("Section_Id", request.Section_Id, DbType.Int32);
            parameters.Add("Subject_Id", request.Subject_Id, DbType.Int32);
            parameters.Add("ORF_Question_Id", request.ORF_Question_Id, DbType.Int32);
            parameters.Add("Word_Read_Per_Minute", request.Word_Read_Per_Minute, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (var transactionStart = connection.BeginTransaction())
                {
                    ReturnId = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: transactionStart, commandType: CommandType.StoredProcedure);

                    if (ReturnId > 0)
                    {

                        foreach (var item in request.StudentMockQuestionAnswers)
                        {
                            var ReturnVal = 0;
                            procedureName = "SaveStudentMockAssessmentQuestionAnswer";

                            parameters = new DynamicParameters();
                            parameters.Add("Parent_Student_MockPractice_Id", ReturnId, DbType.Int32);
                            parameters.Add("Question_Id", item.Question_Id, DbType.Int32);
                            parameters.Add("Option_Id", item.Option_Id, DbType.Int32);

                            ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: transactionStart, commandType: CommandType.StoredProcedure);
                            if (ReturnVal == 0)
                                IsInserted = false;
                        }
                        if (IsInserted)
                            transactionStart.Commit();
                        else
                            transactionStart.Rollback();
                    }
                    else
                        transactionStart.Rollback();
                }
            }
            return ReturnId;
        }
        public async Task<int> TeacherStudentMockAssessmentQuestionSave(TeacherStudentMockAssessmentSave request)
        {
            var procedureName = "TeacherStudentMockAssessmentQuestionSave";
            bool IsInserted = true;
            var ReturnId = 0;

            var parameters = new DynamicParameters();
            parameters.Add("Student_Id", request.Student_Id, DbType.Int32);
            parameters.Add("Teacher_Id", request.Teacher_Id, DbType.Int32);
            parameters.Add("Class_Id", request.Class_Id, DbType.Int32);
            parameters.Add("Section_Id", request.Section_Id, DbType.Int32);
            parameters.Add("Subject_Id", request.Subject_Id, DbType.Int32);
            parameters.Add("ORF_Question_Id", request.ORF_Question_Id, DbType.Int32);
            parameters.Add("Word_Read_Per_Minute", request.Word_Read_Per_Minute, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (var transactionStart = connection.BeginTransaction())
                {
                    ReturnId = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: transactionStart, commandType: CommandType.StoredProcedure);

                    if (ReturnId > 0)
                    {

                        foreach (var item in request.TeacherStudentMockQuestionAnswers)
                        {
                            var ReturnVal = 0;
                            procedureName = "SaveTeacherStudentMockAssessmentQuestionAnswer";

                            parameters = new DynamicParameters();
                            parameters.Add("Teacher_Student_MockPractice_Id", ReturnId, DbType.Int32);
                            parameters.Add("Question_Id", item.Question_Id, DbType.Int32);
                            parameters.Add("Option_Id", item.Option_Id, DbType.Int32);

                            ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: transactionStart, commandType: CommandType.StoredProcedure);
                            if (ReturnVal == 0)
                                IsInserted = false;
                        }
                        if (IsInserted)
                            transactionStart.Commit();
                        else
                            transactionStart.Rollback();
                    }
                    else
                        transactionStart.Rollback();
                }
            }
            return ReturnId;
        }
    }
}
