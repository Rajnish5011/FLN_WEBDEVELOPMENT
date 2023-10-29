using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

//using static System.Net.Mime.MediaTypeNames;

namespace ASPNetCoreFLN_APIs.Entities
{

    public class SpotQuestions
    {
        //public int Question_Id { get; }
        //public int Question_Type_Id { get; }
        //public int Question_Option_Id { get; }
        //public string Question_Instruction { get; }
        //public string Question_Text { get; }
        //public string Question_Type { get; }
        //public bool Is_Draggable { get; }
        //public string Media_Type { get; }
        //public string Media_Url { get; }
        //public string Base64QuestionImage { get; }
        //public string Option_Media_Type { get; }
        //public string Option_Text { get; }
        //public string Option_Media_Url { get; }
        //public string Base64OptionImage { get; }
        //public bool Is_Correct { get; }
        public int Question_Group_Instruction_Id { get; set; }
        public bool Is_ORF_Required { get; set; }
        public bool Is_Spot_Required { get; set; }
        public int ORF_Question_Id { get; set; }
        public string Max_Seconds_To_Read { get; set; }
        public string ORF_Question_Text { get; set; }        
        public int Question_Id { get; set; }
        public Int16 Competency_Id { get; set; }
        public string Competency { get; set; }
        public string Question_Group_Instruction { get; set; }
        public string Question_Text { get; set; }
        public String Question_Type { get; set; }
        public Int32 Total_Question { get; set; }
        public Int32 Min_Attempt_Question { get; set; }
        public string Mastery_Criteria { get; set; }
        public Boolean Is_Draggable { get; set; }
        public string Question_Media_Type { get; set; }
        public string Option_Media_Type { get; set; }
        public string Question_Media_Url { get; set; }
        public Int32 Option_Media_Type_Id { get; set; }
        public int Question_Option_Id { get; set; }
        public string Option_Text { get; set; }
        public string Option_Media_Url { get; set; }
        public Boolean Is_Correct { get; set; }
    }

    public class PeriodicQuestions
    {
        public int Question_Id { get; }
        public byte Question_Type_Id { get; }
        public byte Assessment_Type_Id { get; }
        public string Assessment_Type { get; }
        public string Question_Instruction { get; }
        public string Question_Text { get; }
        public string Question_Type { get; }
        public bool Is_Draggable { get; }
        public string Media_Type { get; }
        public string Media_Url { get; }
        public string Base64QuestionImage { get; }
        public string Option_Media_Type { get; }
        public int Question_Option_Id { get; }
        public string Option_Text { get; }
        public string Option_Media_Url { get; }
        public string Base64OptionImage { get; }
        public bool Is_Correct { get; }
        public int Marks { get; }
        public int Competency_Id { get; }
        public string Competancy { get; }

    }
    public class PeriodicQuestionsByShedule
    {
        public int Question_Id { get; }
        public byte Question_Type_Id { get; }
        public byte Assessment_Type_Id { get; }
        public string Assessment_Type { get; }
        public string Question_Instruction { get; }
        public string Question_Text { get; }
        public string Question_Type { get; }
        public bool Is_Draggable { get; }
        public string Media_Type { get; }
        public string Media_Url { get; }
        public string Base64QuestionImage { get; }
        public string Option_Media_Type { get; }
        public int Question_Option_Id { get; }
        public string Option_Text { get; }
        public string Option_Media_Url { get; }
        public string Base64OptionImage { get; }
        public bool Is_Correct { get; }
        public int Marks { get; }
        public int Competency_Id { get; }
        public string Competancy { get; }
        public int Periodic_ORF_Question_Id { get; set; }
        public string ORF_Question_Text { get; set; }
        public int Min_Word_Read_Per_Minute { get; set; }
        public string Max_Seconds_To_Read { get; set; }

    }
    public class MediaType
    {
        public int Media_Type_Id { get; set; }
        public string Media_Type { get; set; }
    }
    public class MockQuestions
    {
        //public int Question_Id { get; }
        //public byte Question_Type_Id { get; }
        //public byte Assessment_Type_Id { get; }
        //public string Assessment_Type { get; }
        //public string Question_Instruction { get; }
        //public string Question_Text { get; }
        //public string Question_Type { get; }
        //public bool Is_Draggable { get; }
        //public string Media_Type { get; }
        //public string Media_Url { get; }
        //public string Base64QuestionImage { get; }
        //public string Option_Media_Type { get; }
        //public int Question_Option_Id { get; }
        //public string Option_Text { get; }
        //public string Option_Media_Url { get; }
        //public string Base64OptionImage { get; }
        //public bool Is_Correct { get; }
        public int Question_Id { get; }
        public byte Question_Type_Id { get; }
        public byte Assessment_Type_Id { get; }
        public string Assessment_Type { get; }
        public string Question_Instruction { get; }
        public string Question_Text { get; }
        public string Question_Type { get; }
        public bool Is_Draggable { get; }
        public string Media_Type { get; }
        public string Media_Url { get; }
        public string Base64QuestionImage { get; }
        public string Option_Media_Type { get; }
        public int Question_Option_Id { get; }
        public string Option_Text { get; }
        public string Option_Media_Url { get; }
        public string Base64OptionImage { get; }
        public bool Is_Correct { get; }
        public int Marks { get; }
        public int Periodic_ORF_Question_Id { get; set; }
        public string ORF_Question_Text { get; set; }
        public int Min_Word_Read_Per_Minute { get; set; }
        public string Max_Seconds_To_Read { get; set; }

    }
}
