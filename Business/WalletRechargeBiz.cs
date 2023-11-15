using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Tool;
using NTKServer.ViewModels.WalletRecharge;
using NTKServer.ViewModels.WalletRecord;
using Microsoft.CodeAnalysis.Text;

namespace NTKServer.Business
{
    public class WalletRechargeBiz
    {
        #region CRUD
		public static List<WalletRechargeList> GetWalletRechargeList(WalletRechargeFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must("wallet_recharge.status = 0");
            return WalletRechargeService.FindWalletRechargeList(whereSql);
        }

        public static WalletRechargeDto Get(int pk)
        {
            return WalletRechargeService.Find(pk);
        }

        public static void PostCreate(WalletRechargeDto req)
        {
            if (WalletRechargeService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(WalletRechargeDto req)
        {
            if( WalletRechargeService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            WalletRechargeService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		
        public static WalletRechargeReview GetReview(int pk)
        {
            return WalletRechargeService.FindWalletRechargeReview(pk);
        }


        #endregion

        /// <summary>
        /// 審核充值
        /// </summary>
        public static bool RechargeVerify(int pk, bool verifyStatus, int adminPk, string rejectResult)
        {
            if (!verifyStatus && rejectResult == null) {
                throw new AppException("請填寫拒絕原因");
            }

            var record = WalletRechargeService.Find(pk);

            if (record.status != (int)VerifyStatus.Processing)
            {
				throw new AppException(3010, "verify_status_incorrect");
			}

            if (verifyStatus)
            {
                WalletLib.RechargePass(record.member_fk, record.wallet_amount, record.order_no, record.money, record.currency, record.exchange);
				WalletRechargeService.VerifyRecharge(pk, adminPk, (int)VerifyStatus.Success, null);
                MemberTaskLib.MemberTaskFinish(record.member_fk, 3, "CN"); // 首次充值
                #region 寄送站內信

                // 申請單號|金额|币别|汇率
                object[]? list2 = new object[4];
				list2[0] = record.order_no;
				list2[1] = record.wallet_amount;
				list2[2] = ConfigLib.Get("wallet_currency");
				list2[3] = record.exchange;
				SendMessageLib.Send(record.member_fk, 1, list2);

				#endregion
            }
            else
            {
				WalletRechargeService.VerifyRecharge(pk, adminPk, (int)VerifyStatus.Fail, rejectResult);

				#region 寄送站內信

				// 1. 寄站內信
				// 申請單號|金额|币别|汇率|拒絕原因
				object[]? list2 = new object[5];
				list2[0] = record.order_no;
				list2[1] = record.wallet_amount;
				list2[2] = ConfigLib.Get("wallet_currency");
				list2[3] = record.exchange;
				list2[4] = rejectResult;
				SendMessageLib.Send(record.member_fk, 2, list2);

				#endregion
            }

            return true;
        }

        /// <summary>
        /// 調整異常
        /// </summary>
        /// <param name="req"></param>
        /// <exception cref="AppException"></exception>
        public static void ChangeException(WalletChangeExceptionVm req)
        {
            if (WalletLib.AdminOperate(req.member_fk, req.change_balance, req.change_coupon, req.reason))
            {
                throw new AppException(3010, "record_update_false");
            }
        }
    }
}
