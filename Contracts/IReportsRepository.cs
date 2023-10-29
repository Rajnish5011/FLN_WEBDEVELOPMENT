using ASPNetCoreFLN_APIs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface IReportsRepository
    {
        public Task<IEnumerable<VisitComplianceReport>> GetVisitComplianceReport(short DistrictId, short BlockId, int ClusterSchoolId, int SchoolId);

        public Task<IEnumerable<VisitComplianceReportDistrictWise>> GetVisitComplianceReportDistrictWise(DateTime GetMonthYear);

        public Task<IEnumerable<ClassroomObservationReport>> GetClassroomObservationReport(short DistrictId, short BlockId, int ClusterSchoolId, int SchoolId);

        public Task<IEnumerable<VisitSummaryReportMentorWise>> GetMentorWiseVisitSummary(DateTime GetMonthYear);

        public Task<IEnumerable<VisitSummaryReportByMentorWiseWithMonth>> GetMentorWiseSchoolVisitSummaryByMonth(DateTime GetMonthYear);
        
        public Task<IEnumerable<ClassroomObservationReport_Hindi>> GetClassroomObservationReportHindi(DateTime GetMonthYear);

        public Task<IEnumerable<ClassroomObservationReport_Math>> GetClassroomObservationReportMath(DateTime GetMonthYear);

        public Task<IEnumerable<ClassroomObservationSchoolWiseReport_Hindi>> GetClassroomObservationSchoolWiseReportHindi(DateTime GetMonthYear);

        public Task<IEnumerable<ClassroomObservationSchoolWiseReport_Math>> GetClassroomObservationSchoolWiseReportMath(DateTime GetMonthYear);
                
        public Task<IEnumerable<ClassroomObservationPercentageReport>> GetClassroomObservationPercentageReport(DateTime GetMonthYear,short DistrictId, short BlockId);

        public Task<IEnumerable<SchoolVisitPercentageReport>> GetSchoolVisitPercentageReport(DateTime GetMonthYear, short DistrictId, short BlockId);

        public Task<IEnumerable<SchoolVisitPercentageReportByFilter>> GetSchoolVisitPercentageReportByFilter(DateTime GetMonthYearFromDate, short DistrictId, short BlockId);

        public Task<IEnumerable<ClassroomObservationPercentageReportByFilter>> GetClassroomObservationPercentageReportByFilter(DateTime GetMonthYearFromDate, short DistrictId, short BlockId);
    }
}
