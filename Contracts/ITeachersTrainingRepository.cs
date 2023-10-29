using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Entities;
using ASPNetCoreFLN_APIs.TeacherTrainingDto;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using ASPNetCoreFLN_APIs.Dto;

namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface ITeachersTrainingRepository
    {					

        public Task<IEnumerable<TeacherTrainingSchedule>> GetTeacherTrainingScheduleByBlockAdmin(int Block_Admin_User_Id);

        public Task<IEnumerable<TeacherTraningQuestions>> GetTeacherTrainingTestQuestionsByStateAdmin(int ScheduleHeaderTestId);

        public Task<IEnumerable<TeacherTrainingSchedule>> GetTeacherTrainingScheduleByScheduleId(int TeacherTrainingScheduleId, int BlockAdminUserId);

        public  Task<IEnumerable<BlockTeacherList>> GetBlockTeacherListForTrainingByBlockID(int BlockID);

        public Task<int> DeleteTrainingScheduleHeaderTest(int TrainingScheduleHeaderId, int StateAdminUserId, byte Subject_Id);

        public Task<int> UpdateTeacherTrainingQuestionsGroupByStateAdmin(UpdateTeacherTrainingQuestionsGroup request);

        public Task<int> UpdateTeacherTrainingQuestionByStateAdmin(UpdateTeacherTrainingQuestions request);

        public Task<int> UpdateTeacherTrainingQuestionOptionByStateAdmin(UpdateTeacherTrainingQuestionOption request);

        public Task<int> DeleteTeacherTrainingQuestionsGroupByStateAdmin(int QuestionGroupId, int StateAdminUserId);

        public Task<int> DeleteTeacherTrainingQuestionByStateAdmin(int QuestionId, int StateAdminUserId);

        public Task<int> DeleteTeacherTrainingQuestionOption(int QuestionOptionId, int StateAdminUserId);

        public Task<int> DeleteTeacherTrainingSchedule(int TeacherTrainingScheduleId, int Block_Admin_User_Id);

        public Task<int> CreateTeacherTrainingSchedule(CreateTeacherTrainingScheduleDto request);

        public Task<int> CreateTrainingScheduleTeacherSelection(TrainingScheduleTeacherSelectionDto request);

        public Task<int> DeleteTeacherFromScheduledTraining(int Training_Schedule_Teacher_Id, int Block_Admin_User_Id);

        public  Task<int> UpdateTeacherTrainingScheduleHeader(UpdateTeacherTrainingScheduleHeaderDto request);

        public Task<int> UpdateBlockTeacherTrainingSchedule(UpdateTeacherTrainingScheduleDto request);

        public Task<int> DeleteTeacherTrainingScheduleTest(int Training_Schedule_Teacher_Id, int Block_Admin_User_Id, byte Subject_Id);

        public Task<int> ChangeTeacherTrainingTestStatus(int Teacher_Training_Schedule_Test_Id, bool Is_Active);

        public Task<IEnumerable<GetSelectedTeachers>> GetSelectedTeachersForTeacherTrainingByBlockAdmin(int BlockAdminUserId, int TeacherTrainingScheduleId);

        public Task<int> CreateTeacherAttendanceForScheduledTraining(TeacherTrainingAttendanceDto request);

        public Task<IEnumerable<TrainingScheduleAttendance>> GetTeacherTrainingScheduleAttendance(int TeacherTrainingScheduleId, int BlockAdminUserId);

        public Task<IEnumerable<TrainingScheduleTeachersAttendance>> GetTrainingScheduleTeachersAttendance(int TrainingScheduleAttendanceId, int BlockAdminUserId);

        public Task<IEnumerable<TrainingScheduleHeader>> GetTrainingScheduleHeader(bool Is_Current_Header);
        public Task<IEnumerable<TrainingScheduleHeader>> GetTrainingScheduleHeaderByScheduleHeaderId(int Training_Schedule_Header_Id);

        public Task<IEnumerable<TeacherTrainingSchedule>> GetTeacherTrainingScheduleByTeacherId(int Teacher_Id, bool Is_Upcoming);

        public Task<IEnumerable<TeacherTrainingSchedule>> GetTeacherTrainingScheduleDetail(int TeacherTrainingScheduleId,int TrainingScheduleTeacherId);

        public Task<IEnumerable<TeacherTrainingTestList>> GetTeacherTrainingHeaderTestList();

        public Task<int> CreateTeachersTrainingScheduleHeader(TeachersTrainingScheduleHeaderDto request);

        public Task<IEnumerable<TeacherTraningQuestions>> GetTeacherTrainingTestQuestions(int TeacherTrainingScheduleTestId);

        public string GetBase64StringImageByPath(string mediatype, string webRootPath, string ImagePath);

        public Task<int> TeacherTrainingAssessmentQuestionsSave(TrainingScheduleTeacherAssessmentDto request);

        public Task<int> UpdateLatestTeacherAttendance(UpdateTeacherTrainingAttendanceDto request);
                
    }
}