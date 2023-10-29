using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ASPNetCoreFLN_APIs.Contracts;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using ASPNetCoreFLN_APIs.Dto.Login;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DapperContext _context;
        private readonly IConfiguration _configuration;
        private static readonly HttpClient client = new HttpClient();

        public LoginRepository(DapperContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> GetLogin(LoginDto request)
        {
            var query = "GetStateUserLogin";
            var parameters = new DynamicParameters();
            parameters.Add("StateId", request.StateId, DbType.Int32);
            parameters.Add("UserName", request.UserName, DbType.String);
            parameters.Add("Password", request.Password, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<LoginResponseDto>(query, parameters,commandType:CommandType.StoredProcedure);
                return data.SingleOrDefault();
            }
        }

        public async Task<MentorLoginResponse> MentorLogin(MentorLoginDto request)
        {
            var procedureName = "GetMentorUserLogin";
            var parameters = new DynamicParameters();
            parameters.Add("UniqueCode", request.UniqueCode, DbType.String);
            parameters.Add("Password", request.Password, DbType.String);
                       
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<MentorLoginResponse>
                 (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.SingleOrDefault();
            }
        }
        public async Task<TeacherProfile> TeacherLogin(TeacherLoginDto request)
        {
            var query = "GetTeacherLogin";
            var parameters = new DynamicParameters();
            parameters.Add("Username", request.username, DbType.String);
            parameters.Add("Password", request.password, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<TeacherProfile>(query, parameters, commandType: CommandType.StoredProcedure);
                return data.SingleOrDefault();
            }
        }
        public async Task<IEnumerable<UserDashboardLoginResponse>> UserDashboardLogin(UserDashboardLoginDto request)
        {
            var procedureName = "GetUserDashboardLogin";
            var parameters = new DynamicParameters();
            parameters.Add("Username", request.Username, DbType.String);
            parameters.Add("Password", request.Password, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<UserDashboardLoginResponse>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }

        /*
        public async Task<HelpDeskUserLoginResponse> HelpDeskUserLogin(HelpDeskUserLoginDto request)
        {
            var procedureName = "GetHelpDeskUserLogin";
            var parameters = new DynamicParameters();
            parameters.Add("Username", request.Username, DbType.String);
            parameters.Add("Password", request.Password, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<HelpDeskUserLoginResponse>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.SingleOrDefault(); ;
            }
        }

        
        public async Task<TeacherLoginResponse    
        
        Dto> TeacherLogin(TeacherLoginDto request)
        {
            var serializeRequest = JsonConvert.SerializeObject(request);
            var json = System.Text.Json.JsonSerializer.Serialize<TeacherLoginDto>(request);
            //var token = Encrypt(json, _configuration.GetValue<string>("636f72616e6440656e637270244b4559"));
            //var Url = _configuration.GetValue<string>("https://api-staging.mis.oneschoolsuite.com/Login/FLNLogin");
            var token = Encrypt(json, _configuration.GetValue<string>("AppSettings:Key"));
            var Url = _configuration.GetValue<string>("AppSettings:TeacherLoginUrl");
            var values = new Dictionary<string, string>
              {
                  { "token", token }
              };
            var data = new { token = token };
            var content = new FormUrlEncodedContent(values);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsync(Url, content);           
            //HttpResponseMessage response = await client.PostAsync(Url, content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TeacherLoginResponseDto>(responseString);
            }
            else
            {
                return new TeacherLoginResponseDto
                {
                    Status = false,
                    Message = "Invalid login credentials",
                    Response = null
                };
            }
        }
        */
        public static string Encrypt(string raw, string key)
        {
            var BLOCK_SIZE = 128;
            var KEY_SIZE = 192;
            var IV = key.Substring(0, 16);

            byte[] encrypted;
            int rem = raw.Length % 16;  // string length must be multiple of 16..otherwise we have to add extra padding
            if (rem != 0)
            {
                int f = 16 - rem;
                while (f > 0)
                {
                    raw = raw + " ";
                    f--;
                }
            }
            using (Aes aes = Aes.Create())
            {
                aes.BlockSize = BLOCK_SIZE;
                aes.KeySize = KEY_SIZE;
                ICryptoTransform encryptor = aes.CreateEncryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(IV));
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(raw);

                        }
                        encrypted = ms.ToArray();

                    }
                }

            }
            return Convert.ToBase64String(encrypted).ToString();
        }
        public async Task<ParentLoginRespone> ParentLogin(ParentLoginDto request)
        {
            var query = "GetParentLogin";
            var parameters = new DynamicParameters();
            parameters.Add("Username", request.username, DbType.String);
            parameters.Add("Password", request.password, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<ParentLoginRespone>(query, parameters, commandType: CommandType.StoredProcedure);
                return data.SingleOrDefault();
            }
        }
    }
}
