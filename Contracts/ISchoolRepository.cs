using ASPNetCoreFLN_APIs.Dto;
using ASPNetCoreFLN_APIs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface ISchoolRepository
    {
        public Task<IEnumerable<School>> GetSchoolListByClusterSchoolUdiseCode(string ClusterUdiseCode);

        public Task<IEnumerable<School>> GetSchoolByMentorId(int MentorId);

        public Task<IEnumerable<ClusterSchoolList>> GetClusterSchoolListByBlockId(short BlockId);

        public Task<IEnumerable<SchoolList>> GetSchoolListByClusterSchoolId(int ClusterSchoolId);

        public Task<int> TeacherToClassSectionAllocationSave(AllocateTeacherToClassSectionSave request);

        public Task<int> TeacherToClassSectionAllocationUpdate(int TeacherClassSectionId, AllocateTeacherToClassSectionUpdate request);

        public Task<int> DeleteTeacherClassSection(int TeacherClassSectionId);
        public Task<IEnumerable<SchoolListByMentorSchoolCode>> SearchSchoolforStateLevelMonitorByMentorIdSchoolCode(int MentorId, string SchoolCode);
    }
    }
