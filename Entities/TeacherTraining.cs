using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Entities
{
    public class TeacherTrainingSchedule
    {
        public int Teacher_Training_Schedule_Id { get; }
        public int Training_Schedule_Teacher_Id { get; }        
        public int Block_Admin_User_Id { get; set; }
        public string Block_Name { get; set; }
        public string Training_Title { get; set; }
        public DateTime Training_Start_Date { get; set; }
        public DateTime Training_End_Date { get; set; }
        public string Training_Place { get; set; }
        public string Training_Start_Time { get; set; }
        public string Training_End_Time { get; set; }
        public string Training_Description { get; set; }
        public bool Is_Attendance_Done { get; }
        public bool Is_Training_End { get; }

        public int Teacher_Training_Schedule_Test_Id { get; set; }
        public int Subject_Id { get; set; }
        public string Test_Name { get; set; }
        public string Test_Type { get; set; }
        public string Subject_Name { get; set; }
        public string Test_Score { get; set; }
        public bool Is_Test_Done { get; set; }
        public bool Is_Active { get; set; }
    }

    public class TrainingScheduleHeader
    {
        public int Training_Schedule_Header_Id { get; set; }
        public int Schedule_Header_Test_Id { get; set; }
        public int Subject_Id { get; set; }
        public string Schedule_Header_Title { get; set; }
        public string Test_Name { get; set; }
        public string Test_Type { get; set; }
        public string Subject_Name { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string Training_Description { get; set; }
        
    }
    public class TeacherTrainingTestList
    {
        public Int32 Teacher_Training_Test_Id { get; }
        public Int32 Subject_Id { get; }
        public string Subject_Name { get; }
        public string Test_Name { get; }
        public string Test_Type { get; }
        public bool Is_Active { get; set; }
        public string Description { get; set; }
    }
    public class TeacherTrainingAttendanceDto
    {
        [Required(ErrorMessage = "*Required")]
        public int Teacher_Training_Schedule_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public int Block_Admin_User_Id { get; set; }

        public List<ScheduledTeachers> TeacherSelection { get; set; }
    }
    public class ScheduledTeachers
    {
        [Required(ErrorMessage = "*Required")]
        public int Training_Schedule_Teacher_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public bool Is_Present { get; set; }
    }

    public class TeacherTraningQuestions
    {
        public int Question_Group_Id { get; set; }
        public int Question_Id { get; set; }
        public string Question_Group_Name { get; set; }
        public string Question_Text { get; set; }
        public string Question_Type { get; set; }
        public string Question_Media_Type { get; set; }
        public string Option_Media_Type { get; set; }
        public string Question_Media_Url { get; set; }
        public int Option_Media_Type_Id { get; set; }
        public int Question_Option_Id { get; set; }
        public string Option_Text { get; set; }
        public string Option_Media_Url { get; set; }
        public Boolean Is_Correct { get; set; }
    }

    public class UpdateTeacherTrainingAttendanceDto
    {
        [Required(ErrorMessage = "*Required")]
        public int Training_Schedule_Attendance_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public int Block_Admin_User_Id { get; set; }
        public List<ScheduledTeachers> TeacherSelection { get; set; }
    }
    public class TrainingScheduleAttendance
    {
        public int Training_Schedule_Attendance_Id { get; }       
        public string Training_Title { get; set; }
        public DateTime Attendance_Date { get; set; }
        public bool Is_Attendance_Done { get; set; }
    }    
    public class TrainingScheduleTeachersAttendance
    {
        public int Schedule_Teacher_Attendance_Id { get; }        
        public string Employee_Code { get; }
        public string Employee_Name { get; }
        public string Designation { get; }
        public string Attendance_Date_Time { get; }
        public bool Is_Present { get; }
    }
}