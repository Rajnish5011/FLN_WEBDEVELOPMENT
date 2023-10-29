using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ASPNetCoreFLN_APIs.Models
{
    public class FileModel
    {
        //public string FileName { get; set; }

        //public FileTypes FileType { get; set; }

        public int MentorSchoolScheduleId { get; set; }

        public int SchoolId { get; set; }

        public int MentorId { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
   
}
