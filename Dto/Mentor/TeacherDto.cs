using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ASPNetCoreFLN_APIs.Dto.Mentor
{
    public class CreateClusterMeetingScheduleDto
    {
        [Required(ErrorMessage = "*Required")]
        public int Mentor_Id { get; set; }
        public DateTime Meeting_Date { get; set; }
        public string Meeting_Time { get; set; }
        public string Meeting_Place { get; set; }
        public string Meeting_Agenda { get; set; }
    }
    public class UpdateClusterMeetingScheduleDto
    {
        public DateTime Meeting_Date { get; set; }
        public DateTime Meeting_End_Date { get; set; }
        public string Meeting_Time { get; set; }
        public string Meeting_Place { get; set; }
        public string Meeting_Agenda { get; set; }
    }
}
