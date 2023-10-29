using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Entities
{

    #region Dashboard Reports
    public class VisitComplianceReport
    {
        public string Total_School_Visit { get; }
        public string Total_School_Visit_LastMonth { get; }
        public string Total_Unique_School_Visit { get; }
        public string Total_Unique_School_Visit_LastMonth { get; }
        public string Total_Class_Observation { get; }
        public string Total_Class_Observation_LastMonth { get; }
        public string No_Of_Mentor_Completed_Visit { get; }
        public string No_Of_Mentor_Completed_Visit_LastMonth { get; }

    }
    public class ClassroomObservationReport
    {
        public string Total_Enrollment_By_Mentor { get; }
        public string Average_Attendance_By_Mentor { get; }
        public string Total_Enrollment_By_Mentor_LastMonth { get; }
        public string Average_Attendance_By_Mentor_LastMonth { get; }

    }

    public class VisitComplianceReportDistrictWise
    {
        public string District_Name { get; }
        public string Number_Of_Mentors { get; }
        public string Total_Classroom_Observations { get; }
        public string Total_Unique_School_Visit { get; }
        public string Total_School_Visit { get; }
        public string Total_Student_Spot_Tested { get; }
    }
    	
    public class VisitSummaryReportMentorWise
    {
        public string District_Name { get; set; }
        public string Block_Name { get; set; }
        public string Cluster_School_Name { get; set; }
        public string School_Name { get; set; }
        public string Unique_Code { get; set; }
        public string Visit_Date { get; set; }
        public int Total_Classroom_Observations { get; set; }
        //public int Total_Unique_School_Visit { get; set; }
        //public int Total_School_Visit { get; set; }
        public int Total_Student_Spot_Tested { get; }
    }
    public class VisitSummaryReportByMentorWiseWithMonth
    {
        public string District_Name { get; set; }
        public string Block_Name { get; set; }
        public string Cluster_School_Name { get; set; }
        public string Unique_Code { get; set; }
        public string Visit_Month { get; set; }
        public int Total_Classroom_Observations { get; set; }
        public int Total_Unique_School_Visit { get; set; }
        public int Total_School_Visit { get; set; }
        public int Total_Student_Spot_Tested { get; }
    }

    public class ClassroomObservationReport_Hindi
    {
        public string District_Name { get; set; }
        public string Q1_Option1 { get; set; }
        public string Q1_Option2 { get; set; }
        public string Q1_Option3 { get; set; }
        public string Q2_Average_Week { get; set; }
        public string Q3_Option1 { get; set; }
        public string Q3_Option2 { get; set; }
        public string Q3_Option3 { get; set; }
        public string Q4_Option1 { get; set; }
        public string Q4_Option2 { get; set; }
        public string Q4_Option3 { get; set; }
    }
    public class ClassroomObservationReport_Math
    {
        public string District_Name { get; set; }
        public string Q1_Option1 { get; set; }
        public string Q1_Option2 { get; set; }
        public string Q1_Option3 { get; set; }
        public string Q2_Option1 { get; set; }
        public string Q2_Option2 { get; set; }
        public string Q3_Option1 { get; set; }
        public string Q3_Option2 { get; set; }
        public string Q3_Option3 { get; set; }

    }

    public class ClassroomObservationSchoolWiseReport_Hindi 
    {
        public string District_Name { get; set; }                    
        public string Block_Name { get; set; }
        public string Cluster_School_Name { get; set; }
        public string School_Name { get; set; }
        public string UDISE_Code { get; set; }
        public string Q1_Option1 { get; set; }
        public string Q1_Option2 { get; set; }
        public string Q1_Option3 { get; set; }
        public string Q2_Average_Week { get; set; }
        public string Q3_Option1 { get; set; }
        public string Q3_Option2 { get; set; }
        public string Q3_Option3 { get; set; }
        public string Q4_Option1 { get; set; }
        public string Q4_Option2 { get; set; }
        public string Q4_Option3 { get; set; }
    }
    public class ClassroomObservationSchoolWiseReport_Math
    {
        public string District_Name { get; set; }
        public string Block_Name { get; set; }
        public string Cluster_School_Name { get; set; }
        public string School_Name { get; set; }
        public string UDISE_Code { get; set; }
        public string Q1_Option1 { get; set; }
        public string Q1_Option2 { get; set; }
        public string Q1_Option3 { get; set; }
        public string Q2_Option1 { get; set; }
        public string Q2_Option2 { get; set; }
        public string Q3_Option1 { get; set; }
        public string Q3_Option2 { get; set; }
        public string Q3_Option3 { get; set; }

    }
    public class ClassroomObservationPercentageReport
    {
        public string District_Name { get; set; }
        public string Block_Name { get; set; }        
        public string Mentor_Name { get; set; }
        public string Unique_Code { get; set; }
        public string Visit_Month { get; set; }
        public int Total_Classroom_Observations { get; set; }
        public int Target_Classroom_Observation { get; set; }
        public string Observation_Percentage { get; set; }
    }

    public class SchoolVisitPercentageReport
    {
        public string District_Name { get; set; }
        public string Block_Name { get; set; }
        public string Mentor_Name { get; set; }
        public string Unique_Code { get; set; }
        public string Visit_Month { get; set; }
        public int Target_Unique_School_Visit { get; set; }
        public int Total_School_Visit { get; set; }
        public int Unique_School_Visit { get; set; }
        public string Unique_School_Visit_Percentage { get; set; }

    }
    public class SchoolVisitPercentageReportByFilter
    {
        public string Name { get; set; }      
        public string Total_School { get; set; }
        public string Total_Unique_School_Visit { get; set; }
        public string Total_School_Visit { get; set; }
        public string Unique_School_Target_Completed_By_Mentor { get; set; }

    }
    public class ClassroomObservationPercentageReportByFilter
    {
        public string Name { get; set; }               
        public int Observation_Mentor_Number { get; set; }
        public int Total_Classroom_Observation { get; set; }
        public int Toatal_Mentor { get; set; }        
        public int Total_Target_Classroom_Obervation { get; set; }
        public string Target_Observation_Percentage { get; set; }
    }
    #endregion
}
