using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class MentorReportRepository : IMentorReportRepository
    {
        private readonly DapperContext _context;
        public MentorReportRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<MentorReportsDto> GetMentorMonthlyReports(DateTime month_Year, string monitor_Id)
        {
            var procedureName = "MonitorCalculations";
            var parameters = new DynamicParameters();
            MentorReportsDto reports = new MentorReportsDto();
            parameters.Add("@Monitor_Id", monitor_Id, DbType.String, ParameterDirection.Input);
            parameters.Add("@Month_Year", month_Year, DbType.DateTime, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var data = await connection.QueryAsync<MentorReportsDto>
                                     (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    reports.PercentageTotalVisitCompleted = data.Where(x => x.PercentageTotalVisitCompleted != 0).FirstOrDefault() == null ? 0
                    : (double)data.Where(x => x.PercentageTotalVisitCompleted != 0).FirstOrDefault().PercentageTotalVisitCompleted;

                    reports.PercentageTotalClassObservationsCompleted = data.Where(x => x.PercentageTotalClassObservationsCompleted != 0).FirstOrDefault() == null ? 0
                    : (double)data.Where(x => x.PercentageTotalClassObservationsCompleted != 0).FirstOrDefault().PercentageTotalClassObservationsCompleted;

                    reports.PercentageTotalMentorCompletedTargetedVisits = data.Where(x => x.PercentageTotalMentorCompletedTargetedVisits != 0).FirstOrDefault() == null ? 0
                    : (double)data.Where(x => x.PercentageTotalMentorCompletedTargetedVisits != 0).FirstOrDefault().PercentageTotalMentorCompletedTargetedVisits;

                    reports.PercentageTotalMentorCompletedTargetedObservations = data.Where(x => x.PercentageTotalMentorCompletedTargetedObservations != 0).FirstOrDefault() == null ? 0
                    : (double)data.Where(x => x.PercentageTotalMentorCompletedTargetedObservations != 0).FirstOrDefault().PercentageTotalMentorCompletedTargetedObservations;

                    return reports;
                }
                catch (Exception ex)
                {
                    return new MentorReportsDto();
                }
            }
        }

    }
}