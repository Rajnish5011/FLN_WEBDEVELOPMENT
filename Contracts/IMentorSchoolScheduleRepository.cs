using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Entities;
using ASPNetCoreFLN_APIs.Dto.Mentor;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetCoreFLN_APIs.Contracts

{
    public interface IMentorSchoolScheduleRepository
    {						
        public Task<IEnumerable<MentorSchoolSchedule>> GetMentorSchoolScheduleByMentorId(int MentorId);
                
        public Task<int> CreateMentorSchoolSchedule(CreateMentorSchoolScheduleDto request);

        public Task<int> UpdateMentorSchoolSchedule(int MentorSchoolScheduleId, UpdateMentorSchoolScheduleDto request);

        public Task<int> MentorSchoolScheduleVisitEnd(int MentorSchoolScheduleId);

        public Task<MentorProfile> GetMentorProfileByMentorID(int MentorID);

        public Task<MentorMonthlyTarget> MentorMonthlyTargetByMentorId(int MentorId, DateTime GetMonthFromDate);

    }
}