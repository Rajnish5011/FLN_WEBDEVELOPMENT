using System;
using System.Collections.Generic;
using static ASPNetCoreFLN_APIs.Helper.Enums;

namespace ASPNetCoreFLN_APIs.Entities
{
    public class ObservationQuestionMaster
    {
        public short Observation_Question_Id { get; set; }
        public byte Question_Scope_Id { get; set; }
        public byte? Subject_Id { get; set; }
        public string Question_Number { get; set; }
        public string Observation_Question { get; set; }
        public int? Observation_Question_Option_Id { get; set; }
        public string Question_Logic { get; set; }
        public string Response_Type { get; set; }
        public bool? Is_Multiple_Choice { get; set; }
        public bool? Is_Open_Questions { get; set; }
        public bool Is_Dependent{ get; set; }
        public bool Is_Multiple_Dependent_Option { get; set; }
        public int Dependent_Question_ID { get; set; }
        public int Dependent_Question_Option_Id { get; set; }
        public string? Multiple_Dependent_Options { get; set; }
        public bool Is_Question_Have_Alert { get; set; }        
        public bool Is_For_Report { get; set; }
        public bool Is_Active { get; set; }
        public ObservationQuestionsType Type { get; set; }
        public int Created_By { get; set; }
        public int? Modified_By { get; set; }
        public DateTime Created_On { get; set; }
        public DateTime? Modified_On { get; set; }
        public List<ObservationQuestionOptions> ObservationQuestionOptions { get; set; }    

    }
}
