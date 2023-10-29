using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace ASPNetCoreFLN_APIs.Dto
{

    public class StudentAttendanceDto
    {        
        public DateTime Attendance_Date { get; set; }
        public int School_Id { get; set; }
        public byte Class_Id { get; set; }
        public byte Section_Id { get; set; }
        public int Teacher_Id { get; set; }        
        public List<StudentsAttendance> Attendance { get; set; }
    }
    public class StudentsAttendance
    {
        public int Student_Id { get; set; }
        public byte Is_Present { get; set; }
    }

    public class ClusterTeachersAttendanceDto
    {
        [Required(ErrorMessage = "Meeting Schedule Id required")]
        public int Cluster_Meeting_Schedule_Id { get; set; }

        [Required(ErrorMessage = "Mentor Id required")]
        public int Mentor_Id { get; set; }        

        public List<TeachersAttendance> Attendance { get; set; }

    }
    public class TeachersAttendance
    {
        [Required(ErrorMessage = "Teacher Id required")]
        public int Teacher_Id { get; set; }

        [Required(ErrorMessage = "Attendance status required")]
        public bool Is_Present { get; set; }
    }

}
