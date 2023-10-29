using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public Int64 Srn { get; set; }
        public string Name { get; set; }
        public bool IsSpotDone { get; set; }
        public string Word_Read_Per_Minute { get; set; }
        public bool Mastery_Status { get; set; }        
        public DateTime? Spot_Done_Date { get; set;}
    }
    public class StudentByClusterUdiseCode
    {
        public int Id { get; set; }
        public Int64 Srn { get; set; }
        public string Name { get; set; }
        public int TotalRecords { get; set; }
    }
    public class StudentPeriodicStatus
    {
        public int Student_Id { get; }
        public string Srn { get; }
        public string Name { get; }
        public string Status { get; }
    }

    public class SpotDoneStudentMasteryStatus
    {
        public int Question_Group_Instruction_Id { get; set; }
        public string Question_Group_Instruction { get; set; }
        public Int16 Competency_Id { get; set; }
        public int Student_Id { get; set; }
        public string Srn_No { get; set; }
        public string Competency { get; set; }
        public string Class_Name { get; set; }
        public string Section_Name { get; set; }
        public string Subject_Name { get; set; }
        public string Student_Name { get; set; }
        public Int16 Total_Correct_Questions { get; set; }
        public string Mastery_Criteria { get; set; }
        public string Assessment_Week { get; set; }
        public string Word_Read_Per_Minute { get; set; }
        public bool Mastery_Status { get; set; }
    }
}
