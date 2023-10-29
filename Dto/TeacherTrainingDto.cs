using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ASPNetCoreFLN_APIs.TeacherTrainingDto
{
    public class CreateTeacherTrainingScheduleDto
    {
        [Required(ErrorMessage = "*Required")]
        public int Training_Schedule_Header_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public int Block_Admin_User_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public int Block_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Training_Title { get; set; }

        [Required(ErrorMessage = "*Required")]
        public DateTime Training_Start_Date { get; set; }

        [Required(ErrorMessage = "*Required")]
        public DateTime Training_End_Date { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Training_Place { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Training_Start_Time { get; set; }
        public string Training_End_Time { get; set; }
        public string Training_Description { get; set; }
        public List<TeacherTrainingTest> TeacherTest { get; set; }
    }
    public class TeacherTrainingTest
    {
        [Required(ErrorMessage = "*Required")]
        public int Schedule_Header_Test_Id { get; set; }
    }
    public class UpdateTeacherTrainingScheduleDto
    {
        [Required(ErrorMessage = "*Required")]
        public int Teacher_Training_Schedule_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public int Block_Admin_User_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Training_Title { get; set; }

        [Required(ErrorMessage = "*Required")]
        public DateTime Training_Start_Date { get; set; }

        [Required(ErrorMessage = "*Required")]
        public DateTime Training_End_Date { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Training_Place { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Training_Start_Time { get; set; }
        public string Training_End_Time { get; set; }
        public string Training_Description { get; set; }
    }


    public class UpdateTeacherTrainingScheduleHeaderDto
    {
        [Required]
        public int Training_Schedule_Header_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public int State_User_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Schedule_Header_Title { get; set; }

        [Required(ErrorMessage = "*Required")]
        public DateTime Start_Date { get; set; }

        [Required(ErrorMessage = "*Required")]
        public DateTime End_Date { get; set; }
        public string Description { get; set; }
        public List<ScheduleHeaderTest> HeaderTest { get; set; }
    }

    public class TrainingScheduleTeacherSelectionDto
    {

        [Required]
        public int Teacher_Training_Schedule_Id { get; set; }

        [Required]
        public int Block_Admin_User_Id { get; set; }

        public List<TrainingTeacherSelection> TeacherSelection { get; set; }
    }
    public class TrainingTeacherSelection
    {
        [Required(ErrorMessage = "*Required")]
        public int Teacher_Id { get; set; }
    }
    public class TeachersTrainingScheduleHeaderDto
    {
        [Required(ErrorMessage = "*Required")]
        public int State_User_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Schedule_Header_Title { get; set; }

        [Required(ErrorMessage = "*Required")]
        public DateTime Start_Date { get; set; }

        [Required(ErrorMessage = "*Required")]
        public DateTime End_Date { get; set; }
        public string Description { get; set; }
        public List<ScheduleHeaderTest> HeaderTest { get; set; }
    }
    public class ScheduleHeaderTest
    {
        [Required(ErrorMessage = "*Required")]
        public int Teacher_Training_Test_Id { get; set; }
    }
    public class TrainingScheduleTeacherAssessmentDto
    {
        [Required(ErrorMessage = "*Required")]
        public int Teacher_Training_Schedule_Test_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public int Training_Schedule_Teacher_Id { get; set; }
        public List<ScheduleTeacherAssessmentQuestionDto> Questions { get; set; }   
    }
    public class ScheduleTeacherAssessmentQuestionDto
    {        
        [Required(ErrorMessage = "*Required")]
        public int Question_Id { get; set; }
        //[Required(ErrorMessage = "*Required")]
        //public int Question_Option_Id { get; set; }
        public List<ScheduleTeacherAssessmentOptionDto> Options { get; set; }
    }
    public class ScheduleTeacherAssessmentOptionDto
    {        
        [Required(ErrorMessage = "*Required")]
        public int Question_Option_Id { get; set; }
    }
}
