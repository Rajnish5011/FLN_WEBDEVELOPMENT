using ASPNetCoreFLN_APIs.Dto.Login;
using ASPNetCoreFLN_APIs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Contracts
{
    public interface ILoginRepository
    {
        public Task<LoginResponseDto> GetLogin(LoginDto request);
        public Task<MentorLoginResponse> MentorLogin(MentorLoginDto request);
        public Task<TeacherProfile> TeacherLogin(TeacherLoginDto request);
        public Task<IEnumerable<UserDashboardLoginResponse>> UserDashboardLogin(UserDashboardLoginDto request);
        public Task<ParentLoginRespone> ParentLogin(ParentLoginDto request);

        //public Task<HelpDeskUserLoginResponse> HelpDeskUserLogin(HelpDeskUserLoginDto request);
    }
}
