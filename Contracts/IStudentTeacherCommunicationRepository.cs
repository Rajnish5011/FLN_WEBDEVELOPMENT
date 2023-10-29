using ASPNetCoreFLN_APIs.Dto;
using ASPNetCoreFLN_APIs.Dto.StudentTeacherCommunication;
using ASPNetCoreFLN_APIs.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface IStudentTeacherCommunicationRepository
    {
        Task<int> StudentTeacherCommunication(StudentTeacherCommunicationDto communicationDto);
        Task<PaginatedResult<StudentTeacherMessagesDto>> GetStudentTeacherChats(int sender_Id, int receiver_Id, int pageNumber, int pageSize);
        Task<List<ChatTeacherResponse>> GetTecherListToCommunication(int class_Id, int section_Id, int school_Id);
        Task<List<ChatStudentResponse>> GetStudentListToCommunication(int class_Id, int section_Id, int school_Id);
    }
}
