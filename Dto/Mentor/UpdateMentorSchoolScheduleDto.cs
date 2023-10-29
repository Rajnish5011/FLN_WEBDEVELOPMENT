using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ASPNetCoreFLN_APIs.Dto.Mentor
{
    public class UpdateMentorSchoolScheduleDto
    {
        [Required]
        public int School_Id { get; set; }

        [Required]
        public DateTime Visit_Date { get; set; }

        public string Visit_Time_Start { get; set; }

        public string Visit_Time_End { get; set; }

        [Required]
        public int Modified_By { get; set; }

    }
}
