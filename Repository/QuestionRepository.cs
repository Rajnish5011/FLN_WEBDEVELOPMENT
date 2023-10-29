using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Entities;
using ASPNetCoreFLN_APIs.Dto;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Linq.Expressions;

namespace ASPNetCoreFLN_APIs.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DapperContext _context;

        public QuestionRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MediaType>> GetMediaType(IsForOption IsForOption)
        {
            var procedureName = "GetMediaType";
            var parameters = new DynamicParameters();
            parameters.Add("IsForOption", Convert.ToBoolean(IsForOption), DbType.Boolean, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<MediaType>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        public async Task<IEnumerable<MediaType>> GetQuestionImage(IsForOption IsForOption)
        {
            var procedureName = "GetMediaType";
            var parameters = new DynamicParameters();
            parameters.Add("IsForOption", Convert.ToBoolean(IsForOption), DbType.Boolean, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<MediaType>
                  (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        /*
        public async Task<int> CreateQuestionToQuestionBankSave(Question request)
        {
            var ReturnVal = 0;
            var QuestionId = 0;
            var procedureName = "QuestionToQuestionBankSave";
            var parameters = new DynamicParameters();
            parameters.Add("Question_Instruction", request.Question_Instruction, DbType.String);
            parameters.Add("Question_Text", request.Question_Text, DbType.String);
            parameters.Add("Assessment_Type_Id", request.Assessment_Type_Id, DbType.Byte);
            parameters.Add("Question_Type_Id", request.Question_Type_Id, DbType.Byte);
            parameters.Add("Question_Type_Id", request.Question_Type_Id, DbType.Byte);
            parameters.Add("Competency_Id", request.Competancy_Id, DbType.Int16);
            parameters.Add("Week_Id", request.Week_Id, DbType.Byte);
            parameters.Add("Class_Id", request.Class_Id, DbType.Byte);
            parameters.Add("Subject_Id", request.Subject_Id, DbType.Byte);
            parameters.Add("Created_By", request.Created_By, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                using (var QuestionTransaction = connection.BeginTransaction())
                {
                    QuestionId = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: QuestionTransaction, commandType: CommandType.StoredProcedure);

                    if (QuestionId > 0)
                    {
                        var QuestionFlag = true;
                        foreach (var item in request.QuestionOption)
                        {
                            ReturnVal = 0;
                            procedureName = "QuestionOptionSave";
                            parameters = new DynamicParameters();
                            parameters.Add("Question_Id", QuestionId, DbType.Int32);
                            parameters.Add("Option_Text", item.Option_Text, DbType.String);
                            parameters.Add("Is_Correct", item.Is_Correct, DbType.Boolean);
                            parameters.Add("Created_By", request.Created_By, DbType.Int32);

                            using (var OptionTransaction = connection.BeginTransaction())
                            {
                                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: OptionTransaction, commandType: CommandType.StoredProcedure);
                                if (ReturnVal > 0)

                                    if (QuestionFlag == true)
                                    {
                                        QuestionTransaction.Commit();
                                        QuestionFlag = false;
                                    }
                                if (ReturnVal == 0)
                                {
                                    QuestionTransaction.Rollback();
                                    OptionTransaction.Rollback();
                                }
                            }
                        }
                    }
                    else
                        QuestionTransaction.Rollback();
                }
            }
            return ReturnVal;
        }
        
        public async Task<int> CreateImageQuestionToQuestionBankSave([FromForm] IFormFile QuestionImage, IFormFile OptionImage, string WebRootPath, ImageQuestion request)
        {
            var ReturnVal = 0;
            var QuestionId = 0;
            string MediaUrl = "";
            string UniqueFileName = "";
            string QuestionMediaType = "";
            string OptionMediaType = "";

            var parameter = new DynamicParameters();
            parameter.Add("MediaTypeId", request.Media_Type_Id, DbType.Byte);
            QuestionMediaType = GetMediaTypeByMediaTypeId("GetMediaTypeByMediaTypeId", parameter);

            if (QuestionMediaType.ToLower().Trim() == "image" || QuestionMediaType.ToLower().Trim() == "text/image")
            {
                if (QuestionImage.FileName.Length > 0)
                {
                    string QuestionfilePath = Path.GetTempFileName();
                    using (var stream = File.Create(QuestionfilePath))
                    {
                        await QuestionImage.CopyToAsync(stream);
                    }
                    var QuestionImagesFolder = WebRootPath + @"\QuestionImages\";
                    string FileExtention = Path.GetExtension(QuestionImage.FileName);
                    UniqueFileName = Guid.NewGuid().ToString() + FileExtention;
                    string QuestionImageFilePath = Path.Combine(QuestionImagesFolder, UniqueFileName);
                    MediaUrl = QuestionImagesFolder + UniqueFileName;

                    using (var fileStream = new FileStream(QuestionImageFilePath, FileMode.Create))
                    {
                        QuestionImage.CopyTo(fileStream);
                    }
                }
            }
            var procedureName = "QuestionToQuestionBankSave";
            var parameters = new DynamicParameters();
            parameters.Add("Question_Instruction", request.Question_Instruction, DbType.String);
            parameters.Add("Question_Text", request.Question_Text, DbType.String);
            parameters.Add("Assessment_Type_Id", request.Assessment_Type_Id, DbType.Byte);
            parameters.Add("Question_Type_Id", request.Question_Type_Id, DbType.Byte);
            parameters.Add("Media_Type_Id", request.Media_Type_Id, DbType.Byte);
            parameters.Add("Class_Id", request.Class_Id, DbType.Byte);
            parameters.Add("Subject_Id", request.Subject_Id, DbType.Byte);
            parameters.Add("Competency_Id", request.Competancy_Id, DbType.Int16);
            parameters.Add("Week_Id", request.Week_Id, DbType.Byte);
            parameters.Add("Media_Url", MediaUrl, DbType.String);
            parameters.Add("Created_By", request.Created_By, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var QuestionTransaction = connection.BeginTransaction())
                {
                    QuestionId = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: QuestionTransaction, commandType: CommandType.StoredProcedure);

                    if (QuestionId > 0)
                    {
                        var QuestionFlag = true;
                        byte MediaTypeId = 0;

                        if (request.ImageQuestionOption.Count > 0)
                        {
                            var OptionFlag = true;

                            foreach (var item in request.ImageQuestionOption)
                            {
                                if (OptionFlag == true)
                                {
                                    MediaTypeId = item.Media_Type_Id;
                                    OptionFlag = false;
                                    var Optionparam = new DynamicParameters();
                                    Optionparam.Add("MediaTypeId", MediaTypeId, DbType.Byte);
                                    OptionMediaType = GetMediaTypeByMediaTypeId("GetMediaTypeByMediaTypeId", Optionparam);
                                }
                                if (OptionMediaType.ToLower().Trim() == "image" || OptionMediaType.ToLower().Trim() == "text/image")
                                {
                                    if (OptionImage.FileName.Length > 0)
                                    {
                                        string OptionfilePath = Path.GetTempFileName();
                                        using (var stream = File.Create(OptionfilePath))
                                        {
                                            await OptionImage.CopyToAsync(stream);
                                        }
                                        var OptionImagesFolder = WebRootPath + @"\OptionImages\";
                                        string FileExtention = Path.GetExtension(OptionImage.FileName);
                                        UniqueFileName = Guid.NewGuid().ToString() + FileExtention;
                                        string OptionImageFilePath = Path.Combine(OptionImagesFolder, UniqueFileName);
                                        MediaUrl = OptionImagesFolder + UniqueFileName;

                                        using (var fileStream = new FileStream(OptionImageFilePath, FileMode.Create))
                                        {
                                            OptionImage.CopyTo(fileStream);
                                        }
                                    }
                                }

                                ReturnVal = 0;
                                procedureName = "QuestionOptionSave";
                                parameters = new DynamicParameters();
                                parameters.Add("Question_Id", QuestionId, DbType.Int32);
                                parameters.Add("Option_Text", item.Option_Text, DbType.String);
                                parameters.Add("Media_Type_Id", item.Media_Type_Id, DbType.Byte);
                                parameters.Add("Is_Correct", item.Is_Correct, DbType.Boolean);
                                parameters.Add("Media_Url", request.Week_Id, DbType.String);
                                parameters.Add("Created_By", request.Created_By, DbType.Int32);

                                using (var OptionTransaction = connection.BeginTransaction())
                                {
                                    ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: OptionTransaction, commandType: CommandType.StoredProcedure);
                                    if (ReturnVal > 0)
                                        if (QuestionFlag == true)
                                        {
                                            QuestionTransaction.Commit();
                                            QuestionFlag = false;
                                        }
                                    OptionTransaction.Commit();

                                    if (ReturnVal == 0)
                                    {
                                        QuestionTransaction.Rollback();
                                        OptionTransaction.Rollback();
                                    }
                                }
                            }
                        }
                        else
                            QuestionTransaction.Rollback();
                    }
                    else
                        QuestionTransaction.Rollback();
                }
            }
            return ReturnVal;
        }

        */
        public string GetMediaTypeByMediaTypeId(string procedureName, DynamicParameters parameters)
        {
            string MediaType = "";
            using (var connection = _context.CreateConnection())
             {
                connection.Open();
                var data = connection.ExecuteScalar(procedureName, parameters, commandType: CommandType.StoredProcedure);
                MediaType = data.ToString();
            }
            return MediaType;
        }

        public byte GetMediaTypeIDByQuestionId(int QuestionId)
        {
            byte MediaTypeID = 0;
            try
            {
                var procedureName = "GetMediaTypeIdByQuestionId";
                var parameters = new DynamicParameters();
                parameters.Add("@QuestionId", QuestionId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@ReturnMediaTypeID", dbType: DbType.Byte, direction: ParameterDirection.Output); // Add an output parameter
                using (var connection = _context.CreateConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
                        MediaTypeID = parameters.Get<byte>("@ReturnMediaTypeID");
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MediaTypeID;
        }

        public async Task<int> CreateQuestionToQuestionBankSave(Base64ImageQuestion request, string WebRootPath)
        {
            var ReturnVal = 0;
            var QuestionId = 0;
            string QuestionMediaUrl = "", OptionMediaUrl = "";
            string UniqueFileName = "";
            string QuestionMediaType = "";
            string OptionMediaType = "";

            var parameter = new DynamicParameters();
            parameter.Add("MediaTypeId", request.Media_Type_Id, DbType.Byte);
            QuestionMediaType = GetMediaTypeByMediaTypeId("GetMediaTypeByMediaTypeId", parameter);

            if (QuestionMediaType.ToLower().Trim() == "text" || QuestionMediaType.ToLower().Trim() == "text/image" || QuestionMediaType.ToLower().Trim() == "text/video" || QuestionMediaType.ToLower().Trim() == "text/audio")
            {
                if (request.Question_Text == "")
                    return -145;
            }
            if (QuestionMediaType.ToLower().Trim() == "video" || QuestionMediaType.ToLower().Trim() == "text/video" || QuestionMediaType.ToLower().Trim() == "text/audio")
            {
                if (request.Question_Media_Url == "")
                    return -24;
                else
                    QuestionMediaUrl = request.Question_Media_Url;
            }
            if (QuestionMediaType.ToLower().Trim() == "image" || QuestionMediaType.ToLower().Trim() == "text/image")
            {
                if (request.Base64QuestionImage.Length > 0)
                {
                    byte[] QuesionImagebytes = Convert.FromBase64String(request.Base64QuestionImage);
                    var QuestionImagesFolder = WebRootPath + @"\Uploads\QuestionImages\";
                    UniqueFileName = Guid.NewGuid().ToString() + ".png";
                    string QuestionImageFilePath = Path.Combine(QuestionImagesFolder, UniqueFileName);
                    QuestionMediaUrl = QuestionImagesFolder + UniqueFileName;
                    var QuestionImageLength = QuesionImagebytes.Length;
                    var MaxImageSize = (QuestionImageLength / 1024);

                    if (MaxImageSize <= 500)
                        File.WriteAllBytes(QuestionImageFilePath, QuesionImagebytes);
                    else
                        return -500;
                }
                else
                    return -1;
            }
            var procedureName = "QuestionToQuestionBankSave";
            var parameters = new DynamicParameters();
            parameters.Add("Question_Instruction", request.Question_Instruction, DbType.String);
            parameters.Add("Question_Text", request.Question_Text, DbType.String);
            parameters.Add("Assessment_Type_Id", request.Assessment_Type_Id, DbType.Byte);
            parameters.Add("Question_Type_Id", request.Question_Type_Id, DbType.Byte);
            parameters.Add("Media_Type_Id", request.Media_Type_Id, DbType.Byte);
            parameters.Add("Is_Draggable", request.Is_Draggable, DbType.Boolean);
            parameters.Add("Class_Id", request.Class_Id, DbType.Byte);
            parameters.Add("Subject_Id", request.Subject_Id, DbType.Byte);
            parameters.Add("Competency_Id", request.Competancy_Id, DbType.Int16);
            parameters.Add("Week_Id", request.Week_Id, DbType.Byte);
            parameters.Add("Media_Url", QuestionMediaUrl, DbType.String);
            parameters.Add("Marks", request.Marks, DbType.Int32);
            parameters.Add("Created_By", request.Created_By, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var QuestionTransaction = connection.BeginTransaction())
                {
                    QuestionId = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: QuestionTransaction, commandType: CommandType.StoredProcedure);

                    if (QuestionId > 0)
                    {
                        var QuestionFlag = true;
                        var OptionInsertFlag = true;

                        if (request.Base64ImageQuestionOption.Count > 0)
                        {
                            var OptionFlag = true;

                            foreach (var item in request.Base64ImageQuestionOption)
                            {
                                if (OptionFlag == true)
                                {
                                    OptionFlag = false;
                                    var Optionparam = new DynamicParameters();
                                    Optionparam.Add("MediaTypeId", request.Option_Media_Type_Id, DbType.Byte);
                                    OptionMediaType = GetMediaTypeByMediaTypeId("GetMediaTypeByMediaTypeId", Optionparam);
                                }
                                if (OptionMediaType.ToLower().Trim() == "text")
                                {
                                    if (item.Option_Text.Trim() == "")
                                    {
                                        QuestionTransaction.Rollback();
                                        return -144;
                                    }
                                }
                                if (OptionMediaType.ToLower().Trim() == "image" || OptionMediaType.ToLower().Trim() == "text/image")
                                {
                                    if (item.Base64OptionImage.Length > 0)
                                    {
                                        byte[] OptionImagebytes = Convert.FromBase64String(item.Base64OptionImage);
                                        var OptionImagesFolder = WebRootPath + @"\Uploads\OptionImages\";
                                        UniqueFileName = Guid.NewGuid().ToString() + ".png";
                                        string OptionImageFilePath = Path.Combine(OptionImagesFolder, UniqueFileName);
                                        OptionMediaUrl = OptionImagesFolder + UniqueFileName;
                                        var OptionImageLength = OptionImagebytes.Length;
                                        var MaxImageSize = (OptionImageLength / 1024);

                                        if (MaxImageSize <= 500)
                                            try
                                            {
                                                File.WriteAllBytes(OptionImageFilePath, OptionImagebytes);
                                            }
                                            catch
                                            {
                                                QuestionTransaction.Rollback();
                                                return -2;
                                            }
                                        else
                                        {
                                            QuestionTransaction.Rollback();
                                            return -499;
                                        }
                                    }
                                    else
                                    {
                                        QuestionTransaction.Rollback();
                                        return -2;
                                    }
                                }

                                ReturnVal = 0;
                                procedureName = "QuestionOptionSave";
                                parameters = new DynamicParameters();
                                parameters.Add("Question_Id", QuestionId, DbType.Int32);
                                parameters.Add("Option_Text", item.Option_Text, DbType.String);
                                parameters.Add("Media_Type_Id", request.Option_Media_Type_Id, DbType.Byte);
                                parameters.Add("Is_Correct", item.Is_Correct, DbType.Boolean);
                                parameters.Add("Media_Url", OptionMediaUrl, DbType.String);
                                parameters.Add("Created_By", request.Created_By, DbType.Int32);

                                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: QuestionTransaction, commandType: CommandType.StoredProcedure);

                                if (ReturnVal > 0)
                                {
                                    if (QuestionFlag == true)
                                    {
                                        QuestionFlag = false;
                                    }
                                }
                                if (ReturnVal == 0)
                                {
                                    OptionInsertFlag = false;
                                    QuestionTransaction.Rollback();
                                }
                            }
                        }
                        else
                            QuestionTransaction.Rollback();


                        if (OptionInsertFlag == true)
                            QuestionTransaction.Commit();
                        else
                            QuestionTransaction.Rollback();
                    }
                    else
                    {
                        ReturnVal = QuestionId;
                        QuestionTransaction.Rollback();
                    }
                }
            }
            return ReturnVal;
        }
        public async Task<int> UpdatePeriodicAssessmentQuestion(UpdatePeriodicQuestion request, string WebRootPath)
        {
            var ReturnVal = 0;
            var QuestionId = 0;
            string QuestionMediaUrl = "", OptionMediaUrl = "";
            string UniqueFileName = "";
            string QuestionMediaType = "";
            string OptionMediaType = "";

            //byte MediaTypeID = GetMediaTypeIDByQuestionId(request.Question_Id);
            var parameter = new DynamicParameters();
            parameter.Add("MediaTypeId",request.Media_Type_Id, DbType.Byte);
            QuestionMediaType = GetMediaTypeByMediaTypeId("GetMediaTypeByMediaTypeId", parameter);
            QuestionMediaUrl = request.Question_Media_Url;

            if (QuestionMediaType.ToLower().Trim() == "text" || QuestionMediaType.ToLower().Trim() == "text/image" || QuestionMediaType.ToLower().Trim() == "text/video" || QuestionMediaType.ToLower().Trim() == "text/audio")
            {
                if (request.Question_Text == "")
                    return -145;
            }
            if (QuestionMediaType.ToLower().Trim() == "video" || QuestionMediaType.ToLower().Trim() == "text/video" || QuestionMediaType.ToLower().Trim()== "text/audio")
            {
                if (request.Question_Media_Url == "")
                    return -24;
                else
                    QuestionMediaUrl = request.Question_Media_Url;
            }
            if (QuestionMediaType.ToLower().Trim() == "image" || QuestionMediaType.ToLower().Trim() == "text/image")
            {
                if (request.Base64QuestionImage.Length > 0)
                {
                    byte[] QuesionImagebytes = Convert.FromBase64String(request.Base64QuestionImage);
                    var QuestionImagesFolder = WebRootPath + @"\Uploads\QuestionImages\";
                    int startindex = QuestionMediaUrl.LastIndexOf('\\');
                    if (startindex > 0)
                    {
                        UniqueFileName = QuestionMediaUrl.Substring(startindex+1);
                    }
                    string QuestionImageFilePath = Path.Combine(QuestionImagesFolder, UniqueFileName);
                    QuestionMediaUrl = QuestionImagesFolder + UniqueFileName;
                    var QuestionImageLength = QuesionImagebytes.Length;
                    var MaxImageSize = (QuestionImageLength / 1024);

                    if (MaxImageSize <= 500)
                        File.WriteAllBytes(QuestionImageFilePath, QuesionImagebytes);
                    else
                        return -500;
                }
                else
                    return -1;
            }
            var procedureName = "PeriodicAssessmentQuestion_Update";
            var parameters = new DynamicParameters();
            
            parameters.Add("@Question_Id", request.Question_Id, DbType.Int32);
            parameters.Add("Question_Instruction", request.Question_Instruction, DbType.String);
            parameters.Add("Question_Text", request.Question_Text, DbType.String);            
            parameters.Add("@Media_Url", QuestionMediaUrl, DbType.String);
            parameters.Add("@Marks", request.Marks, DbType.Int32);
            parameters.Add("@Is_Active", request.Is_Active, DbType.Boolean);            
            parameters.Add("@Modified_By", request.Created_By, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var QuestionTransaction = connection.BeginTransaction())
                {
                    QuestionId = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: QuestionTransaction, commandType: CommandType.StoredProcedure);

                    if (QuestionId > 0)
                    {
                        var QuestionFlag = true;
                        var OptionInsertFlag = true;

                        if (request.QuestionOptions.Count > 0)
                        {
                            var OptionFlag = true;

                            foreach (var item in request.QuestionOptions)
                            {                                
                                if (OptionFlag == true)
                                {
                                    OptionFlag = false;
                                    var Optionparam = new DynamicParameters();
                                    Optionparam.Add("MediaTypeId", request.Option_Media_Type_Id, DbType.Byte);
                                    OptionMediaType = GetMediaTypeByMediaTypeId("GetMediaTypeByMediaTypeId", Optionparam);
                                }
                                if (OptionMediaType.ToLower().Trim() == "text")
                                {
                                    if (item.Option_Text.Trim() == "")
                                    {
                                        QuestionTransaction.Rollback();
                                        return -144;
                                    }
                                }
                                if (OptionMediaType.ToLower().Trim() == "image" || OptionMediaType.ToLower().Trim() == "text/image")
                                {
                                    if (item.Base64OptionImage.Length > 0)
                                    {
                                        byte[] OptionImagebytes = Convert.FromBase64String(item.Base64OptionImage);
                                        var OptionImagesFolder = WebRootPath + @"\Uploads\OptionImages\";

                                        int optionstartindex = QuestionMediaUrl.LastIndexOf('\\');
                                        if (optionstartindex > 0)
                                        {
                                            UniqueFileName = QuestionMediaUrl.Substring(optionstartindex + 1);
                                        }
                                        string OptionImageFilePath = Path.Combine(OptionImagesFolder, UniqueFileName);
                                        OptionMediaUrl = OptionImagesFolder + UniqueFileName;
                                        var OptionImageLength = OptionImagebytes.Length;
                                        var MaxImageSize = (OptionImageLength / 1024);

                                        if (MaxImageSize <= 500)
                                            try
                                            {
                                                File.WriteAllBytes(OptionImageFilePath, OptionImagebytes);
                                            }
                                            catch
                                            {
                                                QuestionTransaction.Rollback();
                                                return -2;
                                            }
                                        else
                                        {
                                            QuestionTransaction.Rollback();
                                            return -499;
                                        }
                                    }
                                    else
                                    {
                                        QuestionTransaction.Rollback();
                                        return -2;
                                    }
                                }

                                ReturnVal = 0;
                                procedureName = "QuestionOption_Update";
                                parameters = new DynamicParameters();
                                parameters.Add("Question_Option_Id", item.Question_Option_Id, DbType.Int32);
                                parameters.Add("Option_Text", item.Option_Text, DbType.String);
                                parameters.Add("Is_Correct", item.Is_Correct, DbType.Boolean);
                                parameters.Add("Media_Url", OptionMediaUrl, DbType.String);
                                parameters.Add("Created_By", request.Created_By, DbType.Int32);

                                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: QuestionTransaction, commandType: CommandType.StoredProcedure);

                                if (ReturnVal > 0)
                                {
                                    if (QuestionFlag == true)
                                    {
                                        QuestionFlag = false;
                                    }
                                }
                                if (ReturnVal == 0)
                                {
                                    OptionInsertFlag = false;
                                    QuestionTransaction.Rollback();
                                }
                            }
                        }
                        else
                            QuestionTransaction.Rollback();


                        if (OptionInsertFlag == true)
                            QuestionTransaction.Commit();
                        else
                            QuestionTransaction.Rollback();
                    }
                    else
                    {
                        ReturnVal = QuestionId;
                        QuestionTransaction.Rollback();
                    }
                }
            }
            return ReturnVal;
        }
        public async Task<int> DeletePeriodicAssessmentQuestions(int QuestionId)
        {
            var ReturnVal = 0;
            var procedureName = "DeletePeriodicAssessmentQuestionsByQuestionsId";

            var parameters = new DynamicParameters();
            parameters.Add("@QuestionId", QuestionId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);

            }
            return ReturnVal;
        }

        public async Task<int> CreateTeacherTrainingTestQuestion(TeacherTrainingtQuestionDto request, string WebRootPath)
        {
            var QuestionGroupId = 0;
            var ReturnVal = 0;
            var QuestionId = 0;
            string QuestionMediaUrl = "", OptionMediaUrl = "";
            string UniqueFileName = "";
            string QuestionMediaType = "";
            string OptionMediaType = "";

            var GroupInsertFlag = true;
            var QuestionInsertFlag = true;
            var OptionInsertFlag = true;

            var procedureName = "CreateTeacherTrainingQuestionsGroup";
            var parameters = new DynamicParameters();
            parameters.Add("@Question_Group_Name", request.Question_Group_Name, DbType.String);
            parameters.Add("@Schedule_Header_Test_Id", request.Schedule_Header_Test_Id, DbType.Int32);
            parameters.Add("@State_Admin_User_Id", request.State_Admin_User_Id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

                using (var QuestionTransaction = connection.BeginTransaction())
                {
                    QuestionGroupId = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: QuestionTransaction, commandType: CommandType.StoredProcedure);
                    
                    if (QuestionGroupId > 0)
                    {
                        foreach (var item in request.TestQuestions)
                        {
                            var parameter = new DynamicParameters();
                            parameter.Add("MediaTypeId", item.Media_Type_Id, DbType.Byte);
                            QuestionMediaType = GetMediaTypeByMediaTypeId("GetMediaTypeByMediaTypeId", parameter);

                            if (QuestionMediaType.ToLower().Trim() == "text" || QuestionMediaType.ToLower().Trim() == "text/image" || QuestionMediaType.ToLower().Trim() == "text/video")
                            {
                                if (item.Question_Text == "")
                                    return -145;
                            }
                            if (QuestionMediaType.ToLower().Trim() == "video" || QuestionMediaType.ToLower().Trim() == "text/video")
                            {
                                if (item.Question_Media_Url == "")
                                    return -24;
                                else
                                    QuestionMediaUrl = item.Question_Media_Url;
                            }
                            if (QuestionMediaType.ToLower().Trim() == "image" || QuestionMediaType.ToLower().Trim() == "text/image")
                            {
                                if (item.Base64QuestionImage.Length > 0)
                                {
                                    byte[] QuesionImagebytes = Convert.FromBase64String(item.Base64QuestionImage);
                                    var QuestionImagesFolder = WebRootPath + @"\Uploads\TeacherTraining\QuestionImages\";
                                    UniqueFileName = Guid.NewGuid().ToString() + ".png";
                                    string QuestionImageFilePath = Path.Combine(QuestionImagesFolder, UniqueFileName);
                                    QuestionMediaUrl = QuestionImagesFolder + UniqueFileName;
                                    var QuestionImageLength = QuesionImagebytes.Length;
                                    var MaxImageSize = (QuestionImageLength / 1024);

                                    if (MaxImageSize <= 500)
                                        File.WriteAllBytes(QuestionImageFilePath, QuesionImagebytes);
                                    else
                                        return -500;
                                }
                                else
                                    return -1;
                            }

                            procedureName = "CreateTeacherTrainingQuestions";
                            parameters = new DynamicParameters();
                            parameters.Add("@Question_Group_Id", QuestionGroupId, DbType.Int32);
                            parameters.Add("@Question_Text", item.Question_Text, DbType.String);
                            parameters.Add("@Question_Type_Id", item.Question_Type_Id, DbType.Byte);
                            parameters.Add("@Media_Type_Id", item.Media_Type_Id, DbType.Byte);
                            parameters.Add("@Option_Media_Type_Id", item.Option_Media_Type_Id, DbType.Byte);
                            parameters.Add("@Question_Marks", item.Question_Marks, DbType.Byte);
                            parameters.Add("@Media_Url", QuestionMediaUrl, DbType.String);
                            parameters.Add("@State_Admin_User_Id", request.State_Admin_User_Id, DbType.Int32);
                                                      
                            QuestionId = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: QuestionTransaction, commandType: CommandType.StoredProcedure);

                            if (QuestionId > 0)
                            {
                                if (item.QuestionOptions.Count > 0)
                                {
                                    foreach (var option in item.QuestionOptions)
                                    {
                                        var Optionparam = new DynamicParameters();
                                        Optionparam.Add("@MediaTypeId", item.Option_Media_Type_Id, DbType.Byte);
                                        OptionMediaType = GetMediaTypeByMediaTypeId("GetMediaTypeByMediaTypeId", Optionparam);

                                        if (OptionMediaType.ToLower().Trim() == "text")
                                        {
                                            if (option.Option_Text.Trim() == "")
                                            {
                                                QuestionTransaction.Rollback();
                                                return -144;
                                            }
                                        }
                                        if (OptionMediaType.ToLower().Trim() == "image" || OptionMediaType.ToLower().Trim() == "text/image")
                                        {
                                            if (option.Base64OptionImage.Length > 0)
                                            {
                                                byte[] OptionImagebytes = Convert.FromBase64String(option.Base64OptionImage);
                                                var OptionImagesFolder = WebRootPath + @"\Uploads\TeacherTraining\OptionImages\";
                                                UniqueFileName = Guid.NewGuid().ToString() + ".png";
                                                string OptionImageFilePath = Path.Combine(OptionImagesFolder, UniqueFileName);
                                                OptionMediaUrl = OptionImagesFolder + UniqueFileName;
                                                var OptionImageLength = OptionImagebytes.Length;
                                                var MaxImageSize = (OptionImageLength / 1024);

                                                if (MaxImageSize <= 500)
                                                    try
                                                    {
                                                        File.WriteAllBytes(OptionImageFilePath, OptionImagebytes);
                                                    }
                                                    catch
                                                    {
                                                        QuestionTransaction.Rollback();
                                                        return -2;
                                                    }
                                                else
                                                {
                                                    QuestionTransaction.Rollback();
                                                    return -499;
                                                }
                                            }
                                            else
                                            {
                                                QuestionTransaction.Rollback();
                                                return -2;
                                            }
                                        }
                                        ReturnVal = 0;
                                        procedureName = "TeacherTrainingQuestionOptionSave";
                                        parameters = new DynamicParameters();
                                        parameters.Add("@Question_Id", QuestionId, DbType.Int32);
                                        parameters.Add("@Option_Text", option.Option_Text, DbType.String);
                                        //parameters.Add("@Media_Type_Id", OptionMediaTypeId, DbType.Byte);
                                        parameters.Add("@Is_Correct", option.Is_Correct, DbType.Boolean);
                                        parameters.Add("@Option_Media_Url", OptionMediaUrl, DbType.String);
                                        parameters.Add("@State_Admin_User_Id", request.State_Admin_User_Id, DbType.Int32);

                                        ReturnVal = await connection.QuerySingleAsync<int>(procedureName, parameters, transaction: QuestionTransaction, commandType: CommandType.StoredProcedure);

                                        if (ReturnVal == 0)
                                        {
                                            OptionInsertFlag = false;                                            
                                        }                                       
                                    }
                                }
                                else
                                    OptionInsertFlag = false;
                            }
                            else
                                QuestionInsertFlag = false;
                        }
                    }
                    else
                    {
                        GroupInsertFlag = false;                     
                    }
                    if (GroupInsertFlag==true & QuestionInsertFlag == true & OptionInsertFlag == true)
                        QuestionTransaction.Commit();
                    else
                        QuestionTransaction.Rollback();

                }
                if (connection.State == ConnectionState.Open) connection.Close();
            }
            return ReturnVal;
        }
    }
}
