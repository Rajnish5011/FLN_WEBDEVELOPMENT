using System.Collections.Generic;
using System;

namespace ASPNetCoreFLN_APIs.Dto
{

    public class StudentAttendance
    {
        public int Student_Id { get; }
        public string Srn_No { get; }
        public string Student_Name { get; }
        public byte Attendance_Status { get; }
    }
    public class StudentsAttedanceByTeacher
    {
        public int Class_Id { get; set; }
        public int Section_Id { get; set; }
        public string Class_Name { get; set; }
        public string Section_Name { get; set; }
        public int Total_student { get; set; }
        public int Present_student { get; set; }
        public int Absent_student { get; set; }        
        public DateTime Attendance_Date { get; set; }
        public bool Is_Attendance_Marked { get; set; }
    }
    public class StudentAttendanceBySrn
    {
        public string Srn_No { get; }
        public string Student_Name { get; }
        public string Class_Name { get; set; }
        public string Section_Name { get; set; }
        public DateTime Attendance_Date { get; }
        public string Attendance_Status { get; }
    }
}
