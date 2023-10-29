using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Dto.Mentor
{
    public class MentorSchoolScheduleStartDto
    {
        [Required]
        public string base64Image { get; set; }

        [Required]
        public string Latitude { get; set; }

        [Required]
        public string Longitude { get; set; }
    }

    public class AddImageRequest
    {
        public string base64Image { get; set; }

    }
    //public class Image
    //{
    //public Guid Id { get; set; }
    //public byte[] base64Image { get; set; }
    //}
}
