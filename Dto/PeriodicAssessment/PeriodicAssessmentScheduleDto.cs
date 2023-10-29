using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Dto.PeriodicAssessment
{
    public class PeriodicAssessmentScheduleDto
    {
        public byte Periodic_Assessment_Id { get; set; }
        public byte Class_Id { get; set; }
        public byte Subject_Id { get; set; }
        public byte Total_Number_Of_Questions { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public int Created_By { get; set; }
        public List<StudentPeriodicAssessmentScheduleCompetancy> ScheduleCompetancy { get; set; }
        public List<PeriodicAssessmentScheduleORFQuestion> ScheduleORFQuestion { get; set; }
    }

    public class StudentPeriodicAssessmentScheduleCompetancy
    {
        public short Competancy_Id { get; set; }
        public int Number_Of_Question { get; set; }
    }
    public class PeriodicAssessmentScheduleORFQuestion
    {
        public string ORF_Question_Text { get; set; }
        public Int64 Min_Word_Read_Per_Minute { get; set; }
        public string Max_Seconds_To_Read { get; set; }
    }
    public class UpdatePeriodicAssessmentScheduleDto
    {
        public byte Periodic_Assessment_Id { get; set; }
        public byte Class_Id { get; set; }
        public byte Subject_Id { get; set; }
        public byte Number_Of_Questions { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public int Modified_By { get; set; }
        public List<UpdateStudentPeriodicAssessmentScheduleCompetancy> ScheduleCompetancy { get; set; }

    }

    public class UpdateStudentPeriodicAssessmentScheduleCompetancy
    {
        public int Periodic_Assessment_Schedule_Competancy_Id { get; set; }
        public short Competancy_Id { get; set; }
        public Int32 Number_Of_Question { get; set; }
    }
}

