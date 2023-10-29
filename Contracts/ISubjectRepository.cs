using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Entities;


namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface ISubjectRepository
    {
        public Task<IEnumerable<Subject>> GetSubject();

        public Task<IEnumerable<SubjectCompetancy>> GetSubjectCompetancy(byte SubjectId, short SubjectTopicId);

        public Task<IEnumerable<SubjectTopic>> GetSubjectTopicBySubjectId(byte id);

        public Task<IEnumerable<SubjectCompetancyDetail>> GetCompetancyByClassSubjectId( byte ClassId,byte SubjectId);

        public Task<IEnumerable<SpotSubjectCompetancy>> GetSpotCompetancyByMonthClassSubjectId(DateTime GetMonthFromDate, byte ClassId, byte SubjectId);
    }
}