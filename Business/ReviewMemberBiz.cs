using DB.Services;
using Models.Dto;
using Org.BouncyCastle.Tls;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Tool;
using NTKServer.ViewModels.ReviewMember;
using NTKServer.Models.Admin;

namespace NTKServer.Business
{
    public class ReviewMemberBiz
    {
        #region CRUD
		public static List<ReviewMemberList> GetReviewMemberList(ReviewMemberFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must("id_auth <> 1");
            return MemberService.FindReviewMemberList(whereSql)?
                .Select(reviewMember => PublicTool.convertUtcToLocalTime(reviewMember)).ToList();
        }

        public static MemberDto Get(int pk)
        {
            return MemberService.Find(pk);
        }
        
        public static VerifyMemberVm GetVerifyMember(int pk)
        {
            var member = MemberService.GetVerifyMember(pk);
            return member;
        }

        public static void PostCreate(MemberDto req)
        {
            if (MemberService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(MemberDto req)
        {
            if( MemberService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            MemberService.Remove(pk);
        }

        #endregion

        public static bool VerifyMember(VerifyMemberVm vm, int current_admin_pk)
        {

            if (vm.verify_status == 1) // 身份证是否通过校验 0未做过  1通过 2错误 3.审核中
            {
                var member = MemberService.Find(vm.pk);
				//1.判斷選取客服
				//必須有身分證
				if (!vm.admin_user_fk.HasValue) throw new AppException("需选择客服");
				var memberReviewCardFrontRequired = ConfigLib.Get("review_card_required");
				if (memberReviewCardFrontRequired == "1" && string.IsNullOrWhiteSpace(member.card_pic_front)) throw new AppException("需上传证件照正面");
                
				//2.變更狀態
				MemberService.UpdateStatus(vm.pk, vm.verify_status);
				//3.新增客服專員
				MemberService.UpdateAdminUserFk(vm.pk, vm.admin_user_fk.Value);
				//4.更新審核訊息與時間
				MemberService.UpdateAuthTimeAndResult(vm.pk, DateTime.UtcNow, "");

                MemberTaskLib.MemberTaskFinish(vm.pk, 2, "CN"); // 实名认证完成

                //5.通過給會員邀請碼
                var invitationCode = PublicTool.GenerateRandomName(8);
				MemberService.UpdateInvitationCode(vm.pk, invitationCode);

                AdminUserDto adminUserDto = AdminUserService.Find(current_admin_pk);
                object[]? list = new object[3] {adminUserDto.account, adminUserDto.nickname, member.account};
                AdminLogRequest adminLogRequest = new AdminLogRequest
                {
                    admin_user_pk = current_admin_pk,
                    admin_menu_fk = 433,
                    createtime = DateTime.UtcNow,
                    list = list
                };
                AdminLogLib.Save(adminLogRequest);

            }
			else if (vm.verify_status == 2)
            {
				//變更狀態
				MemberService.UpdateStatus(vm.pk, vm.verify_status);
				//更新審核訊息與時間
				MemberService.UpdateAuthTimeAndResult(vm.pk, DateTime.UtcNow, vm.auth_result);
			}
            else
            {
				//變更狀態
				MemberService.UpdateDel(vm.pk, vm.verify_status);
			}

            

			return true;
        }        
    }
}
