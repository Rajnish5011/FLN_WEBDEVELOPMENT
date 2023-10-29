using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Entities;

namespace ASPNetCoreFLN_APIs.Entities
{
    public class TotalNoOfStudent
    {
        public int Mentor_Id { get; set; }
        public int Teacher_Id { get; set; }
        public int Class_Id { get; set; }
        public int Section_Id { get; set; }
        public int Subject_Id { get; set; }
        public string Mentor_Name { get; set; }
        public string Teacher_Name { get; set; }
        public string Class_Name { get; set; }
        public string Section_Name { get; set; }
        public string Subject_Name { get; set; }
        public int Total_Student { get; }
        public int Total_Male { get; set; }
        public int Total_Female { get; set; }
        public int Total_Other { get; set; }
        public Int16 Present_Student_Count { get; set; }
        public Int16 Present_Male_Student { get; set; }
        public Int16 Present_FeMale_Student { get; set; }
    }
}
