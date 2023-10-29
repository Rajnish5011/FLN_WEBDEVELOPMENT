using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Dto.SpotAssessment;
using ASPNetCoreFLN_APIs.Entities;


namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface ISpotAssessmentRepository
    {
        public Task<int> CreateMentorStudentSpotAssessment(SpotAssessmentDto model);
        public Task<IEnumerable<SpotQuestions>> GetSpotAssessmentQuestions(byte ClassId, byte SubjectId);        
        public Task<IEnumerable<SpotQuestions>> GetSpotAssessmentQuestionsByCompetency(int CompetencyId);
        public string GetBase64StringImageByPath(string mediatype, string webRootPath, string ImagePath);
        public Task<IEnumerable<SpotSubjectCompetancy>> GetSpotCompetancyTillDateByClassSubjectId(byte ClassId, byte SubjectId);
    }
}