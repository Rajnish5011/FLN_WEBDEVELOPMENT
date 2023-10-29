using System;

namespace ASPNetCoreFLN_APIs.Dto.StudentTeacherCommunication
{
    public class StudentTeacherCommunicationDto
    {
        public int Sender_Id { get; set; }
        public int Receiver_Id { get; set; }
        public int Role_Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Message_Text { get; set; }
    }
}
