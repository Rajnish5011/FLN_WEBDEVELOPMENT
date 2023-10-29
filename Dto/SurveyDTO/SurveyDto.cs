using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace ASPNetCoreFLN_APIs.Dto.SurveyDTO
{
    public class SurveyDto
    {
        public List<string>? District_Id { get; set; }
        public List<string> Role_Id { get; set; }
        public List<string>? Designation_Id { get; set; }
        public string? Survey_Title { get; set; }
        public string ?Survey_Description { get; set; }
        public DateTime Survey_Start_Date { get; set; }
        public DateTime Survey_End_Date { get; set; }
        public List<SurveyQuestionGroupDto> Question_Groups { get; set; }
    }

    public class SurveyQuestionGroupDto
    {
        public string? Question_Section_Title { get; set; }
        public string? Question_Section_Description { get; set; }
        public List<SurveyQuestionDto> Questions { get; set; }
    }

    public class SurveyQuestionDto
    {
        public string? Survey_Question_Title { get; set; }
        public string? Survey_Question_Description { get; set; }
        public string? Survey_Question_Instructions { get; set; }
        public int Survey_Question_Type_Id { get; set; }
        public int? Survey_Sub_Question_Type_Id { get; set; }
        public int? Survey_Character_Limit { get; set; }
        public List<SurveyQuestionOptionsDto> Survey_Options_List { get; set; }

    }

    public class SurveyQuestionOptionsDto
    {
        public string? Survey_Options { get; set; }
    }
}
