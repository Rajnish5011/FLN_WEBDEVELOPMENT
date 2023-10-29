using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Entities
{

    public class PeriodicAssessmentSchedule
    {
        public int Periodic_Assessment_Schedule_Id { get; }
        public short Periodic_Assessment_Id { get; }
        public byte Class_Id { get; }
        public byte Subject_Id { get; }
        public string Class_Name { get; }
        public string Subject_Name { get; }
        public byte Number_Of_Questions { get; }
        public string Assessment_Type { get; }
        public DateTime Start_Date { get; }
        public DateTime End_Date { get; }
        public int Periodic_Assessment_Schedule_Competancy_Id { get; }
        public int Competancy_Id { get; }
        public string Competancy { get; }
    }
}
