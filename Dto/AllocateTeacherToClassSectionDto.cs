using System.Collections.Generic;

namespace ASPNetCoreFLN_APIs.Dto
{
    public class AllocateTeacherToClassSectionSave
    {
        public int School_Teacher_Id { get; set; }
        public byte Class_Id { get; set; }
        public byte Section_Id { get; set; }
        public int Created_By { get; set; }

    }
    public class AllocateTeacherToClassSectionUpdate
    {
        public byte Class_Id { get; set; }
        public byte Section_Id { get; set; }
        public int Modified_By { get; set; }
        
    }

}
