using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ASPNetCoreFLN_APIs.Dto.Mentor
{
    public class MentorClassObervationDto
    {
        [Required(ErrorMessage = "*Required")]
        public int Mentor_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public int Mentor_School_Visit_Id { get; set; }

        public string Observation_Feedback { get; set; }
        public string Observation_Remark { get; set; }
        public string QuestionAnswer { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Time_Taken { get; set; }
    }
}
