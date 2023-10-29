using ASPNetCoreFLN_APIs.Dto;
using ASPNetCoreFLN_APIs.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface ITeacherRepository
    {
        public Task<IEnumerable<BlockTeacherList>> GetBlockTeacherListByMentorID(int SchoolId);

        public Task<IEnumerable<TeacherListByClusterSchoolCode>> GetTeacherByClusterSchoolCode(string ClusterSchoolCode);

        public Task<IEnumerable<TeacherClassSection>> GetTeacherListBySchoolId(int SchoolId);

        public Task<IEnumerable<TeacherListBySchool>> GetTeacherBySchoolClassSection(int SchoolId, short ClassId, short SectionId);

        public Task<IEnumerable<MentorFeedBackToTeacher>> GetMentorFeedBackToTeacher(int TeacherId);

        public Task<TeacherProfile> GetTeacherProfileByTeacherId(int TeacherId);

        public Task<IEnumerable<ClusterTeacherMeetingScheduleForTeacher>> GetClusterTeacherMeetingScheduleByTeacherId(int TeacherID);

        public Task<TeacherByEmployeeCode> GetTeacherByEmployeeCode(string EmployeeCode);

        public Task<IEnumerable<BlockTeacherList>>GetBlockTeacherListByBlockID(int BlockID);
        public Task<IEnumerable<TeacherListBySrnNo>> GetTeacherListBySrnNo(string Srn_No);
    }
}
