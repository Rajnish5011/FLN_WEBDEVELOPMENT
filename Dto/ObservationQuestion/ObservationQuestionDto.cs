using ASPNetCoreFLN_APIs.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static ASPNetCoreFLN_APIs.Helper.Enums;

namespace ASPNetCoreFLN_APIs.Dto.ObservationQuestion
{
    public class ObservationQuestionDto
    {
        public byte Question_Scope_Id { get; set; }
        public string Question_Scope { get; set; }
        public List<ObservationQuestionMasterDto> Questions { get; set; }
        //public List<ObservationQuestionOptionsDto> observationQuestionOptions { get; set; }
        public List<object> Answers { get; set; }
        public List<object> AnswersId { get; set; }
    }
   public class ObservationQuestionOptionsDto
    {
        public int? Observation_Question_Option_Id { get; set; }
        public string Observation_Question_Option { get; set; }
        public bool Show_Alert_OnSelect { get; set; }
    }
    public class ObservationQuestionMasterDto
    {
        [Key]
        public int Observation_Question_Id { get; set; }
        public string Question_Number { get; set; }
        public string Observation_Question { get; set; }
        public string Response_Type { get; set; }
        public bool Is_Multiple_Choice { get; set; }
        public bool Is_Dependent { get; set; }
        public bool Is_Multiple_Dependent_Option { get; set; }
        public int Dependent_Question_ID { get; set; }
        public int Dependent_Question_Option_Id { get; set; }
        public string Multiple_Dependent_Options { get; set; }        
        public List<ObservationQuestionOptions> QuestionOptions { get; set; }
    }

    public class ObservationQuestions
    {
        [Key]
        public byte Question_Scope_Id { get; set; }
        public string Question_Scope { get; set; }

        [Key]
        public int Observation_Question_Id { get; set; }
        public string Question_Number { get; set; }
        public string Observation_Question { get; set; }
        public string? Response_Type { get; set; }
        public bool Is_Multiple_Choice { get; set; }
        public bool Is_Dependent { get; set; }
        public bool Is_Multiple_Dependent_Option { get; set; }
        public int? Dependent_Question_ID { get; set; }
        public int? Dependent_Question_Option_Id { get; set; }
        public string? Multiple_Dependent_Options { get; set; }

        public int? Observation_Question_Option_Id { get; set; }

        public string Observation_Question_Option { get; set; }

        public bool? Is_Error_Message_Required { get; set; }

        public string? Error_Message { get; set; }

        public bool? Is_Open_Questions { get; set; }

        public bool? Is_Only_Selectable { get; set; }

        public bool? show_Alert_OnSelect { get; set; }
        //public List<ObservationQuestionOptions> QuestionOptions { get; set; }
    }
}
