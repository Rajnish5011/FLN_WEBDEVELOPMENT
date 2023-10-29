using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Entities;
using Dapper;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly DapperContext _context;
        public ReportsRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<VisitComplianceReport>> GetVisitComplianceReport(short DistrictId, short BlockId, int ClusterSchoolId, int SchoolId)
        {
            var procedureName = "GetVisitComplianceReportByParameters";
            var parameters = new DynamicParameters();
            parameters.Add("DistrictId", DistrictId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("BlockId", BlockId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("ClusterSchoolId", ClusterSchoolId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("SchoolId", SchoolId, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<VisitComplianceReport>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<VisitComplianceReportDistrictWise>> GetVisitComplianceReportDistrictWise(DateTime GetMonthYear)
        {
            var procedureName = "GetVisitComplianceReportDistrictWise";
            var parameters = new DynamicParameters();
            parameters.Add("GetMonthYear", GetMonthYear, DbType.Date, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<VisitComplianceReportDistrictWise>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<VisitSummaryReportMentorWise>> GetMentorWiseVisitSummary(DateTime GetMonthYear)
        {
            var procedureName = "GetMentorWiseVisitSummary";
            var parameters = new DynamicParameters();
            parameters.Add("GetMonthYear", GetMonthYear, DbType.Date, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<VisitSummaryReportMentorWise>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<VisitSummaryReportByMentorWiseWithMonth>> GetMentorWiseSchoolVisitSummaryByMonth(DateTime GetMonthYear)
        {
            var procedureName = "GetMentorWiseSchoolVisitSummaryByMonth";
            var parameters = new DynamicParameters();
            parameters.Add("GetMonthYear", GetMonthYear, DbType.Date, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<VisitSummaryReportByMentorWiseWithMonth>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<ClassroomObservationReport_Hindi>> GetClassroomObservationReportHindi(DateTime GetMonthYear)
        {
            var procedureName = "GetClassroomObservationReport_Hindi";
            var parameters = new DynamicParameters();
            parameters.Add("GetMonthYearFromDate", GetMonthYear, DbType.Date, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<ClassroomObservationReport_Hindi>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }

        public async Task<IEnumerable<ClassroomObservationReport_Math>> GetClassroomObservationReportMath(DateTime GetMonthYear)
        {
            var procedureName = "GetClassroomObservationReport_Math";
            var parameters = new DynamicParameters();
            parameters.Add("GetMonthYearFromDate", GetMonthYear, DbType.Date, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<ClassroomObservationReport_Math>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<ClassroomObservationReport>> GetClassroomObservationReport(short DistrictId, short BlockId, int ClusterSchoolId, int SchoolId)
        {
            var procedureName = "GetClassroomObservationReportByParameters";
            var parameters = new DynamicParameters();
            parameters.Add("DistrictId", DistrictId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("BlockId", BlockId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("ClusterSchoolId", ClusterSchoolId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("SchoolId", SchoolId, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<ClassroomObservationReport>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<ClassroomObservationSchoolWiseReport_Hindi>> GetClassroomObservationSchoolWiseReportHindi(DateTime GetMonthYear)
        {
            var procedureName = "GetClassroomObservationSchoolWiseReport_Hindi";
            var parameters = new DynamicParameters();
            parameters.Add("GetMonthYearFromDate", GetMonthYear, DbType.Date, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<ClassroomObservationSchoolWiseReport_Hindi>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<ClassroomObservationSchoolWiseReport_Math>> GetClassroomObservationSchoolWiseReportMath(DateTime GetMonthYear)
        {
            var procedureName = "GetClassroomObservationSchoolWiseReport_Math";
            var parameters = new DynamicParameters();
            parameters.Add("GetMonthYearFromDate", GetMonthYear, DbType.Date, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<ClassroomObservationSchoolWiseReport_Math>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }

        public async Task<IEnumerable<ClassroomObservationPercentageReport>> GetClassroomObservationPercentageReport(DateTime GetMonthYearFromDate, short DistrictId, short BlockId)
        {
            var procedureName = "GetClassroomObservationPercentageReportByParameters";
            var parameters = new DynamicParameters();
            parameters.Add("GetMonthYearFromDate", GetMonthYearFromDate, DbType.Date, ParameterDirection.Input);
            parameters.Add("DistrictId", DistrictId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("BlockId", BlockId, DbType.Int16, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<ClassroomObservationPercentageReport>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<SchoolVisitPercentageReport>> GetSchoolVisitPercentageReport(DateTime GetMonthYearFromDate, short DistrictId, short BlockId)
        {
            var procedureName = "GetSchoolVisitPercentageReportByParameters";
            var parameters = new DynamicParameters();
            parameters.Add("GetMonthYearFromDate", GetMonthYearFromDate, DbType.Date, ParameterDirection.Input);
            parameters.Add("DistrictId", DistrictId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("BlockId", BlockId, DbType.Int16, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<SchoolVisitPercentageReport>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<SchoolVisitPercentageReportByFilter>> GetSchoolVisitPercentageReportByFilter(DateTime GetMonthYearFromDate, short DistrictId, short BlockId)
        {
            var procedureName = "GetSchoolVisitPercentageReportByFilter";
            var parameters = new DynamicParameters();
            parameters.Add("GetMonthYearFromDate", GetMonthYearFromDate, DbType.Date, ParameterDirection.Input);
            parameters.Add("DistrictId", DistrictId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("BlockId", BlockId, DbType.Int16, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<SchoolVisitPercentageReportByFilter>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<ClassroomObservationPercentageReportByFilter>> GetClassroomObservationPercentageReportByFilter(DateTime GetMonthYearFromDate, short DistrictId, short BlockId)
        {
            var procedureName = "GetClassroomObservationPercentageReportByFilter";
            var parameters = new DynamicParameters();
            parameters.Add("GetMonthYearFromDate", GetMonthYearFromDate, DbType.Date, ParameterDirection.Input);
            parameters.Add("DistrictId", DistrictId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("BlockId", BlockId, DbType.Int16, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<ClassroomObservationPercentageReportByFilter>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
    }
}
