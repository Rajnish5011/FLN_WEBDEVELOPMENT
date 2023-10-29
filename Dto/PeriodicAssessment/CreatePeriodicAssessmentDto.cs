using System.Collections.Generic;

namespace ASPNetCoreFLN_APIs.Dto.PeriodicAssessment
{
    public class StudentPeriodicAssessmentSave
    {
        public int Periodic_Assessment_Schedule_Id { get; set; }

        public int School_Id { get; set; }

        public int Teacher_Id { get; set; }

        public int Student_Id { get; set; }

        public int Class_Id { get; set; }

        public int Section_Id { get; set; }      
        public int Subject_Id { get; set; }
        public bool Is_Online_Exam { get; set; }
        public int ORF_Question_Id { get; set; }
        public string Word_Read_Per_Minute { get; set; }

        public List<StudentPeriodicAssessmentQuestionAnswers> QuestionAnswers { get; set; }
    }

    public class StudentPeriodicAssessmentQuestionAnswers
    {
        public int Question_Id { get; set; }

        public int Question_Option_Id { get; set; }

    }
    //public class StudentMockAssessmentQuestionSave
    //{
       
    //    public int StudentID { get; set; }

     
    //    public int QuestionID { get; set; }

    //    public int QuestionOptionID { get; set; }

    //}
}
