using ASPNetCoreFLN_APIs.Dto.PeriodicAssessment;
using ASPNetCoreFLN_APIs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Contracts
{
   public interface IMockAssessmentRepository
    {
        public string GetBase64StringImageByPath(string mediatype, string ImagePath);
        public Task<IEnumerable<MockQuestions>> GetMockAssesmentQuestionsByClassSubjectID(int ClassID, int SubjectID);
        public Task<int> StudentMockAssessmentQuestionSave(StudentMockAssessmentSave StudentMockAssessmentStart);
        public Task<int> TeacherStudentMockAssessmentQuestionSave(TeacherStudentMockAssessmentSave TeacherStudentMockAssessmentStart);
    }
}
