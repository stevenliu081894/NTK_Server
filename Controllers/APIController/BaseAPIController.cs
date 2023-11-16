using System;
using Microsoft.AspNetCore.Mvc;
using NTKServer.Models;

namespace NTKServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseAPIController: Controller
	{
        protected TokenModel GetToken()
        {
            //if (Request.Headers.TryGetValue("token", out var token) == true)
            //{
            //    return TokenCatch.GetToken(token);
            //}
            //else return null;
            return null;
        }

        protected string GetIp()
        {
            try
            {
                return Request.HttpContext.Connection.RemoteIpAddress?.ToString();

            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}

