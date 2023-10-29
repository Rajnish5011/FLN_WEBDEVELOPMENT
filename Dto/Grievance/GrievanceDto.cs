using System;
using System.ComponentModel.DataAnnotations;

namespace ASPNetCoreFLN_APIs.Dto.Grievance
{
    public class CreateGrievanceDto
    {
        public int Grievance_Category_Id { get; set; }

        public int User_Id { get; set; }

        public DateTime Date_Of_Issue { get; set; }

        public string Grievance_Query { get; set; }

        public string Contact_Number { get; set; }
    }
    public class CreateGrievanceCategoryDto
    {

        [Required(ErrorMessage = "*Required")]
        public int Grievance_Category_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Category_Name { get; set; }

        [Required(ErrorMessage = "*Required")]
        public int User_Id { get; set; }

    }
}
