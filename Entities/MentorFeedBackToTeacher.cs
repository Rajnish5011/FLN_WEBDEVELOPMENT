using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Entities
{
    public class MentorFeedBackToTeacher
    {
        public int Id { get; }
        public int Mentor_Id { get;}
        public int Teacher_Id { get; }
        public string Mentor_Name { get; }
        public string Observation_Feedback { get; }
        public string Observation_Remark { get; }
        public string Time_Taken { get; }
        public string Feedback_Date { get; }
    }
    public class MentorClassTeacherObservationFeedBack
    {
        public int Observation_Feedback_Id { get; }      
        public string Hindi_Feedback_For_Mentor { get; }
        public string English_Feedback_For_Mentor { get; }
        public string Hindi_Feedback_For_Teacher { get; }
        public string English_Feedback_For_Teacher { get; }
        public byte Priority { get; }
    }
}
