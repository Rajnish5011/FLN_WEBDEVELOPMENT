using System;

namespace ASPNetCoreFLN_APIs.Entities
{
    public class ObservationQuestionOptions
    {
        public int Observation_Question_Option_Id { get; set; }

        public short? Observation_Question_Id { get; set; }

        public string Observation_Question_Option { get; set; }

        public bool? Is_Error_Message_Required { get; set; }

        public string Error_Message { get; set; }

        public string Logic_Comment { get; set; }

        public bool? Is_Open_Questions { get; set; }

        public bool? Is_Only_Selectable { get; set; }

        public string Open_Question_IDs { get; set; }

        public bool Is_Active { get; set; }

        public int Created_By { get; set; }

        public DateTime Created_On { get; set; }
        public bool show_Alert_OnSelect { get; set; }
    }
}
