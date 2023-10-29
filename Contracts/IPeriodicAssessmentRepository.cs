using ASPNetCoreFLN_APIs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Dto.PeriodicAssessment;

namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface IPeriodicAssessmentRepository
    {
        public Task<IEnumerable<PeriodicAssessmentSchedule>> GetPeriodicAssessmentSchedule();
                
        public Task<IEnumerable<PeriodicQuestions>> GetPeriodicAssessmentQuestions(byte AssessmentTypeId, byte ClassId);

        public string GetBase64StringImageByPath(string mediatype, string ImagePath);

        public Task<int> CreatePeriodicAssessmentSchedule(PeriodicAssessmentScheduleDto PeriodicAssessmentSchedule);

        public Task<int> StudentPeriodicAssessmentSave(StudentPeriodicAssessmentSave PeriodicAssessmentStart);

        public Task<int> DeletePeriodicAssessmentSchedule(int PeriodicAssessmentScheduleId);

        public Task<int> UpdatePeriodicAssessmentSchedule(int PeriodicAssessmentScheduleId, UpdatePeriodicAssessmentScheduleDto request);
        public Task<IEnumerable<PeriodicQuestions>> GetPeriodicAssessmentQuestionsByPeriodicAssessmentId(byte PeriodicAssessmentId);
        public Task<IEnumerable<PeriodicQuestionsByShedule>> GetPeriodicAssessmentQuestionsByPeriodicAssessmentScheduleId(int PeriodicAssessmentScheduleId);
        public Task<IEnumerable<PeriodicQuestions>> GetPeriodicQuestionByAssessmentClassSubjectCompetencyId(int PeriodicAssessmentId,int ClassId,int SubjectId,Int16 CompetencyId);
    }
}
