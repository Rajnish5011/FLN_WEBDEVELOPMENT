using System.Collections.Generic;
using System;
using System.Buffers.Text;

namespace ASPNetCoreFLN_APIs.Dto.SurveyDTO
{
    public class SurveyFormResponse
    {
        public int Survey_Id { get; set; }
        public string? Survey_Title { get; set; }
        public string? Survey_Description { get; set; }
        public DateTime Survey_Start_Date { get; set; }
        public DateTime Survey_End_Date { get; set; }
        public List<string>? District_Id { get; set; }
        public List<string> Role_Id { get; set; }
        public List<string>? Designation_Id { get; set; }
        public List<SurveyQuestionGroup> Question_Groups { get; set; }
    }
    public class SurveyForm
    {
        public int Survey_Id { get; set; }
        public string? Survey_Title { get; set; }
        public string? Survey_Description { get; set; }
        public DateTime Survey_Start_Date { get; set; }
        public DateTime Survey_End_Date { get; set; }
    }
    public class SurveyQuestionGroup
    {
        public string? Question_Section_Title { get; set; }
        public string? Question_Section_Description { get; set; }
        public List<SurveyQuestion> Questions { get; set; }
    }

    public class SurveyQuestion
    {
        public string? Survey_Question_Title { get; set; }
        public string? Survey_Question_Description { get; set; }
        public string? Survey_Question_Instructions { get; set; }
        public int Survey_Question_Type_Id { get; set; }
        public int? Survey_Sub_Question_Type_Id { get; set; }
        public List<SurveyQuestionOptions> Survey_Options_List { get; set; }
        public int? Survey_Character_Limit { get; set; }
    }

    public class SurveyQuestionOptions
    {
        public string? Survey_Options { get; set; }
    }

    public class SurveyAnswers
    {
        public int Survey_ID { get; set; }
        public int Mentor_ID { get; set; }
        public string Answer_Groups { get; set; }
    }
}
