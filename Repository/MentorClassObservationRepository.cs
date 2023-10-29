using Dapper;
using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Entities;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ASPNetCoreFLN_APIs.Dto.Mentor;
using Newtonsoft.Json;
using System.IO;
using System.Text.Json;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using ASPNetCoreFLN_APIs.Dto.ObservationQuestion;
using Azure.Core;
using AutoMapper;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class MentorClassObervationRepository : IMentorClassObservationRepository
    {
        private readonly DapperContext _context;
        private readonly IMapper _mapper;

        [DataContract]
        public class QuestionAnswers
        {
            [DataMember]
            public string question { get; set; }

            [DataMember]
            public string answers { get; set; }

            [DataMember]
            public string Observation_Question_Id { get; set; }

            [DataMember]
            public string Observation_Question_Option_Id { get; set; }

        }
        public class TestData
        {
            public string GenerateSingleJsonObject()
            {
                const string car = "";
                //const string car = $$"""
                //{
                //    "name": "Charger",
                //    "make": "Dodge",
                //    "model": "RT",
                //    "year": 2019,
                //    "price": {
                //      "amount": 36100,
                //      "currency": "USD"
                //    } 
                //}
                //""";
                return car;
            }
        }
        public class JObjectManipulation
        {
            public string SingleJsonObject { get; set; }
            public JObjectManipulation()
            {
                InitializeData();
            }
            public void InitializeData()
            {
                var testData = new TestData();
                SingleJsonObject = testData.GenerateSingleJsonObject();
            }
        }
        public MentorClassObervationRepository(DapperContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateMentorClassObervation(MentorClassObervationDto request)
        {
            var ReturnVal = 0;
            var procedureName = "CreateMentorClassObervation";
            var parameters = new DynamicParameters();
            parameters.Add("Mentor_School_Visit_Id", request.Mentor_School_Visit_Id, DbType.Int32);
            parameters.Add("Observation_Feedback", request.Observation_Feedback, DbType.String);
            parameters.Add("Observation_Remark", request.Observation_Remark, DbType.String);
            parameters.Add("QuestionAnswer", request.QuestionAnswer, DbType.String);
            parameters.Add("Time_Taken", request.Time_Taken, DbType.String);
            parameters.Add("Created_By", request.Mentor_Id, DbType.Int32);
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    //ReturnVal = await connection.ExecuteScalarAsync<int>(procedureName, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                    ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);

                    if (ReturnVal > 0)
                        transaction.Commit();
                    else
                        transaction.Rollback();

                }
            }
            return ReturnVal;
        }
        public async Task<IEnumerable<ClassObservationByMentor>> GetClassObservationByMentorSchoolVisitId(int MentorSchoolVisitId)
        {
            var procedureName = "GetClassTeacherObservationByMentorSchoolVisitId";
            var parameters = new DynamicParameters();
            parameters.Add("MentorSchoolVisitId", MentorSchoolVisitId, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<ClassObservationByMentor>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }

        public async Task<List<ObservationQuestionDto>> GetClassObservationQuestion()
        {

            var procedureName = "GetClassObservationQuestion";
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    throw;
                }
                var data = await connection.QueryAsync<ObservationQuestionScopeMaster, ObservationQuestionMaster, ObservationQuestionOptions, ObservationQuestionScopeMaster>(
                    procedureName,
                    (scopeMaster, questionMaster, questionOptions) =>
                    {
                        if (scopeMaster.ObservationQuestionMasters == null)
                        {
                            scopeMaster.ObservationQuestionMasters = new List<ObservationQuestionMaster>();
                        }
                        if (questionMaster.ObservationQuestionOptions == null)
                        {
                            questionMaster.ObservationQuestionOptions = new List<ObservationQuestionOptions>();
                        }
                        questionMaster.Observation_Question_Id = Convert.ToInt16(questionOptions.Observation_Question_Id);
                        scopeMaster.ObservationQuestionMasters.Add(questionMaster);
                        if (questionOptions != null)
                        {
                            questionOptions.Observation_Question_Option_Id = Convert.ToInt32(questionMaster.Observation_Question_Option_Id);
                            questionMaster.ObservationQuestionOptions.Add(questionOptions);
                        }
                        return scopeMaster;
                    },
                    splitOn: "Question_Scope_Id, Observation_Question_Id", // Split based on shared properties
                    commandType: CommandType.StoredProcedure
                );
                List<ObservationQuestionScopeMaster> observationQuestionScopeMaster = new List<ObservationQuestionScopeMaster>();
                observationQuestionScopeMaster = data.GroupBy(scope => new
                {
                    scope.Question_Scope_Id,
                    scope.Question_Scope,
                })
              .Select(scopeGroup => new ObservationQuestionScopeMaster
              {
                  Question_Scope_Id = scopeGroup.Key.Question_Scope_Id,
                  Question_Scope = scopeGroup.Key.Question_Scope,

                  ObservationQuestionMasters = scopeGroup
                      .SelectMany(questionMaster => questionMaster.ObservationQuestionMasters)
                      .GroupBy(qm => qm.Observation_Question_Id) // Group ObservationQuestionMasters by Observation_Question_Id
                      .Select(qmGroup => new ObservationQuestionMaster
                      {
                          Observation_Question_Id = qmGroup.Key, // Use the Observation_Question_Id as the key
                          //Subject_Id = qmGroup.Select(qm => qm.Subject_Id).FirstOrDefault(),
                          Question_Number = qmGroup.Select(qm => qm.Question_Number).FirstOrDefault(),
                          Observation_Question = qmGroup.Select(qm => qm.Observation_Question).FirstOrDefault(),
                          Is_Multiple_Choice = qmGroup.Select(qm => qm.Is_Multiple_Choice).FirstOrDefault(),
                          Is_Multiple_Dependent_Option = qmGroup.Select(qm => qm.Is_Multiple_Dependent_Option).FirstOrDefault(),
                          Is_Open_Questions = qmGroup.Select(qm => qm.Is_Open_Questions).FirstOrDefault(),
                          Is_Dependent = qmGroup.Select(qm => qm.Is_Dependent).FirstOrDefault(),
                          Dependent_Question_ID = qmGroup.Select(qm => qm.Dependent_Question_ID).FirstOrDefault(),
                          Dependent_Question_Option_Id = qmGroup.Select(qm => qm.Dependent_Question_Option_Id).FirstOrDefault(),
                          Multiple_Dependent_Options = qmGroup.Select(qm => qm.Multiple_Dependent_Options).FirstOrDefault(),
                          //Is_Question_Have_Alert = qmGroup.Select(qm => qm.Is_Question_Have_Alert).FirstOrDefault(),
                          Response_Type = qmGroup.Select(qm => qm.Response_Type).FirstOrDefault(),
                          Type = qmGroup.Select(qm => qm.Type).FirstOrDefault(),
                          //Combine other properties as needed
                          //ObservationQuestionOptions = qmGroup
                          // .SelectMany(qop => qop.ObservationQuestionOptions).GroupBy(x=> x.Observation_Question_Id).Select(op => new ObservationQuestionOptions
                          // {
                               
                          // }).Select(qop => qop.Observation_Question_Id).ToList(),

                      }).ToList()
              })
              .ToList();

                List<ObservationQuestionDto> observationQuestionDto = new List<ObservationQuestionDto>();
                observationQuestionDto = _mapper.Map<List<ObservationQuestionDto>>(observationQuestionScopeMaster);
                return observationQuestionDto;
            }
        }
        public async Task<IEnumerable<ObservationQuestions>> GetClassObservationQuestions(int MentorId, byte SubjectId)
        {
            var procedureName = "GetClassObservationQuestion";
            var parameters = new DynamicParameters();
            //parameters.Add("MentorId", MentorId, DbType.Int32, ParameterDirection.Input);
            //parameters.Add("SubjectId", SubjectId, DbType.Byte, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<ObservationQuestions>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }

        public async Task<IEnumerable<MentorClassTeacherObservationFeedBack>> GetMentorClassTeacherObservationFeedBack(int Mentor_Id,string SelectedOptionIDs)
        {
            var procedureName = "GetObservationFeedbackByMentorSelectedOptions";
            var parameters = new DynamicParameters();
            parameters.Add("@Mentor_Id", Mentor_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@SelectedOptionIDs", SelectedOptionIDs, DbType.String, ParameterDirection.Input);            

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<MentorClassTeacherObservationFeedBack>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
    }
}
