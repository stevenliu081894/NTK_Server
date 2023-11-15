using DB.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Org.BouncyCastle.Ocsp;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.ViewModels.BorrowAddmoney;
using NTKServer.ViewModels.MemberBank;
using NTKServer.ViewModels.Wallet;

namespace NTKServer.Controllers
{
	public class MemberBankController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[UseFilter(272, 5)]
		[TranslatorUIFilter("MemberBank")]
        public IActionResult Index(int member)
        {
            SetFilterSelect();

            try
            {
				List<MemberBankList> list = MemberBankBiz.GetMemberBankList(member);
                return View(list);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(new List<MemberBankList>());
            }
        }


		[UseFilter(272, 5)]
		[TranslatorUIFilter("MemberBank")]		
		public IActionResult Review(string card_pk)
        {
            MemberBankReview model = MemberBankBiz.GetReview(card_pk);
            return View(model);
        }

        public IActionResult PostReview(MemberBankDto req, bool result)
        {
            try
            {
                int tempId = result == true ? 110 : 111;

                if (result == true)
                {
                    // 处理审核同意逻辑
                    MemberBankBiz.PostEditIsConfirm(req,true);
                    MemberBankBiz.PostEditIsDelete(req, false);
                    MemberTaskLib.MemberTaskFinish(req.member_fk, 7, "CN"); // 首次绑提现银行卡

                    // 寄送站內信
                    SendMessageLib.Send(req.member_fk, tempId, new object[2] {
                        req.card_type,
                        req.card
                    });
                }
                else
                {
                    // 处理审核不同意逻辑
                    MemberBankBiz.PostEditIsConfirm(req, false);
                    MemberBankBiz.PostEditIsDelete(req, true);

                    // 寄送站內信
                    SendMessageLib.Send(req.member_fk, tempId, new object[2] {
                        req.card_type,
                        req.card
                    });
                }
                return RedirectToAction("Index", new { member = req.member_fk });
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Review", req);
            }
        }
    }	
}
