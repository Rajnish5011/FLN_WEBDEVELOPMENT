using System;
using System.ComponentModel.DataAnnotations;

namespace ASPNetCoreFLN_APIs.Entities
{
    public class StudentTeacherCommunication
    {
        [Key]
        public int Communication_Id { get; set; }
        public int Sender_Id { get; set; }
        public int Receiver_Id { get; set; }
        public int Message_Id { get; set; }
        public int Role_Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
