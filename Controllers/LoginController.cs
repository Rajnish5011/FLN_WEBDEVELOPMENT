using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Dto.Login;
using ASPNetCoreFLN_APIs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _repo;
        public LoginController(ILoginRepository repo)
        {
            _repo = repo;
        }
        [HttpPost("StateAdmin/UserLogin")]
        public async Task<IActionResult> StateUserLogin([FromBody] LoginDto request)
        {
            try
            {
                var data = await _repo.GetLogin(request);
                if (data == null)
                    return StatusCode(401, new { status = false, message = "Invalid Username or Password!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Login Successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpPost("MentorLogin")]
        public async Task<IActionResult> MentorLogin([FromBody] MentorLoginDto request)
        {
            try
            {
                var data = await _repo.MentorLogin(request);
                if (data == null)
                    return StatusCode(401, new { status = false, message = "Invalid Username or Password!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Login Successfully.", response = data });

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        [HttpPost("School/TeacherLogin")]
        public async Task<IActionResult> TeacherLogin([FromBody] TeacherLoginDto request)
        {
            try
            {
                var data = await _repo.TeacherLogin(request);
                if (data == null)
                    return StatusCode(401, new { status = false, message = "Invalid Username or Password!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Login Successfully.", response = data });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
        [HttpPost("UserDashboardLogin")]
        public async Task<IActionResult>UserDashboardLogin([FromBody] UserDashboardLoginDto request)
        {
            int RoleID = 0;
            try
            {
                var response = (await _repo.UserDashboardLogin(request)).ToList();
                if (response.ToList().Count > 0)
                    RoleID = response.FirstOrDefault().Role_Id;

                if (RoleID == 1)
                {
                    var data = response.GroupBy(item => item.User_Id).Select(selector: g => new
                    {
                        User_Id = g.Key,
                        Role_Id = g.Select(x => x.Role_Id).FirstOrDefault(),
                        Role_Name = g.Select(x => x.Role_Name).FirstOrDefault(),

                        Details = g.Select(itm => new
                        {
                            State_Name = itm.State_Name,
                            District_Id = itm.District_Id,
                            District_Name = itm.District_Name,
                            Block_Id = itm.Block_Id,
                            Block_Name = itm.Block_Name,
                            Cluster_School_Id = itm.Cluster_School_Id,
                            Cluster_School_Name = itm.Cluster_School_Name,
                            Cluster_School_Code = itm.Cluster_School_Code,
                            Cluster_UDISE_Code = itm.Cluster_UDISE_Code,
                            School_Id = itm.School_Id,
                            School_Name = itm.School_Name,
                            School_Code = itm.School_Code,
                            UDISE_Code = itm.UDISE_Code,
                            School_Contact = itm.School_Contact,
                            School_Address = itm.School_Address,
                            Medium = itm.Medium,
                            Medium_Id = itm.Medium_Id,
                            Board_Id = itm.Board_Id,
                            Board_Name = itm.Board_Name,
                            Email_Id = itm.Email_Id,
                            School_Image_Url = itm.School_Image_Url,
                            Phone_No = itm.Phone_No,
                            Mobile_No = itm.Mobile_No,
                            School_Longitude = itm.School_Longitude,
                            School_Latitude = itm.School_Latitude

                        }).ToList()
                    });
                    if (data.ToList().Count == 0)
                        return StatusCode(401, new { status = false, message = "Invalid Username or Password!", response = data });
                    else
                        return StatusCode(200, new { status = true, message = "Login Successfully.", response = data });
                }
                else if (RoleID == 6)
                {
                    var data = response.GroupBy(item => item.User_Id).Select(selector: g => new
                    {
                        User_Id = g.Key,
                        Role_Id = g.Select(x => x.Role_Id).FirstOrDefault(),
                        Role_Name = g.Select(x => x.Role_Name).FirstOrDefault(),

                        Details = g.Select(itm => new
                        {
                            State_Name = itm.State_Name,
                            Email_Id = itm.Email_Id,
                            Mobile_No = itm.Mobile_No

                        }).ToList()
                    });
                    if (data.ToList().Count == 0)
                        return StatusCode(401, new { status = false, message = "Invalid Username or Password!", response = data });
                    else
                        return StatusCode(200, new { status = true, message = "Login Successfully.", response = data });
                }
                else if (RoleID == 7)
                {
                    var data = response.GroupBy(item => item.User_Id).Select(selector: g => new
                    {
                        User_Id = g.Key,
                        Role_Id = g.Select(x => x.Role_Id).FirstOrDefault(),
                        Role_Name = g.Select(x => x.Role_Name).FirstOrDefault(),

                        Details = g.Select(itm => new
                        {
                            State_Name = itm.State_Name,
                            District_Id = itm.District_Id,
                            District_Name = itm.District_Name,
                            Email_Id = itm.Email_Id,
                            Mobile_No = itm.Mobile_No

                        }).ToList()
                    });
                    if (data.ToList().Count == 0)
                        return StatusCode(401, new { status = false, message = "Invalid Username or Password!", response = data });
                    else
                        return StatusCode(200, new { status = true, message = "Login Successfully.", response = data });
                }
                else if (RoleID == 8)
                {
                    var data = response.GroupBy(item => item.User_Id).Select(selector: g => new
                    {
                        User_Id = g.Key,
                        Role_Id = g.Select(x => x.Role_Id).FirstOrDefault(),
                        Role_Name = g.Select(x => x.Role_Name).FirstOrDefault(),

                        Details = g.Select(itm => new
                        {
                            State_Name = itm.State_Name,
                            District_Id = itm.District_Id,
                            District_Name = itm.District_Name,
                            Block_Id = itm.Block_Id,
                            Block_Name = itm.Block_Name,
                            Email_Id = itm.Email_Id,
                            Mobile_No = itm.Mobile_No

                        }).ToList()
                    });
                    if (data.ToList().Count == 0)
                        return StatusCode(401, new { status = false, message = "Invalid Username or Password!", response = data });
                    else
                        return StatusCode(200, new { status = true, message = "Login Successfully.", response = data });
                }
                else if (RoleID == 9)
                {
                    var data = response.GroupBy(item => item.User_Id).Select(selector: g => new
                    {
                        User_Id = g.Key,
                        Role_Id = g.Select(x => x.Role_Id).FirstOrDefault(),
                        Role_Name = g.Select(x => x.Role_Name).FirstOrDefault(),

                        Details = g.Select(itm => new
                        {
                            Full_Name = itm.Full_Name,
                            State_Name = itm.State_Name,
                            District_Id = itm.District_Id,
                            District_Name = itm.District_Name,
                            Email_Id = itm.Email_Id,
                            Mobile_No = itm.Mobile_No

                        }).ToList()
                    });
                    if (data.ToList().Count == 0)
                        return StatusCode(401, new { status = false, message = "Invalid Username or Password!", response = data });
                    else
                        return StatusCode(200, new { status = true, message = "Login Successfully.", response = data });
                }
                else
                {
                    var data = response.GroupBy(item => item.User_Id).Select(selector: g => new
                    {
                        User_Id = g.Key,
                        Role_Id = g.Select(x => x.Role_Id).FirstOrDefault(),
                        Role_Name = g.Select(x => x.Role_Name).FirstOrDefault(),

                        Details = g.Select(itm => new
                        {
                            State_Name = itm.State_Name,                            
                            Email_Id = itm.Email_Id,
                            Mobile_No = itm.Mobile_No

                        }).ToList()
                    });
                    return StatusCode(403, new { status = false, message = "Invalid Username or Password!", response = data });
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }
        [HttpPost("Student/ParentLogin")]
        public async Task<IActionResult> ParentLogin([FromBody] ParentLoginDto request)
        {
            try
            {
                var data = await _repo.ParentLogin(request);
                if (data == null)
                    return StatusCode(401, new { status = false, message = "Invalid Username or Password!", response = data });
                else
                    return StatusCode(200, new { status = true, message = "Login Successfully.", response = data });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
            }
        }

        //[HttpPost("Grievance/HelpDeskUserLogin")]
        //public async Task<IActionResult> HelpDeskUserLogin([FromBody] HelpDeskUserLoginDto request)
        //{
        //    try
        //    {
        //        var data = await _repo.HelpDeskUserLogin(request);
        //        if (data == null)
        //            return StatusCode(403, new { status = false, message = "Invalid Username or Password!", response = data });
        //        else
        //            return StatusCode(200, new { status = true, message = "Login Successfully.", response = data });

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { status = false, message = "Failed! " + ex.Message, response = 500 });
        //    }
        //}
    }
}
