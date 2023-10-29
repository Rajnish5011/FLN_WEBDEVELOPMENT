using System;
using System.Collections.Generic;

namespace ASPNetCoreFLN_APIs.Dto.StudentTeacherCommunication
{
    public class StudentTeacherMessagesDto
    {
        public int Message_Id { get; set; }
        public string Message_Text { get; set; }
        public DateTime CreateDate { get; set; }
        public int Receiver_Id { get; set; }
        public int Sender_Id { get; set; }
    }

    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }


}
