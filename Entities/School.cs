using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Entities
{
    
    public class School
    {
        public int School_Id { get; }
        public string Udise_Code { get; }
        public int School_Code { get; }
        public string School_Name { get; }
    }
    public class SchoolList
    {
        public int School_Id { get; }
        public string School_Name { get; }
    }
    public class ClusterSchoolList
    {
        public int Cluster_School_Id { get; }
        public string Cluster_School_Name { get; }
        public string Udise_Code { get; }
    }
    public class SchoolListByMentorSchoolCode
    {
        public int School_Id { get; }
        public int School_Code { get; }
        public string Udise_Code { get; }
        public string School_Name { get; }
        public int ReturnValue { get; set; }
    }

}
