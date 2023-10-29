using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Entities;
using System.Linq;
using ASPNetCoreFLN_APIs.TeacherTrainingDto;
using Microsoft.AspNetCore.Hosting;
using ASPNetCoreFLN_APIs.Dto;
using Azure.Core;

namespace ASPNetFLN_TestAPI.Controllers
{
    [Route("api/TeachersTraining")]
    [ApiController]

    public class TeachersTrainingController : ControllerBase
    {
        private readonly ITeachersTrainingRepository _TeachersTrainingRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _webRootPath;
        public TeachersTrainingController(ITeachersTrainingRepository TeachersTrainingRepo, IWebHostEnvironment webHostEnvironment)
        {
            _TeachersTrainingRepo = TeachersTrainingRepo;
            _webHostEnvironment = webHostEnvironment;
            _webRootPath = _webHostEnvironment.WebRootPath;
        }
        [HttpGet("StateAdmin/GetTeacherTrainingHeaderTestList")]
        public async Task<IActionResult> GetTeacherTrainingHeaderTestList()
        {
            try
            {
                var data = await _TeachersTrainingRepo.GetTeacherTrainingHeaderTestList();

                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Teacher Not Found!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Teacher Found Successfully ", response = data });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });
            }
        }

        [HttpGet("StateAdmin/GetTeacherTrainingTestQuestionsByStateAdmin/{ScheduleHeaderTestId}")]
        public async Task<IActionResult> GetTeacherTrainingTestQuestionsByStateAdmin(int ScheduleHeaderTestId)
        {
            try
            {
                var response = (await _TeachersTrainingRepo.GetTeacherTrainingTestQuestionsByStateAdmin(ScheduleHeaderTestId)).ToList();
                var data = response.GroupBy(item => item.Question_Group_Id).Select(g => new
                {
                    Question_Group_Id = g.Key,
                    Question_Group_Name = g.Select(x => x.Question_Group_Name.ToString()).FirstOrDefault(),

                    Questions = g.GroupBy(Qitem => Qitem.Question_Id).Select(Q => new
                    {
                        Question_Id = Q.Key,
                        Base64QuestionImage = _TeachersTrainingRepo.GetBase64StringImageByPath(Q.Select(x => x.Question_Media_Type).FirstOrDefault().ToString(), _webRootPath + @"\Uploads\QuestionImages\", Q.Select(x => x.Question_Media_Url).FirstOrDefault().ToString().Trim()),
                        Question_Media_Url = Q.Select(hx => hx.Question_Media_Url.ToString()).FirstOrDefault().ToString(),
                        Question_Type = Q.Select(hx => hx.Question_Type).FirstOrDefault(),
                        Question_Text = Q.Select(hx => hx.Question_Text).FirstOrDefault(),
                        Question_Media_Type = Q.Select(hx => hx.Question_Media_Type).FirstOrDefault(),

                        QuestionsOptions = Q.Select(optitem => new
                        {
                            Base64OptionImage = _TeachersTrainingRepo.GetBase64StringImageByPath(Q.Select(hx => hx.Option_Media_Type).FirstOrDefault().ToString(), _webRootPath + @"\Uploads\OptionImages\", optitem.Option_Media_Url.ToString().Trim()),
                            Question_Option_Id = optitem.Question_Option_Id,
                            Option_Media_Type_Id = optitem.Option_Media_Type_Id,
                            Option_Media_Type = optitem.Option_Media_Type,
                            Option_Text = optitem.Option_Text,
                            Option_Media_Url = optitem.Option_Media_Url.ToString(),
                            Is_Correct = optitem.Is_Correct
                        }).ToList()
                    }).ToList()
                });

                if (response.Count == 0)
                    return StatusCode(200, new { status = false, message = "Questions Not Found!", response = response });

                else
                    return StatusCode(200, new
                    {
                        status = true,
                        message = "Questions Found Successfully.",
                        response = data
                    });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }

        }

        [HttpPost("StateAdmin/CreateTeacherTrainingScheduleHeader")]
        public async Task<IActionResult> CreateTeachersTrainingScheduleHeader(TeachersTrainingScheduleHeaderDto request)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.CreateTeachersTrainingScheduleHeader(request);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Training Schedule Header created Successfully", response = ReturnVal });
                else if (ReturnVal == -1)
                    return StatusCode(200, new { status = false, message = "Cannot create schedule Header! Schedule End Date limit exceeded.", response = 200 });

                else if (ReturnVal == -2)
                    return StatusCode(200, new { status = false, message = "Cannot create schedule! Training Header start date and end date must be under date range.", response = 200 });

                else if (ReturnVal == -3)
                    return StatusCode(200, new { status = true, message = "Some Test was already exist! Rest all test created successfully.", response = 200 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }

        [HttpPut("StateAdmin/UpdateTeacherTrainingScheduleHeader")]
        public async Task<IActionResult> UpdateTeacherTrainingScheduleHeader(UpdateTeacherTrainingScheduleHeaderDto TrainingScheduleHeader)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.UpdateTeacherTrainingScheduleHeader(TrainingScheduleHeader);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Schedule Header updated successfully!", response = ReturnVal });

                else if (ReturnVal == -1)
                    return StatusCode(200, new { status = false, message = "Please Decrease your Training Header Start Date and try again.", response = ReturnVal });

                else if (ReturnVal == -2)
                    return StatusCode(200, new { status = false, message = "Please Increase your Training Header End Date and try again.", response = ReturnVal });

                else if (ReturnVal == -3)
                    return StatusCode(200, new { status = false, message = "Training Header Start Date cannot be greater than or equal to End Date.", response = ReturnVal });

                else if (ReturnVal == -4)
                    return StatusCode(200, new { status = false, message = "Schedule Header Title cannot be empty!", response = ReturnVal });

                else if (ReturnVal == -5)
                    return StatusCode(200, new { status = false, message = "You are not authorised to update Schedule Header.", response = ReturnVal });

                else if (ReturnVal == -6)
                    return StatusCode(200, new { status = false, message = "Please select Test which already not exists.", response = ReturnVal });

                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }

        [HttpDelete("StateAdmin/DeleteTrainingScheduleHeaderTest/{TrainingScheduleHeaderId}/{StateAdminUserId}/{SubjectId}")]
        public async Task<IActionResult> DeleteTrainingScheduleHeaderTest(int TrainingScheduleHeaderId, int StateAdminUserId, byte SubjectId)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.DeleteTrainingScheduleHeaderTest(TrainingScheduleHeaderId, StateAdminUserId, SubjectId);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Header Test Deleted Successfully.", response = ReturnVal });

                else if (ReturnVal == -3)
                    return StatusCode(200, new { status = false, message = "Cannot delete Header Test! This test is being used by Block Admin(s) already!", response = 200 });

                else if (ReturnVal == -4)
                    return StatusCode(200, new { status = false, message = "Cannot delete Header Test! Invalid Parameter(s) passed!", response = 200 });

                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }

        [HttpPut("StateAdmin/UpdateTeacherTrainingQuestionsGroup")]
        public async Task<IActionResult> UpdateTeacherTrainingQuestionsGroupByStateAdmin(UpdateTeacherTrainingQuestionsGroup request)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.UpdateTeacherTrainingQuestionsGroupByStateAdmin(request);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Group name Updated Successfully.", response = ReturnVal });

                else if (ReturnVal == -1)
                    return StatusCode(200, new { status = false, message = "You are not authorised to Updated this Group!", response = 400 });

                else if (ReturnVal == -2)
                    return StatusCode(200, new { status = false, message = "Cannot Updated Group! Assessment start and Some Questions are answered of this Group!", response = 200 });
                
                else if (ReturnVal == -3)
                    return StatusCode(200, new { status = false, message = "Group Name cannot be empty!!", response = 200 });

                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });
            }
        }

        [HttpPut("StateAdmin/UpdateTeacherTrainingQuestion")]
        public async Task<IActionResult> UpdateTeacherTrainingQuestionByStateAdmin(UpdateTeacherTrainingQuestions request)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.UpdateTeacherTrainingQuestionByStateAdmin(request);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Question Updated Successfully.", response = ReturnVal });

                else if (ReturnVal == -1)
                    return StatusCode(200, new { status = false, message = "You are not authorised to update this Question!", response = 400 });

                else if (ReturnVal == -2)
                    return StatusCode(200, new { status = false, message = "Cannot update Question! Test started or Question is answered by some teacher!", response = 200 });
                else if (ReturnVal == -3)
                    return StatusCode(200, new { status = false, message = "Please enter correct Media Type Id!", response = 200 });
                else if (ReturnVal == -4)
                    return StatusCode(200, new { status = false, message = "Question Text cannot be empty!", response = 200 });

                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }

        [HttpPut("StateAdmin/UpdateTeacherTrainingQuestionOption")]
        public async Task<IActionResult> UpdateTeacherTrainingQuestionOptionByStateAdmin(UpdateTeacherTrainingQuestionOption request)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.UpdateTeacherTrainingQuestionOptionByStateAdmin(request);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Option Updated Successfully.", response = ReturnVal });

                else if (ReturnVal == -1)
                    return StatusCode(200, new { status = false, message = "You are not authorised to update this Option!", response = 400 });

                else if (ReturnVal == -2)
                    return StatusCode(200, new { status = false, message = "Cannot update any Option! Test started already!", response = 200 });
                else if (ReturnVal == -3)
                    return StatusCode(200, new { status = false, message = "Please enter Option Text!", response = 200 });
                
                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }
        [HttpDelete("StateAdmin/DeleteTeacherTrainingQuestionsGroup/{QuestionGroupId}/{StateAdminUserId}")]
        public async Task<IActionResult> DeleteTeacherTrainingQuestionsGroupByStateAdmin(int QuestionGroupId, int StateAdminUserId)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.DeleteTeacherTrainingQuestionsGroupByStateAdmin(QuestionGroupId, StateAdminUserId);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Group of Questions Deleted Successfully.", response = ReturnVal });

                else if (ReturnVal == -1)
                    return StatusCode(200, new { status = false, message = "You are not authorised to delete this Questions Group!", response = 400 });

                else if (ReturnVal == -2)
                    return StatusCode(200, new { status = false, message = "Cannot delete Group! Some Questions are answered of this Group!", response = 200 });               
               
                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }

        [HttpDelete("StateAdmin/DeleteTeacherTrainingQuestion/{QuestionId}/{StateAdminUserId}")]
        public async Task<IActionResult> DeleteTeacherTrainingQuestionByStateAdmin(int QuestionId, int StateAdminUserId)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.DeleteTeacherTrainingQuestionByStateAdmin(QuestionId, StateAdminUserId);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Question Deleted Successfully.", response = ReturnVal });

                else if (ReturnVal == -1)
                    return StatusCode(200, new { status = false, message = "You are not authorised to delete this Question!", response = 400 });

                else if (ReturnVal == -2)
                    return StatusCode(200, new { status = false, message = "Cannot delete Question! This Question is answered by some teacher!", response = 200 });

                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }

        [HttpDelete("StateAdmin/DeleteTeacherTrainingQuestionOption/{QuestionOptionId}/{StateAdminUserId}")]
        public async Task<IActionResult> DeleteTeacherTrainingQuestionOption(int QuestionOptionId, int StateAdminUserId)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.DeleteTeacherTrainingQuestionOption(QuestionOptionId, StateAdminUserId);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Option Deleted Successfully.", response = ReturnVal });

                else if (ReturnVal == -1)
                    return StatusCode(200, new { status = false, message = "You are not authorised to delete this Option!", response = 200 });

                else if (ReturnVal == -2)
                    return StatusCode(200, new { status = false, message = "Cannot delete Option! This Option is used by teacher in a test!", response = 200 });

                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }

        [HttpGet("BlockAdmin/GetTrainingScheduleHeaderDetail")]
        public async Task<IActionResult> GetTrainingScheduleHeader(bool Is_Current_Header)
        {
            try
            {
                var result = await _TeachersTrainingRepo.GetTrainingScheduleHeader(Is_Current_Header);
                var data = result.GroupBy(item => item.Training_Schedule_Header_Id)
                           .Select(selector: x => new
                           {
                               Training_Schedule_Header_Id = x.Key,
                               Schedule_Header_Title = x.Select(x => x.Schedule_Header_Title).First(),
                               Start_Date = x.Select(x => x.Start_Date).First(),
                               End_Date = x.Select(x => x.End_Date).First(),
                               Training_Description = x.Select(x => x.Training_Description).First(),

                               Tests = x.Select(y => new
                               {
                                   Schedule_Header_Test_Id = y.Schedule_Header_Test_Id,
                                   Test_Name = y.Test_Name,
                                   Test_Type = y.Test_Type,
                                   Subject_Id = y.Subject_Id,
                                   Subject_Name = y.Subject_Name                                   
                               })
                           });
                if (result.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Schedule Header Not Found!", response = data });
                else
                {
                    return StatusCode(200, new { status = true, message = "Schedule Header Found Successfully.", response = data });
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
        [HttpGet("BlockAdmin/GetTrainingScheduleHeaderDetailByScheduleHeaderId/{Training_Schedule_Header_Id}")]
        public async Task<IActionResult> GetTrainingScheduleHeaderByScheduleHeaderId(int Training_Schedule_Header_Id)
        {
            try
            {
                var result = await _TeachersTrainingRepo.GetTrainingScheduleHeaderByScheduleHeaderId(Training_Schedule_Header_Id);
                var data = result.GroupBy(item => item.Training_Schedule_Header_Id)
                           .Select(selector: x => new
                           {
                               Training_Schedule_Header_Id = x.Key,
                               Schedule_Header_Title = x.Select(x => x.Schedule_Header_Title).First(),
                               Start_Date = x.Select(x => x.Start_Date).First(),
                               End_Date = x.Select(x => x.End_Date).First(),
                               Training_Description = x.Select(x => x.Training_Description).First(),

                               Tests = x.Select(y => new
                               {
                                   Schedule_Header_Test_Id = y.Schedule_Header_Test_Id,
                                   Test_Name = y.Test_Name,
                                   Test_Type = y.Test_Type,
                                   Subject_Id = y.Subject_Id,
                                   Subject_Name = y.Subject_Name
                               })
                           });
                if (result.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Schedule Header Not Found!", response = data });
                else
                {
                    return StatusCode(200, new { status = true, message = "Schedule Header Found Successfully.", response = data });
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
        [HttpGet("BlockAdmin/GetBlockTeacherListForTraining/{BlockID}")]
        public async Task<IActionResult> GetBlockTeacherListForTrainingByBlockID(int BlockID)
        {
            try
            {
                var data = await _TeachersTrainingRepo.GetBlockTeacherListForTrainingByBlockID(BlockID);

                if (data.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Teacher Not Found!", response = data });

                else
                    return StatusCode(200, new { status = true, message = "Teacher Found Successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpPost("BlockAdmin/CreateBlockTeachersTrainingSchedule")]
        public async Task<IActionResult> CreateBlockTeacherTrainingSchedule(CreateTeacherTrainingScheduleDto TeacherTrainingSchedule)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.CreateTeacherTrainingSchedule(TeacherTrainingSchedule);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Training Schedule created Successfully", response = ReturnVal });
                else if (ReturnVal == -1)
                    return StatusCode(200, new { status = false, message = "Cannot create schedule! Schedule End Date limit exceeded.", response = ReturnVal });

                else if (ReturnVal == -2)
                    return StatusCode(200, new { status = false, message = "Cannot create schedule! Training start date and end date must be under date range set by state.", response = ReturnVal });

                else if (ReturnVal == -3)
                    return StatusCode(200, new { status = false, message = "Cannot create schedule! Training start date cannot be greater than end date.", response = ReturnVal });

                else if (ReturnVal == -4)
                    return StatusCode(200, new { status = false, message = "Please insert valid test and try again.", response = ReturnVal });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }

        [HttpPost("BlockAdmin/AddTeachersForScheduledTraining")]
        public async Task<IActionResult> CreateTrainingScheduleTeacherSelection(TrainingScheduleTeacherSelectionDto request)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.CreateTrainingScheduleTeacherSelection(request);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Teacher(s) added Successfully", response = 1 });

                else if (ReturnVal == -1)
                    return StatusCode(200, new { status = false, message = "Cannot insert, Teacher already added for this scheduled training!", response = 200 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });
            }
        }

        [HttpDelete("BlockAdmin/DeleteTeacherFromScheduledTraining/{Training_Schedule_Teacher_Id}/{Block_Admin_User_Id}")]
        public async Task<IActionResult> DeleteTeacherFromTrainingSchedule(int Training_Schedule_Teacher_Id, int Block_Admin_User_Id)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.DeleteTeacherFromScheduledTraining(Training_Schedule_Teacher_Id, Block_Admin_User_Id);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Teacher Deleted Successfully", response = ReturnVal });

                else if (ReturnVal == -3)
                    return StatusCode(200, new { status = false, message = "Cannot delete teacher! Attendance already done for this training!", response = 200 });
                
                else if (ReturnVal == -4)
                    return StatusCode(200, new { status = false, message = "Cannot delete teacher! Invalid Parameter(s) passed!", response = 200 });
                
                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }

        [HttpPut("BlockAdmin/UpdateTeachersTrainingSchedule")]
        public async Task<IActionResult> UpdateBlockTeacherTrainingSchedule(UpdateTeacherTrainingScheduleDto TeacherTrainingSchedule)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.UpdateBlockTeacherTrainingSchedule(TeacherTrainingSchedule);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Training Schedule updated successfully!", response = ReturnVal });

                else if (ReturnVal == -1)
                    return StatusCode(200, new { status = false, message = "Cannot update! Schedule End Date limit exceeded.", response = ReturnVal });

                else if (ReturnVal == -2)
                    return StatusCode(200, new { status = false, message = "Cannot update! Training Start Date and End Date must be under date range set by state Admin.", response = ReturnVal });

                else if (ReturnVal == -3)
                    return StatusCode(200, new { status = false, message = "Cannot update! Training Start Date cannot be greater than End Date.", response = ReturnVal });

                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }
        [HttpDelete("BlockAdmin/DeleteTeacherTrainingScheduleTest/{TeacherTrainingScheduleId}/{BlockAdminUserId}/{SubjectId}")]
        public async Task<IActionResult> DeleteTeacherTrainingScheduleTest(int TeacherTrainingScheduleId, int BlockAdminUserId, byte SubjectId)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.DeleteTeacherTrainingScheduleTest(TeacherTrainingScheduleId, BlockAdminUserId, SubjectId);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Test Deleted Successfully.", response = ReturnVal });

                else if (ReturnVal == -3)
                    return StatusCode(200, new { status = false, message = "Cannot delete Test! Because Assessment already done for this test!", response = 200 });

                else if (ReturnVal == -4)
                    return StatusCode(200, new { status = false, message = "Cannot delete Test! Invalid Parameter(s) passed!", response = 200 });

                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }

        [HttpDelete("StateAdmin/DeleteTeacherTrainingScheduleByBlockAdmin/{TeacherTrainingScheduleId}/{BlockAdminUserId}")]
        public async Task<IActionResult> DeleteTeacherTrainingSchedule(int TeacherTrainingScheduleId, int BlockAdminUserId)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.DeleteTeacherTrainingSchedule(TeacherTrainingScheduleId, BlockAdminUserId);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Training Schedule Deleted Successfully.", response = ReturnVal });

                else if (ReturnVal == -3)
                    return StatusCode(200, new { status = false, message = "Cannot delete Training Schedule! Test is started already!", response = 200 });

                else if (ReturnVal == -4)
                    return StatusCode(200, new { status = false, message = "You are not authorise to delete this schedule!!", response = 200 });

                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }
        [HttpPut("BlockAdmin/ChangeTeacherTrainingTestStatus/{Teacher_Training_Schedule_Test_Id}/{Is_Active}")]
        public async Task<IActionResult> ChangeTeacherTrainingTestStatus(int Teacher_Training_Schedule_Test_Id, bool Is_Active)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.ChangeTeacherTrainingTestStatus(Teacher_Training_Schedule_Test_Id, Is_Active);
                if (ReturnVal == 1)
                    return StatusCode(200, new { status = true, message = "Test Status changed to Active successfully!", response = ReturnVal });

                else if (ReturnVal == -1)
                    return StatusCode(200, new { status = false, message = "Test Status changed to In-Active!", response = ReturnVal });

                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }

        [HttpGet("BlockAdmin/GetTeacherTrainingSchedule/{Block_Admin_User_Id}")]
        public async Task<IActionResult> GetTeacherTrainingScheduleByBlockAdmin(int Block_Admin_User_Id)
        {
            try
            {
                var result = await _TeachersTrainingRepo.GetTeacherTrainingScheduleByBlockAdmin(Block_Admin_User_Id);
                var data = result.GroupBy(item => item.Teacher_Training_Schedule_Id)
                           .Select(selector: x => new
                           {
                               Teacher_Training_Schedule_Id = x.Key,
                               Training_Title = x.Select(x => x.Training_Title).First(),
                               Training_Start_Date = x.Select(x => x.Training_Start_Date).First(),
                               Training_End_Date = x.Select(x => x.Training_End_Date).First(),
                               Training_Start_Time = x.Select(x => x.Training_Start_Time).First(),
                               Training_End_Time = x.Select(x => x.Training_End_Time).First(),
                               Training_Place = x.Select(x => x.Training_Place).First(),
                               Is_Training_End = x.Select(x => x.Is_Training_End).First(),
                               Block_Name = x.Select(x => x.Block_Name).First(),
                               Training_Description = x.Select(x => x.Training_Description).First(),

                               Tests = x.Select(y => new
                               {
                                   Teacher_Training_Schedule_Test_Id = y.Teacher_Training_Schedule_Test_Id,
                                   Test_Name = y.Test_Name,
                                   Test_Type = y.Test_Type,
                                   Subject_Id = y.Subject_Id,
                                   Subject_Name = y.Subject_Name,
                                   Is_Test_Done = y.Is_Test_Done,
                                   Is_Active = y.Is_Active
                               })
                           });
                if (result.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Training Schedule Not Found!", response = data });
                else
                {
                    return StatusCode(200, new { status = true, message = "Training Schedule Found Successfully.", response = data });
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("BlockAdmin/GetTeacherTrainingScheduleByScheduleId/{TeacherTrainingScheduleId}/{BlockAdminUserId}")]
        public async Task<IActionResult> GetTeacherTrainingScheduleByScheduleId(int TeacherTrainingScheduleId, int BlockAdminUserId)
        {
            try
            {
                var result = await _TeachersTrainingRepo.GetTeacherTrainingScheduleByScheduleId(TeacherTrainingScheduleId,BlockAdminUserId);
                var data = result.GroupBy(item => item.Teacher_Training_Schedule_Id)
                           .Select(selector: x => new
                           {
                               Teacher_Training_Schedule_Id = x.Key,
                               Training_Title = x.Select(x => x.Training_Title).First(),
                               Training_Start_Date = x.Select(x => x.Training_Start_Date).First(),
                               Training_End_Date = x.Select(x => x.Training_End_Date).First(),
                               Training_Start_Time = x.Select(x => x.Training_Start_Time).First(),
                               Training_End_Time = x.Select(x => x.Training_End_Time).First(),
                               Training_Place = x.Select(x => x.Training_Place).First(),                               
                               Is_Training_End = x.Select(x => x.Is_Training_End).First(),
                               Block_Name = x.Select(x => x.Block_Name).First(),
                               Training_Description = x.Select(x => x.Training_Description).First(),

                               Tests = x.Select(y => new
                               {
                                   Teacher_Training_Schedule_Test_Id = y.Teacher_Training_Schedule_Test_Id,
                                   Test_Name = y.Test_Name,
                                   Test_Type = y.Test_Type,
                                   Subject_Id = y.Subject_Id,
                                   Subject_Name = y.Subject_Name,
                                   Is_Test_Done = y.Is_Test_Done,
                                   Is_Active = y.Is_Active
                               })
                           });
                if (result.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Training Schedule Not Found!", response = data });
                else
                {
                    return StatusCode(200, new { status = true, message = "Training Schedule Found Successfully.", response = data });
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("BlockAdmin/GetTeachersSelectedForTeacherTraining/{BlockAdminUserId}/{TeacherTrainingScheduleId}")]
        public async Task<IActionResult> GetSelectedTeachersForTeacherTrainingByBlockAdmin(int BlockAdminUserId, int TeacherTrainingScheduleId)
        {
            try
            {
                var result = await _TeachersTrainingRepo.GetSelectedTeachersForTeacherTrainingByBlockAdmin(BlockAdminUserId, TeacherTrainingScheduleId);
                if (result.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Teacher Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Teacher Found Successfully.", response = result });

            }
            catch (Exception ex)  
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpPost("BlockAdmin/MarkTeacherAttendanceForScheduledTraining")]
        public async Task<IActionResult> CreateTeacherAttendanceForScheduledTraining(TeacherTrainingAttendanceDto request)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.CreateTeacherAttendanceForScheduledTraining(request);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Attendance Marked Successfully", response = 1 });

                else if (ReturnVal == -2)
                    return StatusCode(200, new { status = false, message = "Invalid data entered to mark attendance!", response = 200 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }

        [HttpGet("BlockAdmin/GetTrainingScheduleAttendanceList/{TeacherTrainingScheduleId}/{BlockAdminUserId}")]
        public async Task<IActionResult> GetTeacherTrainingScheduleAttendance(int TeacherTrainingScheduleId, int BlockAdminUserId)
        {
            try
            {
                var result = await _TeachersTrainingRepo.GetTeacherTrainingScheduleAttendance(TeacherTrainingScheduleId, BlockAdminUserId);
                if (result.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Attendance Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Attendance Found Successfully.", response = result });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("BlockAdmin/GetTrainingScheduleTeachersAttendanceList/{TrainingScheduleAttendanceId}/{BlockAdminUserId}")]
        public async Task<IActionResult> GetTrainingScheduleTeachersAttendance(int TrainingScheduleAttendanceId, int BlockAdminUserId)
        {
            try
            {
                var result = await _TeachersTrainingRepo.GetTrainingScheduleTeachersAttendance(TrainingScheduleAttendanceId, BlockAdminUserId);
                if (result.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Attendance Not Found!", response = result });
                else
                    return StatusCode(200, new { status = true, message = "Attendance Found Successfully.", response = result });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("Teacher/GetTeacherTrainingSchedule/{Teacher_Id}/{Is_Upcoming}")]
        public async Task<IActionResult> GetTeacherTrainingScheduleByTeacherId(int Teacher_Id, bool Is_Upcoming)
        {
            try
            {
                var result = await _TeachersTrainingRepo.GetTeacherTrainingScheduleByTeacherId(Teacher_Id, Is_Upcoming);
                var data = result.GroupBy(item => item.Teacher_Training_Schedule_Id)
                           .Select(selector: x => new
                           {
                               Teacher_Training_Schedule_Id = x.Key,
                               Training_Schedule_Teacher_Id = x.Select(x => x.Training_Schedule_Teacher_Id).First(),
                               Training_Title = x.Select(x => x.Training_Title).First(),
                               Training_Start_Date = x.Select(x => x.Training_Start_Date).First(),
                               Training_End_Date = x.Select(x => x.Training_End_Date).First(),
                               Training_Start_Time = x.Select(x => x.Training_Start_Time).First(),
                               Training_End_Time = x.Select(x => x.Training_End_Time).First(),
                               Training_Place = x.Select(x => x.Training_Place).First(),
                               Is_Training_End = x.Select(x => x.Is_Training_End).First(),
                               Training_Description = x.Select(x => x.Training_Description).First(),

                               Tests = x.Select(y => new
                               {
                                   Teacher_Training_Schedule_Test_Id = y.Teacher_Training_Schedule_Test_Id,
                                   Test_Name = y.Test_Name,
                                   Test_Type = y.Test_Type,
                                   Subject_Id = y.Subject_Id,
                                   Subject_Name = y.Subject_Name,
                                   Is_Test_Done = y.Is_Test_Done,
                                   Test_Score = y.Test_Score,
                                   Is_Active = y.Is_Active
                               })
                           });
                if (result.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Training Schedule Not Found!", response = data });
                else
                {
                    return StatusCode(200, new { status = true, message = "Training Schedule Found Successfully.", response = data });
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("Teacher/GetTeacherTrainingScheduleDetailByTeacher/{TeacherTrainingScheduleId}/{TrainingScheduleTeacherId}")]
        public async Task<IActionResult> GetTeacherTrainingScheduleDetail(int TeacherTrainingScheduleId, int TrainingScheduleTeacherId)
        {
            try
            {
                var result = await _TeachersTrainingRepo.GetTeacherTrainingScheduleDetail(TeacherTrainingScheduleId, TrainingScheduleTeacherId);
                var data = result.GroupBy(item => item.Teacher_Training_Schedule_Id)
                           .Select(selector: x => new
                           {
                               Teacher_Training_Schedule_Id = x.Key,
                               Training_Schedule_Teacher_Id = x.Select(x => x.Training_Schedule_Teacher_Id).First(),
                               Training_Title = x.Select(x => x.Training_Title).First(),
                               Training_Start_Date = x.Select(x => x.Training_Start_Date).First(),
                               Training_End_Date = x.Select(x => x.Training_End_Date).First(),
                               Training_Start_Time = x.Select(x => x.Training_Start_Time).First(),
                               Training_End_Time = x.Select(x => x.Training_End_Time).First(),
                               Training_Place = x.Select(x => x.Training_Place).First(),
                               Is_Training_End = x.Select(x => x.Is_Training_End).First(),
                               Training_Description = x.Select(x => x.Training_Description).First(),

                               Tests = x.Select(y => new
                               {
                                   Teacher_Training_Schedule_Test_Id = y.Teacher_Training_Schedule_Test_Id,
                                   Test_Name = y.Test_Name,
                                   Test_Type = y.Test_Type,
                                   Subject_Id = y.Subject_Id,
                                   Subject_Name = y.Subject_Name,
                                   Is_Test_Done = y.Is_Test_Done,
                                   Test_Score = y.Test_Score,
                                   Is_Active = y.Is_Active
                               })
                           });
                if (result.ToList().Count == 0)
                    return StatusCode(200, new { status = false, message = "Training Schedule Not Found!", response = data });
                else
                {
                    return StatusCode(200, new { status = true, message = "Training Schedule Found Successfully.", response = data });
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpGet("Teacher/GetTeacherTrainingTestQuestions/{TeacherTrainingScheduleTestId}")]
        public async Task<IActionResult> GetTeacherTrainingTestQuestions(int TeacherTrainingScheduleTestId)
        {
            try
            {
                var response = (await _TeachersTrainingRepo.GetTeacherTrainingTestQuestions(TeacherTrainingScheduleTestId)).ToList();
                var data = response.GroupBy(item => item.Question_Group_Id).Select(g => new
                {
                    Question_Group_Id = g.Key,
                    Question_Group_Name = g.Select(x => x.Question_Group_Name.ToString()).FirstOrDefault(),

                    Questions = g.GroupBy(Qitem => Qitem.Question_Id).Select(Q => new
                    {
                        Question_Id = Q.Key,
                        Base64QuestionImage = _TeachersTrainingRepo.GetBase64StringImageByPath(Q.Select(x => x.Question_Media_Type).FirstOrDefault().ToString(), _webRootPath + @"\Uploads\QuestionImages\", Q.Select(x => x.Question_Media_Url).FirstOrDefault().ToString().Trim()),
                        Question_Media_Url = Q.Select(hx => hx.Question_Media_Url.ToString()).FirstOrDefault().ToString(),
                        Question_Type = Q.Select(hx => hx.Question_Type).FirstOrDefault(),
                        Question_Text = Q.Select(hx => hx.Question_Text).FirstOrDefault(),
                        Question_Media_Type = Q.Select(hx => hx.Question_Media_Type).FirstOrDefault(),

                        QuestionsOptions = Q.Select(optitem => new
                        {
                            Base64OptionImage = _TeachersTrainingRepo.GetBase64StringImageByPath(Q.Select(hx => hx.Option_Media_Type).FirstOrDefault().ToString(), _webRootPath + @"\Uploads\OptionImages\", optitem.Option_Media_Url.ToString().Trim()),
                            Question_Option_Id = optitem.Question_Option_Id,
                            Option_Media_Type_Id = optitem.Option_Media_Type_Id,
                            Option_Media_Type = optitem.Option_Media_Type,
                            Option_Text = optitem.Option_Text,
                            Option_Media_Url = optitem.Option_Media_Url.ToString(),
                            Is_Correct = optitem.Is_Correct
                        }).ToList()
                    }).ToList()
                });

                if (response.Count == 0)
                    return StatusCode(200, new { status = false, message = "Questions Not Found!", response = response });

                else
                    return StatusCode(200, new
                    {
                        status = true,
                        message = "Questions Found Successfully.",
                        response = data
                    });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }

        }

        [HttpPost("Teacher/SubmitTeacherTrainingAssessment")]
        public async Task<IActionResult> TeacherTrainingAssessmentQuestionsSave(TrainingScheduleTeacherAssessmentDto Assessment)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.TeacherTrainingAssessmentQuestionsSave(Assessment);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Assessment submitted successfully!", response = ReturnVal });
                else if (ReturnVal == -1)
                    return StatusCode(200, new { status = false, message = "Assessment already submitted for this test!", response = ReturnVal });

                else if (ReturnVal == -2)
                    return StatusCode(200, new { status = false, message = "Failed to submit Assessment! Try again.", response = ReturnVal });
                else if (ReturnVal == -3)
                    return StatusCode(200, new { status = false, message = "Failed to submit some Assessment Question! Try again.", response = ReturnVal });
                else if (ReturnVal == -4)
                    return StatusCode(200, new { status = false, message = "Failed to submit some Question Answers! Try again.", response = ReturnVal });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }

        [HttpPut("BlockAdmin/UpdateLatestTeacherAttendance")]
        public async Task<IActionResult> UpdateLatestTeacherAttendance(UpdateTeacherTrainingAttendanceDto request)
        {
            try
            {
                var ReturnVal = await _TeachersTrainingRepo.UpdateLatestTeacherAttendance(request);
                if (ReturnVal > 0)
                    return StatusCode(200, new { status = true, message = "Attendance Updated Successfully", response = 2 });

                else if (ReturnVal == -2)
                    return StatusCode(200, new { status = false, message = "Invalid data! Teacher must be added in schedule to mark/update attendance!", response = 200 });
               
                else if (ReturnVal == -3)
                    return StatusCode(200, new { status = false, message = "Invalid Attendance! You can update only latest Attendance.", response = 200 });
                else
                    return StatusCode(200, new { status = false, message = "Invalid data!", response = 400 });
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed!" + ex.Message, response = 500 });

            }
        }
    }
}
