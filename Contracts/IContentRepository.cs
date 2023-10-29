using ASPNetCoreFLN_APIs.Dto.PeriodicAssessment;
using ASPNetCoreFLN_APIs.Entities;
using ASPNetCoreFLN_APIs.TeacherTrainingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Contracts
{
   public interface IContentRepository
    {
        public Task<IEnumerable<ContentRepositoryCategory>> GetContentRepositoryCategory();
        public Task<IEnumerable<AppContentRepository>> GetAppContentRepositoryByContentForRoleID(int ContentForRoleId, int currentPageNumber, int pageSize);
        public Task<int> AddContentForRoleSave(AddContentForRoleDto request);
    }
}
