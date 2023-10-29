using ASPNetCoreFLN_APIs.Dto.StudentTeacherCommunication;
using ASPNetCoreFLN_APIs.Dto.SurveyDTO;
using ASPNetCoreFLN_APIs.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface ISurveyRepository
    {
        Task<int> CreateSurveyForm(SurveyDto survey);
        Task<List<SurveyForm>> GetAllSurveyForm(string mentor_Id);
        Task<List<SurveyFormResponse>> GetSurveyForm(string mentor_Id);
        Task<int> SaveSurveyForm(SurveyAnswers answers);
    }
}
