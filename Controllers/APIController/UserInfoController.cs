using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NTKServer.Business;
using NTKServer.Internal;
using NTKServer.Models;


namespace NTKServer.Controllers
{
    public class UserInfoController : BaseAPIController
    {
        [HttpPost("getUserInfo")]
        public APIResponse<GetUserInfoResponse> getUserInfo(GetUserInfoRequest req)
        {

            GetUserInfoResponse rep = new GetUserInfoResponse
            {
                test = req.test + 1
            };
            return APIResponse<GetUserInfoResponse>.Ok(rep);
        }
    }
}

