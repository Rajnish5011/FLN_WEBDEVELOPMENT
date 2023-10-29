using System.Collections.Generic;

namespace ASPNetCoreFLN_APIs.Dto.PeriodicAssessment
{

    public class TeacherStudentMockAssessmentSave
    {
        public int Teacher_Id { get; set; }
        public int Student_Id { get; set; }
        public int Class_Id { get; set; }
        public int Section_Id { get; set; }
        public int Subject_Id { get; set; }
        public int ORF_Question_Id { get; set; }
        public string Word_Read_Per_Minute { get; set; }

        public List<TeacherStudentMockAssessmentQuestionAnswers> TeacherStudentMockQuestionAnswers { get; set; }
    }

    public class TeacherStudentMockAssessmentQuestionAnswers
    {
        public int Question_Id { get; set; }
        public int Option_Id { get; set; }

    }
    public class StudentMockAssessmentSave
    { 
        public int Student_Id { get; set; }
        public int Class_Id { get; set; }
        public int Section_Id { get; set; }
        public int Subject_Id { get; set; }
        public int ORF_Question_Id { get; set; }
        public string Word_Read_Per_Minute { get; set; }

        public List<StudentMockAssessmentQuestionAnswers> StudentMockQuestionAnswers { get; set; }
    }

    public class StudentMockAssessmentQuestionAnswers
    {
        public int Question_Id { get; set; }
        public int Option_Id { get; set; }

    }



}
