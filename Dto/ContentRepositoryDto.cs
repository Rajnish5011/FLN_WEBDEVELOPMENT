using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ASPNetCoreFLN_APIs.TeacherTrainingDto
{
    public class AddContentForRoleDto
    {
        [Required(ErrorMessage = "*Required")]
        public int Content_Category_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public byte Content_For_Role_Id { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Content_Title { get; set; }

        public string Content_Description { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Content_Url { get; set; }

        [Required(ErrorMessage = "*Required")]
        public int User_Id { get; set; }
    }
}
