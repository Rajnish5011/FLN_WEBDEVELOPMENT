using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPNetCoreFLN_APIs.Entities
{
    public class UserDashboardLoginResponse
    {
        public int User_Id { get; set; }
        public int Role_Id { get; set; }
        public string Role_Name { get; set; }
        public int School_Id { get; set; }
        public int Cluster_School_Id { get; set; }
        public string Cluster_School_Name { get; set; }
        public string Full_Name { get; set; }        
        public string State_Name { get; set; }
        public short Block_Id { get; set; }
        public string Block_Name { get; set; }
        public int District_Id { get; set; }
        public string District_Name { get; set; }
        public int Board_Id { get; set; }
        public string Board_Name { get; set; }
        public int Medium_Id { get; set; }
        public string Medium { get; set; }
        public string School_Code { get; set; }
        public string UDISE_Code { get; set; }
        public string Cluster_UDISE_Code { get; set; }
        public string Cluster_School_Code { get; set; }
        public string School_Name { get; set; }
        public string School_Image_Url { get; set; }
        public string Phone_No { get; set; }
        public string Mobile_No { get; set; }
        public string Email_Id { get; set; }
        public string School_Address { get; set; }
        public string School_Contact { get; set; }
        public string School_Longitude { get; set; }
        public string School_Latitude { get; set; }

    }
    public class HelpDeskUserLoginResponse
    {
        public int Help_Desk_User_Id { get; set; }
        public string Full_Name { get; set; }
        public string Username { get; set; }
        public int Role_Id { get; set; }
        public string Role_Name { get; set; }
        public string State_Name { get; set; }
        public string Email_Id { get; set; }
        public string Contact_Number { get; set; }
    }
    public class ParentLoginRespone
      {
        public int Student_Id { get; set; }
        public string Srn_No { get; set; }
        public string Student_Name { get; set; }
        public int School_Id { get; set; }
        public string School_Name { get; set; }
        public int Class_Id { get; set; }
        public string Class_Name { get; set; }
        public int Section_Id { get; set; }
        public string Section_Name { get; set; }
        public string Father_Name { get; set; }
        public string Mother_Name { get; set; }
        public byte Role_Id { get; set; }
        public string Role_Name { get; set; }
        public int Gender_Id { get; set; }
        public string Gender { get; set; }
        public string Date_Of_Birth { get; set; }
        public string Adhaar_No { get; set; }
        public string Email_Id { get; set; }
        public string Mobile_No { get; set; }
        public string Father_Mobile_No { get; set; }
        public string Mother_Mobile_No { get; set; }
        public string Address { get; set; }
    }
}