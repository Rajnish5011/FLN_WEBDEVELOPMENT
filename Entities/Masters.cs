using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Entities;

namespace ASPNetCoreFLN_APIs.Entities
{
    public class Class
    {
        public int Id { get;  }
        public string Name { get;  }
    }

    public class ClassSection
    {
        public int Class_Id { get; }
        public int Section_Id { get; }
        public string Class_Name { get; }
        public string Section_Name { get; }
    }

    public class Blocks
    {
        public short Id { get; }
        public string Name { get; }
    }
    public class Roles
    {
        public short Role_Id { get; set; }
        public string Role_Name { get; set; }

    }
    public class Designations
    {
        public short Designation_Id { get; set; }
        public string Designation { get; set; }

    }
    public class Months
    {
        public int Id { get; }
        public string Name { get; }
    }
    public class Districts
    {
        public short Id { get; }
        public string Name { get; }

    }
    public class Section
    {       
        public int Id { get; }
        public string Name { get; }
    }
}
