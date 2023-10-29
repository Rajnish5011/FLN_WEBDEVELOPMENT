using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json.Serialization;

namespace ASPNetCoreFLN_APIs.Entities
{
    public class MentorSchoolSchedule
    {        
        public int Id { get; set; }                
        public bool Is_Schedule_Started { get; set; }
        public int School_Id { get; set; }    
        public string School_Name { get; set; }
        public DateTime Visit_Date { get; set; }

        //[JsonIgnore]
        //public TimeSpan Visit_Time_Start { get; set; }
        public string Visit_Time_Start { get; set; }
        //[JsonIgnore]
        //public TimeSpan Visit_Time_End { get; set; }
        public string Visit_Time_End { get; set; }
        public string School_Latitude { get; set; }
        public string School_Longitude { get; set; }

    }

    public class MentorPastVisit
    {
        public int Mentor_School_Visit_Id { get; }
        public int Mentor_School_Schedule_Id { get; }
        public int Mentor_Id { get; }
        public int School_Id { get; }
        public int Teacher_Id { get; }
        public string Teacher_Name { get; }
        public int Class_Id { get; }
        public string Class_Name { get; }
        public int Section_Id { get; }
        public string Section_Name { get; }
        public int Subject_Id { get; }
        public string Subject_Name { get; }
        public string Visit_Date { get; }
        public int Present_Student_Count { get; }
        public int Total_Student { get; set; }
        public int Total_Students_Spot_Done { get;}
        public string IsVisitDone { get; }
        public string Observation_Status { get; }
        public string Attendance_Status { get; }
        public string Spot_Assessment_Count { get; }
        public int TotalRecords { get; set; }
    }
    public class MentorPastVisitBySchoolSheduleId
    {
        public int Mentor_School_Visit_Id { get; }
        public int Mentor_School_Schedule_Id { get; }
        public int Mentor_Id { get; }
        public int School_Id { get; }
        public int Teacher_Id { get; }
        public string Teacher_Name { get; }
        public int Class_Id { get; }
        public string Class_Name { get; }
        public int Section_Id { get; }
        public string Section_Name { get; }
        public int Subject_Id { get; }
        public string Subject_Name { get; }
        public string Visit_Date { get; }
        public int Present_Student_Count { get; }
        public int Total_Student { get; set; }
        public int Total_Students_Spot_Done { get; }
        public string IsVisitDone { get; }
        public string Observation_Status { get; }
        public string Attendance_Status { get; }
        public string Spot_Assessment_Count { get; }
        //public int TotalRecords { get; set; }
    }

    public class MentorPastVisitByMentorID
    {
        public int Mentor_School_Visit_Id { get; }
        public int Mentor_School_Schedule_Id { get; }
        public int Mentor_Id { get; }
        public int School_Id { get; }
        public int Teacher_Id { get; }
        public string School_Name { get; }
        public string Teacher_Name { get; }
        public int Class_Id { get; }
        public string Class_Name { get; }
        public int Section_Id { get; }
        public string Section_Name { get; }
        public int Subject_Id { get; }
        public string Subject_Name { get; }
        public string Visit_Date { get; }
        public int Present_Student_Count { get; }
        public int Total_Student { get; set; }
        public int Total_Students_Spot_Done { get; }
        public string IsVisitDone { get; }
        public string Observation_Status { get; }
        public string Attendance_Status { get; }
        public string Spot_Assessment_Count { get; }
        public int TotalRecords { get; set; }
    }
    public class MentorProfile
    {
        public int Mentor_Id { get; set; }
        public int District_Id { get; set; }
        public int Block_Id { get; set; }
        public int Cluster_School_Id { get; set; }
        public int Designation_Id { get; set; }
        public string Designation { get; set; }
        public string District_Name { get; set; }
        public string Block_Name { get; set; }
        public string Cluster_School_Code { get; set; }
        public string Cluster_School_Name { get; set; }
        public string Unique_Code { get; set; }
        public string Mentor_Name { get; set; }
        public string Date_Of_Birth { get; set; }
        public string Email_Id { get; set; }
        public string Mobile_No { get; set; }
    }
    public class MentorLoginResponse
    {
        public int Mentor_Id { get; set; }
        public int District_Id { get; set; }
        public int Block_Id { get; set; }
        public int Cluster_School_Id { get; set; }
        public byte Role_Id { get; set; }
        public string Role_Name { get; set; }
        public int Designation_Id { get; set; }
        public string Designation { get; set; }
        public string District_Name { get; set; }
        public string Block_Name { get; set; }
        public string Cluster_School_Code { get; set; }     
        public string Cluster_School_Name { get; set; }
        public string Unique_Code { get; set; }
        public string Mentor_Name { get; set; }
        public string Date_Of_Birth { get; set; }
        public string Email_Id { get; set; }
        public string Mobile_No { get; set; }
    }
    public class MentorScheduleStartImage
    {
        public byte[] Mentor_Image_Data { get; set; }

    }
    }

