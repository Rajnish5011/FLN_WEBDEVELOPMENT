using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Options;
using Dapper;
using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Entities;
using Microsoft.AspNetCore.Http;
using ASPNetCoreFLN_APIs.Models;
using System.IO;
using System.Text;
//using static System.Net.Mime.MediaTypeNames;
using System.Buffers.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using ASPNetCoreFLN_APIs.Dto.Mentor;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class MentorSchoolScheduleStartRepository : IMentorSchoolScheduleStartRepository
    {
        private readonly DapperContext _context;
        public MentorSchoolScheduleStartRepository(DapperContext context)
        {
            _context = context;
        }
        /*
        public async Task<int> MentorPictureSave(IFormFile imageFile, MentorSchoolScheduleStartDto MentorSchoolScheduleStart)
        {
            int ReturnVal = 0;

            string filePath = Path.GetTempFileName();
            using (var stream = File.Create(filePath))
            {
                await imageFile.CopyToAsync(stream);            
                
            }           
            //Converts image file into byte[]
            byte[] imageData = await File.ReadAllBytesAsync(filePath);
            byte[] imageBytes = Convert.FromBase64String(imageFile.ToString());
            var MentorImage = Convert.ToBase64String(imageData);

                var procedureName = "CreateMentorSchoolScheduleStart";
                var parameters = new DynamicParameters();
                parameters.Add("MentorSchoolScheduleId", MentorSchoolScheduleStart.MentorSchoolScheduleId, DbType.Int32);
            parameters.Add("MentorPicData", imageBytes, DbType.Binary);
            //parameters.Add("MentorPicData", MentorImage, DbType.String);
            parameters.Add("MentorPicName", imageFile.FileName);
                parameters.Add("SchoolId", MentorSchoolScheduleStart.SchoolId, DbType.Int32);
                parameters.Add("MentorId", MentorSchoolScheduleStart.MentorId, DbType.Int32);
                parameters.Add("Latitude", MentorSchoolScheduleStart.Latitude, DbType.String);
                parameters.Add("Longitude", MentorSchoolScheduleStart.Longitude, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);

                ReturnVal = 1;
                connection.Close();
                connection.Dispose();
            }              

            return ReturnVal;            
        }
        */       
        public async Task<byte[]> GetMentorImage(int Mentor_School_Schedule_Start_Id)
        {
            var procedureName = "GetMentorImageByMentorScheduleStartId";
            var parameters = new DynamicParameters();
            parameters.Add("Mentor_School_Schedule_Start_Id", Mentor_School_Schedule_Start_Id, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                byte[] ImgData = await connection.ExecuteScalarAsync<byte[]>(procedureName, parameters, commandType: CommandType.StoredProcedure);                
                return ImgData;
            }
        }
        public async Task<int> MentorSchoolScheduleStart(int MentorSchoolScheduleId, int MentorId, [FromBody] MentorSchoolScheduleStartDto MentorSchoolScheduleStart)
        {
            int ReturnVal = 0;
            byte[] imageInBytes = Convert.FromBase64String(MentorSchoolScheduleStart.base64Image);                       
            var procedureName = "CreateMentorSchoolScheduleStart";
            var parameters = new DynamicParameters();
            parameters.Add("MentorSchoolScheduleId", MentorSchoolScheduleId, DbType.Int32);
            parameters.Add("MentorPicData", imageInBytes, DbType.Binary);
            parameters.Add("MentorPicName", "Mentor" + MentorId + ".jpg");
            parameters.Add("MentorId", MentorId, DbType.Int32);
            parameters.Add("Latitude", MentorSchoolScheduleStart.Latitude, DbType.String);
            parameters.Add("Longitude", MentorSchoolScheduleStart.Longitude, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                ReturnVal = await connection.ExecuteScalarAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);

                connection.Close();
                connection.Dispose();
            }
            return ReturnVal;

        }
    }
}
