using FluentValidation;
using NTKServer.Models.MemberOp;
using NTKServer.Services;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using DB.Services;
using Models.Dto;
using NTKServer.Libs;
using NTKServer.Internal;

namespace NTKServer.Validates
{
    public class MemberSignUpValidator : AbstractValidator<SignUpRequest>
    {
        public MemberSignUpValidator()
        {
            // 帳號account 4-16 只能 英文+数字..不能特殊字元, 第一码不可以数字
            RuleFor(x => x.account).Custom((account, context) =>
            {
                if (string.IsNullOrEmpty(account))
                    throw new AppException(1234, "enter_account");
                if (account.Length < 4 || account.Length > 16)
                    throw new AppException(1235, "account_length_4_16_error");
                if (!Regex.IsMatch(account, @"^[A-Za-z][A-Za-z0-9]*$"))
                    throw new AppException(1236, "account_alpha_numeric");
            });


            RuleFor(x => x.phone_number).Custom((phone_number, context) =>
            {
                if (string.IsNullOrEmpty(phone_number))
                {
                    throw new AppException(1202, "fill_phone_number");
                }
                if (!new PhoneAttribute().IsValid(phone_number))
                {
                    throw new AppException(1203, "invalid_phone_number_format");
                }
            });

            RuleFor(x => x.password).Custom((password, context) =>
            {
                if (string.IsNullOrEmpty(password))
                    throw new AppException(1207, "enter_password");
                if (password.Length < 6 || password.Length > 96)
                    throw new AppException(1208, "password_length_6_96_error");
                if (!Regex.IsMatch(password, @"[A-Za-z]"))
                    throw new AppException(1205, "password_needs_letter");
                if (!Regex.IsMatch(password, @"^[A-Za-z0-9]*$"))
                    throw new AppException(1206, "password_alpha_numeric");
            });

            RuleFor(x => x.sex).Custom((sex, context) =>
            {
                if (string.IsNullOrEmpty(sex))
                    throw new AppException(1208, "enter_sex");
                if (sex != "M" && sex != "W")
                    throw new AppException(1208, "sex is not valid");
            });

            //RuleFor(x => x.verity_mail).Custom((verity, context) =>
            //{
            //    if (string.IsNullOrEmpty(verity))
            //    {
            //        throw new AppException(1211, "fill_verification_code");
            //    }
            //    if (!Regex.IsMatch(verity, @"^\d{4}$"))
            //    {
            //        throw new AppException(1212, "incorrect_verification_code");
            //    }
            //});

            //RuleFor(x => x.invitation_code).Custom((code, context) =>
            //{
            //    if (!string.IsNullOrEmpty(code) && !Regex.IsMatch(code.ToLower(), "^[0-9,a-z]{8}$"))
            //    {
            //        throw new AppException(1214, "incorrect_invitation_code");
            //    }
            //});

            //RuleFor(x => x.lang).Custom((language, context) =>
            //{
            //    if (string.IsNullOrEmpty(language))
            //    {
            //        throw new AppException(1217, "incorrect_language");
            //    }
            //});

            //RuleFor(x => x.verity_captcha).Custom((verity, context) =>
            //{
            //    if (string.IsNullOrEmpty(verity))
            //    {
            //        throw new AppException(1232, "fill_captcha_code");
            //    }
            //    //if (!System.Text.RegularExpressions.Regex.IsMatch(verity, @"^\d{4}$"))
            //    //{
            //    //    throw new AppException(1200, "incorrect_captcha_code");
            //    //}
            //});
        }

        private string CreatePassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string dbpassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, dbpassword);
        }

        public bool DbAuth(SignUpRequest input, string client_ip)
        {
            //if (MemberServices.CheckPhoneExists(input.phone_number))
            //{
            //    throw new AppException(1201, "already_member");
            //}



            /// need to change to verify sms code logic ////
            //VerityService verityService = new VerityService();
            //VerifyResponse verifyResponse = verityService.GetByEmail(input.email);

            //if (verifyResponse == null) // 如果沒有取得驗證碼
            //{
            //    throw new AppException(1215, "verification_code_not_received");
            //}
            //else // 如果電子郵件存在
            //{
            //    TimeSpan span = DateTime.UtcNow - verifyResponse.send_time;

            //    if (span.TotalMinutes > 15) // 如果已經過了15分鐘
            //    {
            //        throw new AppException(1216, "verification_code_expired");
            //    }
            //    if (input.verity_mail != verifyResponse.code) // 驗證碼錯誤
            //    {
            //        throw new AppException(1212, "incorrect_verification_code");
            //    }
            //}

            //if (MemberServices.CheckUsernameExists(input.account))
            //{
            //    throw new AppException(1233, "already_account");
            //}



            /// add member to db ///
            //MemberDto memberDto = new MemberDto
            //{
            //    email = input.email,
            //    passwd = CreatePassword(input.passwd),
            //    account = input.account,
            //    create_time = DateTime.UtcNow,
            //    create_ip = client_ip,
            //    last_login_time = DateTime.UtcNow,
            //    last_login_ip = client_ip,
            //    auth_time = DateTime.UtcNow,
            //    email_status = true,
            //    lang = input.lang,
            //    country = ConfigLib.Get("app_default_lang"),
            //    date_format = input.country_pk == "VIETNAM" ? 5 : null
            //};
            //int pk = MemberServices.Insert(memberDto);

            //verityService.Delete(input.email);

            return true;
        }
    }
}
