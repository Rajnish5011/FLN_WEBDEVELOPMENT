using ASPNetCoreFLN_APIs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface IMastersRepository
    {
        public Task<IEnumerable<Districts>> GetDistrictByStateId(short id);

        public Task<IEnumerable<Blocks>> GetBlocksByDistrictId(short id);

        public Task<IEnumerable<AssessmentType>> GetAssessmentType();

        public Task<IEnumerable<QuestionType>> GetQuestionType();

        public Task<IEnumerable<Class>> GetClass();

        public Task<IEnumerable<Class>> GetClassBySchoolId(int SchoolId);

        public Task<IEnumerable<ClassSection>> GetClassSectionBySchoolId(int SchoolId);

        public Task<IEnumerable<Months>> GetMonths();

        public Task<IEnumerable<Weeks>> GetWeeks();

        //public Task<IEnumerable<SubjectCompetancyDetail>> GetCompetancyBySubjectClassId(byte SubjectId, byte ClassId);
        public Task<IEnumerable<Roles>> GetAllRoles();

        public Task<IEnumerable<Designations>> GetDesignationByRole(short RoleId);

    }
}
