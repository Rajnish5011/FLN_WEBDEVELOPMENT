using ASPNetCoreFLN_APIs.Dto;
using ASPNetCoreFLN_APIs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface IAttendanceRepository
    {
        public Task<int> StudentAttendanceByTeacherSave(StudentAttendanceDto Attendance);

        public Task<IEnumerable<StudentAttendance>> GetStudentAttendanceByDateSchoolClassSection(string Attendance_Date, int SchoolId, byte ClassId, byte SectionId);

        public Task<IEnumerable<StudentsAttedanceByTeacher>> GetStudentAttendanceByTeacherId(int TeacherId);

        public Task<int> ClusterTeachersAttendanceByMentor(ClusterTeachersAttendanceDto Attendance);
        public Task<IEnumerable<StudentAttendanceBySrn>>GetStudentAttendanceBySrnDateWise (string Srn, string FromDate, string ToDate);
    }
}
