using DB.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Serilog;
using System.Text.Json;
using NTKServer.Business;
using NTKServer.Cache;
using NTKServer.Internal;
using NTKServer.Validates;
using NTKServer.Models.MemberOp;
using NTKServer.Models;

namespace NTKServer.Controllers
{
    public class MemberOpController : BaseAPIController
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MemberOpController(
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment
            )
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        private string GetIP()
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

        private string GetDevice()
        {
            try
            {
                return Request.Headers["User-Agent"].ToString();

            }
            catch (Exception)
            {
                return "";
            }
        }


        /////// <summary>
        /////// 取得注册验证码
        /////// </summary>
        //[HttpPost("verifymailcode")]
        //public async Task<APIResponse> VerifyMailCode(VerifyPhoneCodeRequest req)
        //{
        //    try
        //    {
        //        VerifyPhoneCodeValidator validator = new VerifyPhoneCodeValidator();
        //        validator.ValidateAndThrow(req);

        //        // 检查Member是否有此EMAIL
        //        validator.CheckMember(req.email);

        //        // 检查Verity是否有此EMAIL~并写入或更新
        //        string verifyCode = MemberBiz.GetVerifyCode();
        //        if (validator.CheckEmail(req.email, ref verifyCode))
        //        {
        //            DocResponse docResponse = CmsDocumentService.GetDocByCid("verity", req.lang);

        //            string htmlBody = docResponse.content.Replace("{0}", verifyCode);

        //            await Email.mailapiAsync(req.email, docResponse.title, htmlBody);
        //        }
        //        return APIResponse.Ok(null, ""); // todo: member/verifymailcode 这个发送验证码的 在接口中把码返回来，测试期间所有获取二维码的可以先返回来
        //    }
        //    catch (AppException ex)
        //    {
        //        Log.Error(ex, ex.Message);
        //        return APIResponse.Error(ex.GetStatus(), ex.GetMessage(req.lang));
        //    }
        //}

        ///// <summary>
        ///// 忘记密码取得验证码
        ///// </summary>
        //[HttpPost("resetpwdverifymailcode")]
        //public async Task<APIResponse> ResetPWDVerifyMailCode(VerifyMailCodeRequest req)
        //{
        //    try
        //    {
        //        VerifyMailCodeValidator validator = new VerifyMailCodeValidator();
        //        validator.ValidateAndThrow(req);

        //        if (validator.CheckMailInMember(req.email)) // 检查Member是否有此EMAIL
        //        {
        //            string verifyCode = MemberBiz.GetVerifyCode(); // 取得亂數驗證碼
        //            if (validator.CheckEmail(req.email, ref verifyCode))
        //            {

        //                DocResponse docResponse = CmsDocumentService.GetDocByCid("verity", req.lang);

        //                string htmlBody = docResponse.content.Replace("{0}", verifyCode);

        //                await Email.mailapiAsync(req.email, docResponse.title, htmlBody);
        //            }
        //        }
        //        return APIResponse.Ok(null, "");
        //    }
        //    catch (AppException ex)
        //    {
        //        Log.Error(ex, ex.Message);
        //        return APIResponse.Error(ex.GetStatus(), ex.GetMessage(req.lang));
        //    }
        //}

        /// <summary>
        /// 會員註冊
        /// </summary>
        [HttpPost("signup")]
        public async Task<APIResponse> SignUp(SignUpRequest req)
        {
            try
            {
                MemberSignUpValidator validator = new MemberSignUpValidator();
                validator.ValidateAndThrow(req);

                // 检查验证码
                if (!validator.DbAuth(req, GetIP()))
                {
                    return APIResponse.Error(900, "other_error");
                }
                return APIResponse.Ok(null, "註冊成功");
            }
            catch (AppException ex)
            {
                Log.Error(ex, ex.Message);
                return APIResponse.Error(ex.GetStatus(), ex.GetMessage(""));
            }
        }

        /// <summary>
        /// 登入
        /// </summary>
        [HttpPost("signin")]
        public APIResponse<SignInResponse> SignIn(SignInRequest input)
        {
            MemberLoginDto member_login = new MemberLoginDto
            {
                ip = GetIP(),
                login_account = input.phone_number,
                device = GetDevice(),
                create_time = DateTime.UtcNow,
            };

            try
            {
                MemberSignInValidator validator = new MemberSignInValidator();
                validator.ValidateAndThrow(input);
                
                int pk = validator.DbAuth(input);
                member_login.member_fk = pk;

                TokenModel tokenModel = new TokenModel
                {
                    member_fk = pk,
                    ip = GetIP(),
                };

                //SignInResponse data = AuthBiz.Login(tokenModel);

                //MemberBiz.CreateLoginRecord(member_login, 1, "登入成功");

                return APIResponse<SignInResponse>.Ok(new SignInResponse(), "登入成功");
            }
            catch (AppException ex)
            {
                Log.Error(ex, ex.Message);
                return APIResponse<SignInResponse>.Error(ex.GetStatus(), ex.GetMessage(""));
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost("signout")]
        public APIResponse LogOut()
        {
            TokenModel tokenModel = GetToken();
            try
            {
                //AuthBiz.Logout(tokenModel);
                return APIResponse.Ok("");
            }
            catch (AppException ex)
            {
                Log.Error(ex, ex.Message);
                return APIResponse.Error(ex.GetStatus(), ex.GetMessage(""));
            }
        }

        /// <summary>
        /// 變更密碼
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("resetpassword")]
        public APIResponse ResetPassword(ResetPassWordRequest input)
        {
            TokenModel tokenModel = GetToken();
            try
            {
                ResetPasswordValidator validator = new ResetPasswordValidator();
                validator.ValidateAndThrow(input);
                int pk = validator.DbAuth(tokenModel, input);

                //MemberBiz.DbResetPassword(input.newpasswd, tokenModel.member_fk);

                return APIResponse.Ok(null, "密码修改成功");
            }
            catch (AppException ex)
            {
                Log.Error(ex, ex.Message);
                return APIResponse.Error(ex.GetStatus(), ex.GetMessage(GetToken().lang)); // TODO
            }
        }

        /// <summary>
        /// 变更手機號碼
        /// </summary>
        //[HttpPost("resetphonenumber")]
        //public APIResponse ResetPhoneNumber(ResetPhoneRequest input)
        //{
        //    TokenModel tokenModel = GetToken();
        //    try
        //    {
        //        ResetPhoneNumberValidator validator = new ResetPhoneNumberValidator();
        //        validator.ValidateAndThrow(input);
        //        validator.DbAuth(tokenModel, input.password);

        //        MemberBiz.DbResetPhoneNumber(input, tokenModel.member_fk);

        //        return APIResponse.Ok("");
        //    }
        //    catch (AppException ex)
        //    {
        //        Log.Error(ex, ex.Message);
        //        return APIResponse.Error(ex.GetStatus(), ex.GetMessage(input.lang));
        //    }
        //}

        /// <summary>
        /// 實名認證
        /// </summary>
        //[HttpPost("verifyidentity")]
        //public APIResponse VerifyIdentity(VerifyIdentityRequest input)
        //{
        //    TokenModel tokenModel = GetToken();
        //    try
        //    {
        //        IdentityVerifyValidator validator = new IdentityVerifyValidator();
        //        validator.ValidateAndThrow(input);

        //        MemberServices.UploadIdentityVerification(input, tokenModel.member_fk);

        //        return APIResponse.Ok(null, "實名認證上傳成功");

        //    }
        //    catch (AppException ex)
        //    {
        //        Log.Error(ex, ex.Message);
        //        return APIResponse.Error(ex.GetStatus(), ex.GetMessage(GetToken().lang)); // TODO
        //    }
        //}

        /// <summary>
        /// 申请变更密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[HttpPost("passwordapply")]
        //public async Task<APIResponse> PasswordApply(PasswordApplyRequest input)
        //{
        //    TokenModel tokenModel = GetToken();
        //    try
        //    {
        //        PasswordApplyValidator validator = new PasswordApplyValidator();
        //        validator.ValidateAndThrow(input);

        //        // 驗證圖形驗證碼
        //        CacheQueryAsync.SelectDB(3); // db3：API 資料存放區
        //        string key = $"vericode_{HttpContext.Connection.RemoteIpAddress.Text()}";
        //        string code = await CacheQueryAsync.StringGetAsync(key);
        //        if (code != input.captcha)
        //        {
        //            throw new AppException(1200, "incorrect_captcha_code");
        //        }

        //        MemberResponse memberResponse = MemberServices.GetByUsernameOrEmail(input.email);

        //        MemberBiz.DbResetPassword(input.newpasswd, memberResponse.pk);

        //        return APIResponse.Ok(null, "申请变更密码成功");
        //    }
        //    catch (AppException ex)
        //    {
        //        Log.Error(ex, ex.Message);
        //        return APIResponse.Error(ex.GetStatus(), ex.GetMessage(GetToken().lang)); // TODO
        //    }
        //}

        /// <summary>
        /// 更新图片验证码
        /// </summary>
        //[HttpGet("vertrfycode")]
        //public async Task<IActionResult> VertrfyCode()
        //{
        //    //string dir = _webHostEnvironment.GetPath("/pub/captcha");
        //    string pathCaptcha = Path.Combine("Image", "captcha");
        //    string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathCaptcha);
        //    string code = Captcha.CreateCode(dir);

        //    CacheQueryAsync.SelectDB(3); // db3：API 資料存放區
        //    string key = $"vericode_{HttpContext.Connection.RemoteIpAddress.Text()}";
        //    //string code = await CacheQueryAsync.StringGetAsync(key);

        //    _ = await CacheQueryAsync.StringSetAsync(key, code, new TimeSpan(0, 5, 0));

        //    string path = Path.Combine(dir, $"{code}.jpg");
        //    using (MemoryStream stream = Captcha.BuildImage(code, path))
        //    {
        //        return File(stream.ToArray(), "image/jpeg");
        //    }
        //}

        /// <summary>
        /// 取得帐号设置
        /// </summary>
        //[HttpPost("getsetting")]
        //public APIResponse<GetSettingResponse?> GetSetting()
        //{
        //    TokenModel tokenModel = GetToken();

        //    try
        //    {
        //        GetSettingResponse? res = MemberBiz.GetSettingById(tokenModel);

        //        return APIResponse<GetSettingResponse?>.Ok(res);
        //    }
        //    catch (AppException ex)
        //    {
        //        Log.Error(ex, ex.Message);
        //        return APIResponse<GetSettingResponse?>.Error(ex.GetStatus(), ex.GetMessage(tokenModel.lang));
        //    }
        //}

        /// <summary>
        /// 手机认证
        /// </summary>
        //[HttpPost("sendphoneverify")]
        //public APIResponse SendPhoneVerify(SendphoneVerifyRequest request)
        //{
        //    TokenModel tokenModel = GetToken();

        //    try
        //    {
        //        MemberBiz.SendVerificationCode(request);

        //        return APIResponse.Ok(null);
        //    }
        //    catch (AppException ex)
        //    {
        //        Log.Error(ex, ex.Message);
        //        return APIResponse.Error(ex.GetStatus(), ex.GetMessage(tokenModel.lang));
        //    }
        //}

        /// <summary>
        /// 是否已验证过
        /// </summary>
        //[HttpPost("isfinishauth")]
        //public APIResponse IsFinishAuth()
        //{
        //    TokenModel tokenModel = GetToken();

        //    try
        //    {
        //        bool isAuthFinished = MemberBiz.IsAuthFinished(tokenModel);

        //        return APIResponse.Ok(isAuthFinished);
        //    }
        //    catch (AppException ex)
        //    {
        //        Log.Error(ex, ex.Message);
        //        return APIResponse.Error(ex.GetStatus(), ex.GetMessage(tokenModel.lang));
        //    }
        //}

        /// <summary>
        /// 验证手机验证码
        /// </summary>
        //[HttpPost("verifyphonecode")]
        //public APIResponse VerifyPhoneCode(VerifyPhoneCodeRequest req)
        //{
        //    TokenModel tokenModel = GetToken();

        //    try
        //    {
        //        bool isCodeValid = MemberBiz.VerifyPhoneCode(tokenModel, req);

        //        return APIResponse.Ok(isCodeValid);
        //    }
        //    catch (AppException ex)
        //    {
        //        Log.Error(ex, ex.Message);
        //        return APIResponse.Error(ex.GetStatus(), ex.GetMessage(tokenModel.lang));
        //    }
        //}

        ///// <summary>
        ///// 获取通知设置
        ///// </summary>
        //[HttpPost("getnotify")]
        //public APIResponse<GetNotifyResponse> GetNotify()
        //{
        //    TokenModel tokenModel = GetToken();

        //    try
        //    {
        //        GetNotifyResponse response = MemberBiz.GetNotifySetting(tokenModel);

        //        return APIResponse<GetNotifyResponse>.Ok(response);
        //    }
        //    catch (AppException ex)
        //    {
        //        Log.Error(ex, ex.Message);
        //        return APIResponse<GetNotifyResponse>.Error(ex.GetStatus(), ex.GetMessage(tokenModel.lang));
        //    }
        //}
    }
}
