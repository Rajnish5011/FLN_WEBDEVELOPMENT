using ASPNetCoreFLN_APIs.Dto;
using ASPNetCoreFLN_APIs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface IStudentRepository
    {        
        public Task<IEnumerable<StudentByClusterUdiseCode>> GetStudentByClusterUDISECode(string ClusterUDISECode, int currentPageNumber, int pageSize);

        public Task<IEnumerable<Student>> GetStudentData(int SchoolId, byte ClassId, byte SectionId);

        public Task<IEnumerable<TotalNoOfStudent>> GetStudentCountByMentorSchoolVisitId(int MentorSchoolVisitId);
        public Task<IEnumerable<StudentPeriodicStatus>> GetStudentListByPeriodicStatus(int Id, int SchoolId, byte ClassId, byte SectionId);

        public Task<IEnumerable<Student>> GetStudentListSpotAssessmentWiseBySchoolClassSectionId(int SchoolId, byte ClassId, byte SectionId);

        public Task<IEnumerable<Student>> GetStudentListSpotAssessmentWiseByMentorSchoolVisitId(int MentorSchoolVisitId);

        public Task<IEnumerable<SpotDoneStudentMasteryStatus>> GetStudentMasteryStatusByMentorSchoolVisitId(int MentorSchoolVisitId);
    }
}
