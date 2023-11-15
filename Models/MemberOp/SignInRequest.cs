using System;
namespace NTKServer.Models.MemberOp
{
	public class SignInRequest
	{
        public string phone_number { get; set; }
        public string password { get; set; }
    }
}

