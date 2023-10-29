using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPNetCoreFLN_APIs.Entities
{

    public class Survey
    {

        public int Survey_Id { get; set; }
        public string? Survey_Title { get; set; }
        public string? Survey_Description { get; set; }
        public DateTime Survey_Start_Date { get; set; }
        public DateTime Survey_End_Date { get; set; }
        public DateTime Created_On { get; set; }
        public string Role_Id { get; set; }
        public string? Designation_Id { get; set; }
        public string? District_Id { get; set; }
        public string Survey_Form { get; set; }
    }
}
