using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Dto;
using ASPNetCoreFLN_APIs.Dto.Mentor;
using ASPNetCoreFLN_APIs.Entities;


namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface IMentorSchoolScheduleVisitRepository
	{
        public Task<int> CreateMentorSchoolScheduleVisit(MentorSchoolScheduleVisitDto mentorSchoolSchedule);
        public Task<int> MentorSchoolClassVisitEnd(int MentorSchoolVisitId);
        public Task<int> UpdatePresentStudentCount(int MentorSchoolVisitId,UpdatePresentStudentCountDto request);
        public Task<IEnumerable<MentorPastVisitBySchoolSheduleId>> GetMentorPastVisitByMentorSchoolScheduleId(int MentorSchoolScheduleId);       
        public Task<IEnumerable<MentorPastVisit>> GetMentorPastVisitBySchoolId(int SchoolId, int currentPageNumber, int pageSize);
        //public Task<IEnumerable<SpotDoneStudentMasteryStatus>> GetStudentMasteryStatusByMentorSchoolVisitId(int MentorSchoolVisitId);
        public Task<IEnumerable<MentorPastVisitByMentorID>>GetMentorPastVisitByMentorId(int MentorId, int currentPageNumber, int pageSize);
    }
}
