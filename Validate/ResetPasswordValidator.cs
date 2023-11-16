using FluentValidation;
using NTKServer.Models.MemberOp;

using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using DB.Services;
using NTKServer.Models;
using Models.Dto;
using NTKServer.Internal;

namespace NTKServer.Validates
{
    public class ResetPasswordValidator : AbstractValidator<ResetPassWordRequest>
    {
        public ResetPasswordValidator()
        {
          
            RuleFor(x => x.old_pw).Custom((passwd, context) =>
            {
                if (string.IsNullOrEmpty(passwd))
                    throw new AppException(1207, "enter_password");
                if (passwd.Length < 6 || passwd.Length > 96)
                    throw new AppException(1208, "password_length_6_96_error");
                if (!Regex.IsMatch(passwd, @"[A-Za-z]"))
                    throw new AppException(1205, "password_needs_letter");
                if (!Regex.IsMatch(passwd, @"^[A-Za-z0-9]*$"))
                    throw new AppException(1206, "password_alpha_numeric");
            });

            RuleFor(x => x.new_pw).Custom((passwd, context) =>
            {
                if (string.IsNullOrEmpty(passwd))
                    throw new AppException(1207, "enter_password");
                if (passwd.Length < 6 || passwd.Length > 96)
                    throw new AppException(1208, "password_length_6_96_error");
                if (!Regex.IsMatch(passwd, @"[A-Za-z]"))
                    throw new AppException(1205, "password_needs_letter");
                if (!Regex.IsMatch(passwd, @"^[A-Za-z0-9]*$"))
                    throw new AppException(1206, "password_alpha_numeric");
            });            
        }

        /// <summary>
        /// DB 验证
        /// </summary>
        public int DbAuth(TokenModel tokenModel, ResetPassWordRequest input)
        {
            //MemberResponse result = MemberServices.GetByUsernameOrEmail(input.email);
            MemberDto result = MemberService.Find(tokenModel.member_fk);

            if (result == null)
            {
                throw new AppException(1270, "none_account");
            }
            
            if (!result.status)
            {
                throw new AppException(1230, "user_permission_closed");
            }

            bool test = BCrypt.Net.BCrypt.Verify(input.old_pw, result.passwd);
            if (test == false )
            {
                throw new AppException(1240, "user_verify_failed");
            }            

            return result.pk;
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
