using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Entities;

namespace ASPNetCoreFLN_APIs.Entities
{
    public class ClassObservationByMentor
    {
        public int Mentor_School_Visit_Id { get; }
        public int Mentor_Class_Teacher_Observed_Id { get; }
        public int Mentor_Id { get; }
        public int Teacher_Id { get; }
        public int Class_Id { get; }
        public int Section_Id { get; }
        public int Subject_Id { get; }
        public string Teacher_Name { get; }
        public string Class_Name { get; }
        public string Section_Name { get; }
        public string Subject_Name { get; }
        public DateTime Created_On { get; }
        public bool Is_ObservationDone { get; }
        public short Total_Student_Count { get; }
        public short Total_Spot_Done { get; }
        public string Time_Taken { get; set; }
        public DateTime Feedback_Date { get; set; }
        public string Observation_Feedback { get; }
        public string Observation_Remark { get; }
        public string Mentor_Name { get; }
        public int Present_Student_Count { get; }
        public int Student_Id { get; set; }
        public string Student_Name { get; }
        public string Srn_No { get; set; }        
        public string School_Name { get; set; }


    }

    public class MentorMonthlyTarget
    {
        public int Target_Unique_School_Visit { get; set; }
        public int Target_Classroom_Observation { get; set; }
        public int Target_Spot_Assessment { get; set; }        
        public int Unique_School_Visit { get; set; }
        public int Classroom_Observation { get; set; }
        public int Spot_Assessment_Done { get; set; }
        
    }
}
