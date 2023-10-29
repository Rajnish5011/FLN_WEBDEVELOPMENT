using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNetCoreFLN_APIs.Entities
{
    public class ObservationQuestionScopeMaster
    {
        public byte Question_Scope_Id { get; set; }

        public string Question_Scope { get; set; }

        public bool Is_Active { get; set; }

        public int Created_By { get; set; }

        public int? Modified_By { get; set; }

        public DateTime Created_On { get; set; }

        public DateTime? Modified_On { get; set; }

        public List<ObservationQuestionMaster> ObservationQuestionMasters { get; set; }
    }
}


