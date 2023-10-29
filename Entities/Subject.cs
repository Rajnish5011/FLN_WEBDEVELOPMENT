using System;
using System.Collections.Generic;


namespace ASPNetCoreFLN_APIs.Entities
{
    public class Subject
    {
        public short Id { get; }
        public string Name { get; }

    }
    public class SubjectTopic
    {
        public byte Id { get; }
        public string Name { get; }

    }
    public class SubjectCompetancy
    {
        public int Id { get; }
        public string Text { get; }
    }
    public class SubjectCompetancyDetail
    {
        public int Competancy_Id { get; }
        public int Assessment_type_Id { get; }
        public string Assessment_Type { get; }
        public string Subject_Name { get; }
        public string Competancy { get; }
    }
    public class SpotSubjectCompetancy
    {
        public int Competency_Id { get; }
        public string Competency { get; }
    }
}
