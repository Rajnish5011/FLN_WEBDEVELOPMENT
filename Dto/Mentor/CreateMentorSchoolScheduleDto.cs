using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ASPNetCoreFLN_APIs.Dto.Mentor
{
    public class CreateMentorSchoolScheduleDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int School_Id { get; set; }
        public int Mentor_Id { get; set; }
        public DateTime Visit_Date { get; set; }

        public string Visit_Time_Start { get; set; }

        public string Visit_Time_End { get; set; }

        [Required(ErrorMessage = "*Required")]
        public int Created_By { get; set; }

    }
}
