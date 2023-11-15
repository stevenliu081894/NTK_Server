using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Tool;
using NTKServer.ViewModels.MemberBank;

namespace NTKServer.Business
{
    public class MemberBankBiz
    {
        #region CRUD
		public static List<MemberBankList> GetMemberBankList(int member)
        {
            return MemberBankService.FindMemberBankList(member);
        }

        public static MemberBankDto Get(string card_pk)
        {
            return MemberBankService.Find(card_pk);
        }

        public static void PostCreate(MemberBankDto req)
        {
            if (MemberBankService.Insert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(MemberBankDto req)
        {
            if( MemberBankService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void PostEditIsConfirm(MemberBankDto req, bool is_confirm)
        {
            if (MemberBankService.UpdateIsConfirm(req.card_pk, is_confirm) == 0)
            {
                throw new AppException("更新提款卡是否核实失败");
            }
        }

        public static void PostEditIsDelete(MemberBankDto req, bool is_confirm)
        {
            if (MemberBankService.UpdateIsDelete(req.card_pk, is_confirm) == 0)
            {
                throw new AppException("更新提款卡是否删除失败");
            }
        }

        public static void Delete(string card_pk)
        {
            MemberBankService.Remove(card_pk);
        }

        #endregion
		
		#region ViewModel		
        public static MemberBankReview GetReview(string card_pk)
        {
            var review = MemberBankService.FindMemberBankReview(card_pk);
            var host = ConfigLib.Get("filesite");
            review.card_front = host + review.card_front;

            return review;
        }


		#endregion		
	}
}
