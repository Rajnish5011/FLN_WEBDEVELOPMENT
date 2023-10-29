using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Entities;
using ASPNetCoreFLN_APIs.Dto.Mentor;

namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface ITeachersMeetingRepository
    {						
        public Task<IEnumerable<ClusterTeacherMeetingSchedule>> GetClusterTeacherMeetingScheduleByMentorId(int MentorId);

        public Task<int> CreateClusterTeacherMeetingSchedule(CreateClusterMeetingScheduleDto request);

        public Task<int> ClusterTeachersMeetingScheduleEnd(int ClusterMeetingScheduleId, int MentorId, string Remarks);
        public Task<int> UpdateClusterTeachersMeetingSchedule(int ClusterMeetingScheduleId, int MentorId, UpdateClusterMeetingScheduleDto request);
        public Task<int> DeleteClusterTeachersMeetingSchedule(int ClusterMeetingScheduleId, int MentorId);

    }
}