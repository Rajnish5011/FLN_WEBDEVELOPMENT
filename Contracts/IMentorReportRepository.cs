using ASPNetCoreFLN_APIs.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface IMentorReportRepository
    {
        Task<MentorReportsDto> GetMentorMonthlyReports(DateTime month_Year,string monitor_Id);
    }
}
