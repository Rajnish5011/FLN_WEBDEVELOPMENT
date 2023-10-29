using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPNetCoreFLN_APIs.Dto.SpotAssessment
{
    public class SpotAssessmentDto
    {      

        [Required]
        public int Mentor_School_Visit_Id { get; set; }

        [Required]
        public int Mentor_Id { get; set; }

        [Required]
        public int Student_Id { get; set; }

        public bool Is_ORF_Required { get; set; }

        public bool Is_Spot_Required { get; set; }

        public int ORF_Question_Id { get; set; }

        public string Word_Read_Per_Minute { get; set; }
        public List<SpotAssessmentQuestionAnswers> QuestionAnswers { get; set; }
    }

    public class SpotAssessmentQuestionAnswers
    {
        //[Required]
        public int Question_Group_Instruction_Id { get; set; }

        //[Required]
        public int Question_Id { get; set; }

        //[Required]
        public int Question_Option_Id { get; set; }
    }
}
