using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Entities;
using ASPNetCoreFLN_APIs.Dto.Mentor;
using ASPNetCoreFLN_APIs.Dto.Grievance;

namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface IManageGrievanceRepository
    {						
        public Task<IEnumerable<GrievanceCategories>> GetGrievanceCategoriesByGrievanceForRoleID(int GrievanceForRoleID);
        
        public Task<IEnumerable<GrievanceTicketList>> GetGrievanceTicketsRaisedByUsers(bool IsClosed);

        public Task<IEnumerable<GrievanceTicketListByUser>> GetGrievanceTicketListDetailRaisedByUser(int UserId, byte RoleId);

        public Task<IEnumerable<GrievanceTicketDetails>> GetGrievanceDetailsByTicketID(int TicketID);

        public Task<int> CreateGrievanceTicketByUser(CreateGrievanceDto request);

        //public Task<int> CreateNewGrievanceCategory(CreateGrievanceCategoryDto request);

        public Task<int>GrievanceTicketReplyByHelpDeskUser(int TicketId, int HelpDeskUserId, string GrievanceReply);

    }
}