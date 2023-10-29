using System.Collections.Generic;

namespace ASPNetCoreFLN_APIs.Dto
{
    public class ChatStudentResponse
    {
        public int Student_Id { get; set; }
        public string Student_Name { get; set; }
    }

    public class ChatTeacherResponse
    {
        public int Teacher_Id { get; set; }
        public string Employee_Name { get; set; }
        public string School_Name { get; set; }
    }
    
}
