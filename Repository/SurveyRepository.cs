using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto.StudentTeacherCommunication;
using Dapper;
using System.Data;
using System;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Entities;
using ASPNetCoreFLN_APIs.Dto.SurveyDTO;
using Newtonsoft.Json;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;
using ASPNetCoreFLN_APIs.Dto;
using System.Linq;
using static ASPNetCoreFLN_APIs.Dto.SurveyDTO.SurveyFormResponse;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly DapperContext _Context;
        public SurveyRepository(DapperContext Context)
        {
            _Context = Context;
        }

        public async Task<int> CreateSurveyForm(SurveyDto survey)
        {
            try
            {
                var procedureName = "SurveyForm_Insert";
                var parameters = new DynamicParameters();
                string roleIds = JsonConvert.SerializeObject(survey.Role_Id);
                parameters.Add("@ROLE_ID", roleIds, DbType.String, ParameterDirection.Input, size: int.MaxValue);
                string designationIds = JsonConvert.SerializeObject(survey.Designation_Id);
                parameters.Add("@DESGINATION_ID", designationIds, DbType.String, ParameterDirection.Input, size: int.MaxValue);
                string districtIds = JsonConvert.SerializeObject(survey.District_Id);
                parameters.Add("@DISTRICT_ID", districtIds, DbType.String, ParameterDirection.Input, size: int.MaxValue);
                parameters.Add("@SURVEY_TITLE", survey?.Survey_Title, DbType.String, ParameterDirection.Input);
                parameters.Add("@SURVEY_DESCRIPTION", survey?.Survey_Description, DbType.String, ParameterDirection.Input);
                parameters.Add("@SURVEY_START_DATE", survey.Survey_Start_Date, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@SURVEY_END_DATE", survey.Survey_End_Date, DbType.DateTime, ParameterDirection.Input);
                string surveyGroup = JsonConvert.SerializeObject(survey.Question_Groups);
                parameters.Add("@SURVEY_FORM", surveyGroup, DbType.String, ParameterDirection.Input, size: int.MaxValue);
                parameters.Add("@RETURNVALUE", dbType: DbType.Int32, direction: ParameterDirection.Output); // Add an output parameter
                using var connection = _Context.CreateConnection();
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
                var returnValue = parameters.Get<int>("@RETURNVALUE");
                return returnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SurveyForm>> GetAllSurveyForm(string mentor_Id)
        {
            try
            {
                var procedureName = "GetSurveyFrom";
                var parameters = new DynamicParameters();
                parameters.Add("@MENTOR_ID", mentor_Id, DbType.String, ParameterDirection.Input);
                List<SurveyForm> surveyList = new List<SurveyForm>();
                DateTime dateToCheck = DateTime.Now.Date;
                using (var connection = _Context.CreateConnection())
                {
                    var data = await connection.QueryAsync<Survey>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    var surveyData = data.Where(x => x.Survey_Start_Date.Date == dateToCheck || x.Survey_End_Date.Date >= dateToCheck).ToList();
                    if (surveyData.Any())
                    {
                        foreach (var item in surveyData)
                        {
                            var surveyDto = new SurveyForm
                            {
                                Survey_Id = item.Survey_Id,
                                Survey_Title = item.Survey_Title,
                                Survey_Description = item.Survey_Description,
                                Survey_Start_Date = item.Survey_Start_Date,
                                Survey_End_Date = item.Survey_End_Date
                            };
                            surveyList.Add(surveyDto);
                        }
                        return surveyList;
                    }
                    return new List<SurveyForm>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SurveyFormResponse>> GetSurveyForm(string survey_Id)
        {
            try
            {
                var procedureName = "GetSurveyDetail_ById";
                var parameters = new DynamicParameters();
                parameters.Add("@SURVEY_ID", survey_Id, DbType.String, ParameterDirection.Input);
                List<SurveyFormResponse> surveyList = new List<SurveyFormResponse>();
                using (var connection = _Context.CreateConnection())
                {
                    var data = await connection.QueryAsync<Survey>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    if(data.FirstOrDefault().Survey_Id!=0)
                    {
                        foreach (var item in data)
                        {
                            var surveyDto = new SurveyFormResponse
                            {
                                Survey_Id = item.Survey_Id,
                                Survey_Title = item.Survey_Title,
                                Survey_Description = item.Survey_Description,
                                Survey_Start_Date = item.Survey_Start_Date,
                                Survey_End_Date = item.Survey_End_Date,
                            };
                            surveyDto.Question_Groups = JsonConvert.DeserializeObject<List<SurveyQuestionGroup>>(item.Survey_Form);
                            surveyDto.District_Id = JsonConvert.DeserializeObject<List<string>>(item.District_Id);
                            surveyDto.Role_Id = JsonConvert.DeserializeObject<List<string>>(item.Role_Id);
                            surveyDto.Designation_Id = JsonConvert.DeserializeObject<List<string>>(item.Designation_Id);
                            surveyList.Add(surveyDto);
                        }
                        return surveyList;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SaveSurveyForm(SurveyAnswers answers)
        {
            try
            {
                var procedureName = "SaveSurvey_Insert";
                var parameters = new DynamicParameters();
                parameters.Add("@SURVEY_ID", answers.Survey_ID, DbType.String, ParameterDirection.Input);
                parameters.Add("@MENTOR_ID", answers.Mentor_ID, DbType.String, ParameterDirection.Input);
                parameters.Add("@ANSWER_GROUPS", answers.Answer_Groups, DbType.String, ParameterDirection.Input);
                parameters.Add("@RETURNVALUE", dbType: DbType.Int32, direction: ParameterDirection.Output); 
                using var connection = _Context.CreateConnection();
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
                var returnValue = parameters.Get<int>("@RETURNVALUE");
                return returnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

