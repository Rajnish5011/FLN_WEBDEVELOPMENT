using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Dto.Mentor;
using ASPNetCoreFLN_APIs.Dto.ObservationQuestion;
using ASPNetCoreFLN_APIs.Entities;
using Microsoft.AspNetCore.Mvc;
using static ASPNetCoreFLN_APIs.Repository.MentorClassObervationRepository;

namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface IMentorClassObservationRepository
    {
        public Task<int> CreateMentorClassObervation(MentorClassObervationDto mentorSchoolSchedule);

        public Task<IEnumerable<ClassObservationByMentor>> GetClassObservationByMentorSchoolVisitId(int MentorSchoolVisitId);

        public Task<List<ObservationQuestionDto>> GetClassObservationQuestion();

        public Task<IEnumerable<ObservationQuestions>> GetClassObservationQuestions(int MentorId, byte SubjectId);

        public Task<IEnumerable<MentorClassTeacherObservationFeedBack>> GetMentorClassTeacherObservationFeedBack(int Mentor_Id, string SelectedOptionIDs);
    }
}