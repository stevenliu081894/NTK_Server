using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
using StackExchange.Redis;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.Demo;
using System.Collections.Generic;
using System.Security.Cryptography;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class DemoController : BaseController
    {
        /// <summary>
        /// (1)如何設置權限以及(2)错误异常返回的处理 范本
        /// </summary>
        /// 
        #region 权限处理范本
        private void SetPermissions()
        {
            var user = GetUser();
            ViewData["btn1"] = AdminRoleBiz.VerifyPower(131, 14, user.role);
            ViewData["btn2"] = AdminRoleBiz.VerifyPower(132, 14, user.role);
        }


        [MenuFilter(130, 14)]
        public IActionResult Index(int page = 1)
        {
            try
            {
                //------------------------------------------------
                // 這只是範例, 實際抓資料請從 Business 處理
                List<DemoMemberListVm> users = new();
                for (int idxUser = 1; idxUser <= 10000; idxUser++)
                {
                    DemoMemberListVm userInfo = new DemoMemberListVm()
                    {
                        id = idxUser,
                        account = "Jack" + (idxUser + 1).ToString(),
                        realName = "Kaba" + (idxUser + 1).ToString(),

                    };
                    users.Add(userInfo);
                }
                //------------------------------------------------

                SetPermissions();
                var response = users.ToPagedList(page, pageSize);
                return View(response);
            }
            catch (AppException ex) 
            {
                ShowWarning(ex.Message);
                return View();
            }
        }

        /// <summary>
        /// 進入編輯畫面
        /// </summary>
        [UseFilter(131,14)]
        public IActionResult Edit(int id)
        {
            //------------------------------------------------
            // 這只是範例, 實際抓資料請從 Business 處理
            DemoMemberVm member = new()
            {
                id = id,
                account = "Jack" + id.ToString(),
                realName = "Kaba" + id.ToString(),
                isSucceed = true
            };
            member.email = member.account + "@gamil.com";
            member.birthday = new DateTime(1989, 9, 8);
            //------------------------------------------------

            return View(member);
        }

        /// <summary>
        /// 展示有錯誤發生, 如何通知 UI 使用者
        /// </summary>
        public IActionResult PostEdit(DemoMemberVm req) 
        {
            try
            {
                // 這裡處理儲存的事務

                // 範例如何處理錯誤
                if (req.isSucceed == false)
                {
                    // 有三種可以使用
                    // ShowMessage("一般訊息");
                    // ShowWarning("警告");
                    // ShowError("錯誤");
                    ShowWarning("某某內容錯誤, 請修正");
                    return View("Edit", req);
                }

                return RedirectToAction("Index");
            }
            catch (AppException ex) 
            {
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

        /// <summary>
        /// 展示權限控制功能
        /// </summary>
        [UseFilter(132,14)]
        public IActionResult Details(int id) 
        {
            // 請分別用一般客服和客服主管登入看結果
            // (一般客服沒有權限使用本功能, 客服主管有權限使用)
            // 處理方法 ==> 上面加 [UseFilter(權限編號, 模組編號)]

            try
            {
                return RedirectToAction("Edit", id);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return RedirectToAction("Index");
            }
        }

        #endregion


        /// <summary>
        /// 处理多重条件条件查询范本
        /// </summary>
        #region 条件搜寻范本

        [MenuFilter(133, 14)]
        public IActionResult SearchIndex()
        {
            DemoSearchVm req = new DemoSearchVm();
            try
            {
                req.list = DemoBiz.GetSearchList(req.filter).ToPagedList(1, pageSize);
                return View("SearchIndex", req);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("SearchIndex", req);
            }
        }

        [MenuFilter(133, 14)]
        public IActionResult PostSearch(DemoSearchFilter filter, int page = 1)
        {
            DemoSearchVm req = new()
            {
                filter = filter
            };

            try
            {
                req.list = DemoBiz.GetSearchList(req.filter).ToPagedList(page, pageSize);
                return View("SearchIndex",req);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("SearchIndex",req);
            }
        }

        #endregion
    }
}
