
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Dto;
using ASPNetCoreFLN_APIs.Entities;
using Microsoft.Extensions.Options;
using Dapper;
using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using Microsoft.AspNetCore.Http;
using ASPNetCoreFLN_APIs.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.ComponentModel.DataAnnotations;

namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface IQuestionRepository
    {
        public Task<IEnumerable<MediaType>> GetMediaType(IsForOption IsForOption);

        public Task<IEnumerable<MediaType>> GetQuestionImage(IsForOption IsForOption);

        //public Task<int>
        //(Question question);

        //public Task<int> CreateImageQuestionToQuestionBankSave([FromForm] IFormFile QuestionImage, IFormFile OptionImage, string WebRootPath, ImageQuestion question);

        public Task<int> CreateQuestionToQuestionBankSave(Base64ImageQuestion question, string WebRootPath);

        public Task<int> UpdatePeriodicAssessmentQuestion(UpdatePeriodicQuestion question, string WebRootPath);

        public Task<int> DeletePeriodicAssessmentQuestions(int QuestionId);


        public Task<int> CreateTeacherTrainingTestQuestion(TeacherTrainingtQuestionDto question, string WebRootPath);

    }
}
