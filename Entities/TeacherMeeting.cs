using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Entities
{
   public class ClusterTeacherMeetingSchedule
    {
        public int Cluster_Meeting_Schedule_Id { get; set; }
        public string Meeting_Agenda { get; }
        public string Meeting_Date { get; }
        public string Meeting_Time { get; }
        public string Meeting_Place { get; }
        public string Meeting_Remarks { get; }
        public string Meeting_End_Date { get; }
        public bool Is_Remarks_Done { get; }        
        public bool Is_Meeting_End { get; }
        public bool Is_For_CurrentMonth { get; }        
        public bool Is_Attendance_Done { get; }

    }
    public class ClusterTeacherMeetingScheduleForTeacher
    {
        public int Cluster_Meeting_Schedule_Id { get; set; }
        public string Meeting_Agenda { get; }
        public string Meeting_Date { get; }
        public string Meeting_Time { get; }
        public string Meeting_End_Date { get; }
        public string Meeting_Place { get; }
        public string Meeting_Remarks { get; }
        public bool Is_Meeting_End { get; }
        public bool Is_Attendance_Done { get; }
        public string Attendance_Status { get; }
    }
  
    public class GetSelectedTeachers
    {
        public int Training_Schedule_Teacher_Id { get; }
        public int Teacher_Id { get; }
        public string Employee_Code { get; }
        public string Employee_Name { get; }
        public string Designation { get; set; }                   
        public bool Is_Latest_Attendance_Done { get; set; }
        public bool Is_Present { get; set;  }
    }
}
