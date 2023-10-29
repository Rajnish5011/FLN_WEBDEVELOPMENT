using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Entities
{    
    public class TeacherClassSection
    {
        public int Teacher_Class_Section_Id { get; set; }
        public int School_Teacher_Id { get; set; }        
        public int Teacher_Id { get; set; }
        public string Employee_Code { get; set; }
        public string Employee_Name { get; set; }
        public string Designation { get; set; }
        public int Class_Id { get; set; }
        public string Class_Name { get; set; }
        public int Section_Id { get; set; }
        public string Section_Name { get; set; }

    }
    public class TeacherListBySchool
    {
        public int Teacher_Id { get; }
        public string Employee_Code { get; }
        public string Employee_Name { get; }
        public string Designation { get; set; }
        public string Last_Observation_Date { get; set; }        
    }

    public class TeacherByEmployeeCode
    {
        public int Teacher_Id { get; }
        public string Employee_Code { get; }
        public string Employee_Name { get; }
        public string Designation { get; set; }
        public string Last_Observation_Date { get; set; }
    }
    public class TeacherListByClusterSchoolCode
    {
        public int School_Id { get; }
        public int Teacher_Id { get; }
        public string Employee_Code { get; }
        public string Employee_Name { get; }
        public string Designation { get; set; }
        public string School_Name { get; }
    }
    public class TeacherProfile
    {
        public int Teacher_Id { get; }
        public int School_Id { get; }
        public int Role_Id { get; set; }
        public string Employee_Code { get; }
        public string Employee_Name { get; }
        public string School_Name { get; }
        public string Designation { get; }
        public string Role_Name { get; set; }
        public string District_Name { get; }
        public string Block_Name { get; }
        public DateTime Date_Of_Birth { get; }
        public string Email_Id { get; }
        public Int64 Mobile_No { get; }



    }
    public class BlockTeacherList
    {
        public int Teacher_Id { get; }
        public string Employee_Code { get; }
        public string Employee_Name { get; }
        public string Designation { get; }
    }

    public class MISTeacherData
    {        
        public string employeeCode { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string datE_OF_BIRTH { get; set; }
        public string gradeName { get; set; }
        public string schoolCode { get; set;}
        public string udiseCode { get; set; }

    }
    public class TeacherListBySrnNo
    {
        public int Teacher_Id { get; }
        public string Employee_Code { get; }
        public string Employee_Name { get; }
    }

}
