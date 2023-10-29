using ASPNetCoreFLN_APIs.Dto;
using ASPNetCoreFLN_APIs.Dto.PeriodicAssessment;
using ASPNetCoreFLN_APIs.Entities;
using ASPNetCoreFLN_APIs.TeacherTrainingDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Contracts
{
   public interface IManageTargetsRepository
    {
        public Task<int>AddMonthlyTargetForMentorOrMonitor(MonthlyTarget request);
        public Task<IEnumerable<MonthlyTargetList>> GetMonthlyMentorsTarget(bool IsCurrentYear,int RoleId=0);
        public Task<int> UpdateMonthlyMentorsTarget(UpdateMonthlyTarget request);
    }
}
