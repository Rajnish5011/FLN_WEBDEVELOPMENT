using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPNetCoreFLN_APIs.Dto.Login
{
    public class LoginDto
    {
        [Required]
        public int StateId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class LoginResponseDto
    {
        [Required]
        public int State_User_Id { get; set; }
        [Required]
        public int State_Id { get; set; }
    }
    public class MentorLoginDto
    {
        [Required]
        //[RegularExpression("^[0-9A-Za-z]*$", ErrorMessage = "UniqueCode Alphabets and Numbers allowed.")]        
        [RegularExpression("^[\\S]+$", ErrorMessage = "Space not allowed in Username.")]
        public string UniqueCode { get; set; }
        
        [Required]
        [RegularExpression("^[\\S]+$", ErrorMessage = "Space not allowed in Password.")]
        public string Password { get; set; }
    }
    public class TeacherLoginDto
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
    public class TeacherLoginResponseDto
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public TeacherLoginDetailedResponseDto Response { get; set; }
    }
    public class TeacherLoginDetailedResponseDto
    {
        public string mapping { get; set; }
        public string forservice { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string instituteType { get; set; }
        public string payeeCode { get; set; }
        public string employeeCode { get; set; }
    }
    public class UserDashboardLoginDto
    {

        [Required(ErrorMessage = "Username is Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
    public class HelpDeskUserLoginDto
    {

        [Required(ErrorMessage = "Username is Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
    public class UserDashboardLoginResponseDto
    {
        public int User_Id { get; set; }
        public int School_Id { get; set; }
        public int Cluster_School_Id { get; set; }
        public string Cluster_School_Name { get; set; }
        public byte State_Id { get; set; }
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
        public int Role_Id { get; set; }
        public string Role_Name { get; set; }
    }
    public class ParentLoginDto
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}