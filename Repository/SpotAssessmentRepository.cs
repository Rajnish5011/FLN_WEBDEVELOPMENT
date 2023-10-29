using Dapper;
using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using System;
using System.Data;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Entities;
using System.Collections.Generic;
using System.Linq;
using Azure;
using ASPNetCoreFLN_APIs.Dto.SpotAssessment;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class SpotAssessmentRepository : ISpotAssessmentRepository
    {
        private readonly DapperContext _context;

        public SpotAssessmentRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<int> CreateMentorStudentSpotAssessment(SpotAssessmentDto request)
        {
            var Student_SpotAssessment_Id = 0;
            var ReturnVal = 0;
            var Is_Spot_Inserted = true;

            var procedureName = "MentorStudentSpotAssessmentSave";
            var parameters = new DynamicParameters();
            parameters.Add("Mentor_School_Visit_Id", request.Mentor_School_Visit_Id, DbType.Int32);
            parameters.Add("Student_Id", request.Student_Id, DbType.Int32);
            parameters.Add("Is_ORF_Required", request.Is_ORF_Required, DbType.Boolean);
            parameters.Add("Is_Spot_Required", request.Is_Spot_Required, DbType.Boolean);
            parameters.Add("ORF_Question_Id", request.ORF_Question_Id, DbType.Int32);
            parameters.Add("Word_Read_Per_Minute", request.Word_Read_Per_Minute, DbType.String);
            parameters.Add("Created_By", request.Mentor_Id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    Student_SpotAssessment_Id = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                    if (Student_SpotAssessment_Id > 0)
                        transaction.Commit();

                    else
                        transaction.Rollback();
                }
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            if (Student_SpotAssessment_Id > 0)
            {
                if (request.Is_Spot_Required)
                {
                    using (var connection = _context.CreateConnection())
                    {
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        using (var Spottransaction = connection.BeginTransaction())
                        {
                            foreach (var item in request.QuestionAnswers)
                            {
                                procedureName = "CreateStudentSpotAssessmentAttempted";
                                parameters = new DynamicParameters();
                                parameters.Add("Student_SpotAssessment_Id", Student_SpotAssessment_Id, DbType.Int32);
                                parameters.Add("Question_Group_Instruction_Id", item.Question_Group_Instruction_Id, DbType.Int32);
                                parameters.Add("Question_Id", item.Question_Id, DbType.Int32);
                                parameters.Add("Question_Option_Id", item.Question_Option_Id, DbType.Int32);
                                parameters.Add("Created_By", request.Mentor_Id, DbType.Int32);
                               
                                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: Spottransaction, commandType: CommandType.StoredProcedure);

                                if (ReturnVal == 0)
                                    Is_Spot_Inserted = false;
                                else                                
                                    ReturnVal = 1;
                            }
                            try
                            {
                                if (Is_Spot_Inserted)
                                    Spottransaction.Commit();
                                else
                                    Spottransaction.Rollback();

                                if (connection.State == ConnectionState.Open)
                                    connection.Close();
                            }
                            catch { }
                        }
                    }                    
                }
                else
                    ReturnVal = 1;
            }
            else
                ReturnVal = Student_SpotAssessment_Id;

            return ReturnVal;
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
        public async Task<IEnumerable<SpotQuestions>> GetSpotAssessmentQuestions(byte ClassId, byte SubjectId)
        {
            var procedureName = "GetSpotAssessmentQuestionsByClassSubjectId";
            var parameters = new DynamicParameters();
            parameters.Add("ClassId", ClassId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("SubjectId", SubjectId, DbType.Int16, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<SpotQuestions>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<SpotQuestions>> GetSpotAssessmentQuestionsByCompetency(int CompetencyId)
        {
            var procedureName = "GetSpotAssessmentQuestionsByCompetencyId";
            var parameters = new DynamicParameters();
            parameters.Add("@CompetencyId", CompetencyId, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<SpotQuestions>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<SpotSubjectCompetancy>> GetSpotCompetancyTillDateByClassSubjectId(byte ClassId, byte SubjectId)
        {
            var procedureName = "GetSpotSubjectCompetancyTillDateByClassSubjectId";
            var parameters = new DynamicParameters();

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
