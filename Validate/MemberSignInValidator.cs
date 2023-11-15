using FluentValidation;
using NTKServer.Models.MemberOp;
using NTKServer.Models.Dto;
using NTKServer.Services;
using NTKServer.Internal;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using DB.Services;

namespace NTKServer.Validates
{
    public class MemberSignInValidator : AbstractValidator<SignInRequest>
    {
        public MemberSignInValidator()
        {            
            RuleFor(x => x.phone_number).Custom((phone_number, context) =>
            {
                if (string.IsNullOrEmpty(phone_number))
                {
                    throw new AppException(1202, "fill_phone_number");
                }
            });

            RuleFor(x => x.password).Custom((password, context) =>
            {
                if (string.IsNullOrEmpty(password))
                    throw new AppException(1207, "enter_password");
                if (password.Length < 6 || password.Length > 32)
                    throw new AppException(1208, "password_length_6_32_error");
                if (!Regex.IsMatch(password, @"[A-Za-z]"))
                    throw new AppException(1205, "password_needs_letter");
                if (!Regex.IsMatch(password, @"^[A-Za-z0-9]*$"))
                    throw new AppException(1206, "password_alpha_numeric");
            });

        }

        /// <summary>
        /// DB 验证
        /// </summary>
        public int DbAuth(SignInRequest input)
        {
            //MemberResponse result = MemberServices.GetByUsernameOrEmail(input.email);

            //if (result == null)
            //{
            //    throw new AppException(1270, "[1270] 帐号错误");
            //}

            //if (result.status == 0)
            //{
            //    throw new AppException(1230, "[1230] 用户被停权");
            //}

            //try
            //{
            //    bool test = BCrypt.Net.BCrypt.Verify(input.passwd, result.passwd);

            //    if (test == false)
            //    {

            //        throw new AppException(1240, "密码错误");
            //        // throw new AppException(1240, "[1240] 密码错误");
            //    }
            //}
            //catch(AppException)
            //{
            //    string pass = BCrypt.Net.BCrypt.HashPassword(input.passwd);
            //    throw new AppException(1240, "密码错误");
            //}
            //catch (Exception)
            //{
            //    throw new AppException(1230, "帐号无效");
            //}


            //return result.pk;
            return 1;
        }

        /// <summary>
        /// 检查用户版本是否太旧
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        private static bool VersionWork(string version)
        {
            // 预留功能, 暂时没用处
            return true;
        }
    }
}
