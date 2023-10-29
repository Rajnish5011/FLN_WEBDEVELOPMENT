using ASPNetCoreFLN_APIs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Contracts
{
   public interface ISectionRepository
    {
        public Task<IEnumerable<Section>>GetSection();

        public Task<IEnumerable<Section>> GetSectionBySchoolClass(int SchoolId, byte ClassId);
                
    }
}
