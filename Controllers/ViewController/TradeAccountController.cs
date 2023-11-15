using Microsoft.AspNetCore.Mvc;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.TradeAccount;
using X.PagedList;

namespace NTKServer.Controllers
{
	public class TradeAccountController : BaseController
    {
        private void SetPermissions()
        {
            var user = GetUser();
            ViewData["position_btn"] = AdminRoleBiz.VerifyPower(394, 2, user.role);//持仓列表
            ViewData["entrusted_btn"] = AdminRoleBiz.VerifyPower(393, 2, user.role);//本日委託
            ViewData["deal_btn"] = AdminRoleBiz.VerifyPower(389, 2, user.role);//本日交割
            ViewData["stop_btn"] = AdminRoleBiz.VerifyPower(366, 2, user.role);//撤销紀錄
            ViewData["money_flow_btn"] = AdminRoleBiz.VerifyPower(391, 2, user.role);//資金紀錄
        }

        #region 交易中账号

        [MenuFilter(390, 2)]
        public IActionResult TradeAccountIndex()
        {
            TradeAccountSearchVm req = new TradeAccountSearchVm();
            try
            {
                req.list = TradeAccountBiz.GetSearchList(req.filter).ToPagedList(1, pageSize);
                SetPermissions();
                return View("TradeAccountIndex", req);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("TradeAccountIndex", req);
            }
        }

        [MenuFilter(390, 2)]
        public IActionResult TradeAccountIndexPostSearch(TradeAccountSearchFilter request, int page = 1)
        {
            TradeAccountSearchVm req = new TradeAccountSearchVm()
            {
                filter = request
            };
            try
            {
                req.list = TradeAccountBiz.GetSearchList(req.filter).ToPagedList(page, pageSize);
                return View("TradeAccountIndex", req);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("TradeAccountIndex", req);
            }
        }

        #endregion


        #region 本日交割

        /// <summary>
        /// 本日交割
        /// </summary>   
        [MenuFilter(389, 2)]
        public IActionResult DealIndex(string sub_account)
        {
            DealSearchVm req = new DealSearchVm();
            req.filter = new DealSearchFilter();
            req.filter.sub_account = string.IsNullOrWhiteSpace(sub_account) ? null : sub_account;
            try
            {
                req.list = TradeDealBiz.GetTradeDealSearchList(req.filter).ToPagedList(1, pageSize);
                SetPermissions();
                return View("DealIndex", req);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("DealIndex", req);
            }
        }

        /// <summary>
        /// 本日交割
        /// </summary>
        /// <param name="dataTableParameters"></param>
        public IActionResult DealIndexPostSearch(DealSearchFilter request, int page = 1)
        {
            DealSearchVm req = new DealSearchVm()
            {
                filter = request
            };
            try
            {
                req.list = TradeDealBiz.GetTradeDealSearchList(req.filter).ToPagedList(page, pageSize);
                return View("DealIndex", req);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("DealIndex", req);
            }
        }
        #endregion

        #region 本日委託
        /// <summary>
        /// 撤單
        /// </summary>
        public IActionResult CancelOrder(TradeEntrustedVm req)
        {
            try
            {
                var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                var user = GetUser();
                TradeOrderBiz.CancelOrder(req, ip, user.nickName);
                return RedirectToAction("TradeEntrustedIndex");
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return RedirectToAction("TradeEntrustedIndex", req);
            }
        }

        #endregion
    }
}
