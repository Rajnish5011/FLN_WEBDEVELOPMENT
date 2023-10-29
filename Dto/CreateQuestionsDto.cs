using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ASPNetCoreFLN_APIs.Dto
{

    public class Question
    {
        public string Question_Instruction { get; set; }

        public byte[] Question_Image { get; set; }

        [Required]
        public string Question_Text { get; set; }

        [Required]
        public byte Assessment_Type_Id { get; set; }

        [Required]
        public byte Question_Type_Id { get; set; }

        [Required]
        public int Competancy_Id { get; set; }

        [Required]
        public byte Class_Id { get; set; }

        [Required]
        public byte Subject_Id { get; set; }

        public byte Week_Id { get; set; }

        [Required]
        public int Created_By { get; set; }

        [Required]
        public List<QuestionOption> QuestionOption { get; set; }

    }
    public class QuestionOption
    {
        public string Option_Text { get; set; }

        public byte[] Option_Image { get; set; }

        [Required]
        public bool Is_Correct { get; set; }

    }
    public class ImageQuestion
    {
        public string Question_Instruction { get; set; }

        [Required]
        public string Question_Text { get; set; }

        [Required]
        public byte Assessment_Type_Id { get; set; }

        [Required]
        public byte Question_Type_Id { get; set; }

        [Required]
        public byte Media_Type_Id { get; set; }

        [Required]
        public int Competancy_Id { get; set; }

        [Required]
        public byte Class_Id { get; set; }

        [Required]
        public byte Subject_Id { get; set; }

        public byte Week_Id { get; set; }

        public int Created_By { get; set; }

        [Required]
        public List<ImageQuestionOption> ImageQuestionOption { get; set; }

    }
    public class ImageQuestionOption
    {
        [Required(ErrorMessage = "Please select Image")]
        [DataType(DataType.Upload)]
        public IFormFile OptionImage { get; set; }
                
        public string Option_Text { get; set; }

        [Required]
        public byte Media_Type_Id { get; set; }
                
        [Required]
        public bool Is_Correct { get; set; }

        public string Media_Url { get; set; }

    }
    public class Base64ImageQuestion
    {
        public string Base64QuestionImage { get; set; }

        public string Question_Instruction { get; set; }

        [Required(ErrorMessage = "*Question Text Required")]
        public string Question_Text { get; set; }
                
        [Required(ErrorMessage = "*Question Media Type ID")]
        public byte Media_Type_Id { get; set; }

        [Required(ErrorMessage = "*Option Media Type ID")]
        public byte Option_Media_Type_Id { get; set; }

        public string Question_Media_Url { get; set; }

        [Required]
        public byte Assessment_Type_Id { get; set; }

        [Required]
        public byte Question_Type_Id { get; set; }
                
        [Required]
        public bool Is_Draggable { get; set; } 

        [Required]
        public int Competancy_Id { get; set; }

        [Required]
        public byte Class_Id { get; set; }

        [Required]
        public byte Subject_Id { get; set; }

        public byte Week_Id { get; set; }
        //[Required]
        public int Marks { get; set; }
        public int Created_By { get; set; }

        public List<Base64ImageQuestionOption> Base64ImageQuestionOption { get; set; } = new List<Base64ImageQuestionOption>();

    }
    public class Base64ImageQuestionOption
    {                
        public string Base64OptionImage { get; set; }
                
        public string Option_Text { get; set; }

        [Required]
        public bool Is_Correct { get; set; } 

    }

    public class UpdatePeriodicQuestion
    {
        public int Question_Id { get; set; }

        public string Base64QuestionImage { get; set; }

        public string Question_Instruction { get; set; }

        [Required(ErrorMessage = "*Question Text Required")]
        public string Question_Text { get; set; }

        [Required(ErrorMessage = "*Question Media Type ID")]
        public byte Media_Type_Id { get; set; }

        [Required(ErrorMessage = "*Option Media Type ID")]
        public byte Option_Media_Type_Id { get; set; }

        public string Question_Media_Url { get; set; }

        //[Required]
        //public byte Assessment_Type_Id { get; set; }
                
        [Required]
        public bool Is_Active { get; set; }
               
        [Required]
        public int Marks { get; set; }
        public int Created_By { get; set; }

        public List<UpdatePeriodicQuestionOption> QuestionOptions { get; set; } = new List<UpdatePeriodicQuestionOption>();

    }
    public class UpdatePeriodicQuestionOption
    {
        [Required]
        public int Question_Option_Id { get; set; }

        public string Base64OptionImage { get; set; }

        [Required]
        public string Option_Text { get; set; }

        [Required]
        public bool Is_Correct { get; set; }

    }

    public enum IsForOption
    {
        ForOption = 1,
        NotForOption = 0
    }

    public class TeacherTrainingtQuestionDto
    {
        [Required(ErrorMessage = "*Required")]
        public int Schedule_Header_Test_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Question_Group_Name { get; set; }

        [Required(ErrorMessage = "*Required")]
        public int State_Admin_User_Id { get; set; }
        public List<TeacherTrainingQuestions> TestQuestions { get; set; } = new List<TeacherTrainingQuestions>();
    }
    public class TeacherTrainingQuestions
    {                        
        public string Base64QuestionImage { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Question_Text { get; set; }

        [Required(ErrorMessage = "*Required")]
        public byte Question_Type_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public byte Media_Type_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public byte Option_Media_Type_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public byte Question_Marks { get; set; }

        public string Question_Media_Url { get; set; }                
        
        public List<TeacherTestQuestionOption> QuestionOptions { get; set; } = new List<TeacherTestQuestionOption>();

    }
    public class TeacherTestQuestionOption
    {        
        public string Base64OptionImage { get; set; }

        public string Option_Text { get; set; }

        [Required(ErrorMessage = "*Required")]
        public bool Is_Correct { get; set; }

    }

    public class UpdateTeacherTrainingQuestionsGroup
    {
        [Required(ErrorMessage = "*Required")]
        public int Question_Group_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Question_Group_Name { get; set; }
               
        [Required(ErrorMessage = "*Required")]
        public int State_Admin_User_Id { get; set; }              

        [Required(ErrorMessage = "*Required")]
        public bool Is_Active { get; set; }
    }
    public class UpdateTeacherTrainingQuestions
    {
        [Required(ErrorMessage = "*Required")]
        public int Question_Id { get; set; }

        public string Question_Text { get; set; }

        [Required(ErrorMessage = "*Required")]
        public byte Question_Type_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public byte Media_Type_Id { get; set; }
                        
        [Required(ErrorMessage = "*Required")]
        public int State_Admin_User_Id { get; set; } 

        [Required(ErrorMessage = "*Required")]
        public byte Question_Marks { get; set; }

        [Required(ErrorMessage = "*Required")]
        public bool Is_Active{ get; set; }

    }
    public class UpdateTeacherTrainingQuestionOption
    {
        [Required(ErrorMessage = "*Required")]
        public int Question_Option_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Option_Text { get; set; }
                
        [Required(ErrorMessage = "*Required")]
        public int State_Admin_User_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public bool Is_Correct { get; set; }

        [Required(ErrorMessage = "*Required")]
        public bool Is_Active { get; set; }
    }
}
