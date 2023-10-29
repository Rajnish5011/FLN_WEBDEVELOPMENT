using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Entities
{
    public class AssessmentType
    {
        public byte Id { get; }
        public string Name { get; }
    }
    public class QuestionType
    {
        public int Id { get; }
        public string Text { get; }
    }
}
