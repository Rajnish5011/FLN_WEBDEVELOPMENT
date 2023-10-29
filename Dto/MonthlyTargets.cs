using System;
using System.ComponentModel.DataAnnotations;

namespace ASPNetCoreFLN_APIs.Dto
{
    public class MonthlyTarget
    {
        public string Target_Title { get; set; }
        public byte Role_Id { get; set; }
        public byte Designation_Id { get; set; }
        public byte Target_Unique_School_Visit { get; set; }
        public short Target_Classroom_Observation { get; set; }
        public short Target_Spot_Assessment { get; set; }
        public int State_Admin_User_Id { get; set; }
        public DateTime GetMonthYear { get; set; }
    }
    public class UpdateMonthlyTarget
    {
        public int Monthly_Target_Id { get; set; }
        public int State_Admin_User_Id { get; set; }
        public string Target_Title { get; set; }        
        public byte Target_Unique_School_Visit { get; set; }
        public short Target_Classroom_Observation { get; set; }
        public short Target_Spot_Assessment { get; set; }        
    }
}
