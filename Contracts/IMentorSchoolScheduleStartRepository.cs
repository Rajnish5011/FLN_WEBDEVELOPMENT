using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Entities;
using ASPNetCoreFLN_APIs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;
using System.IO;
using System.Drawing;
using Microsoft.IdentityModel.Tokens;
using ASPNetCoreFLN_APIs.Dto.Mentor;

namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface IMentorSchoolScheduleStartRepository
    {
        public Task<int> MentorSchoolScheduleStart(int MentorSchoolScheduleId, int MentorId, [FromBody] MentorSchoolScheduleStartDto MentorSchoolScheduleStart);

        public Task<byte[]> GetMentorImage(int Mentor_School_Schedule_Start_Id);

    }
}
