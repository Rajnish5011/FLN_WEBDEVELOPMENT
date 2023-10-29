using System;
using System.Text.Json.Serialization;

namespace ASPNetCoreFLN_APIs.Dto.Mentor
{
    public class MentorSchoolScheduleVisitDto
    {
        public int Mentor_School_Schedule_Id { get; set; }
        public int Mentor_Id { get; set; }
        public int Class_Id { get; set; }
        public int Section_Id { get; set; }
        public int Teacher_Id { get; set; }
        public int Subject_Id { get; set; }

    }
}
