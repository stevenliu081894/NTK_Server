using Microsoft.AspNetCore.Mvc;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.MultiLang;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class MultiLangLangController : BaseController
    {
        private void SetPermissions()
        {
            var user = GetUser();
            ViewData["multilang_lang_edit_btn"] = AdminRoleBiz.VerifyPower(427, 9, user.role);
        }

        #region 语系管理
        [MenuFilter(426, 9)]
        public IActionResult Index()
        {
            MultiLangLangSearchVm req = new MultiLangLangSearchVm();
            try
            {
                req.list = MultiLangBiz.GetSearchList().ToPagedList(req.page, pageSize); ;
                SetPermissions();
                return View("Index", req);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Index", req);
            }
        }

        /// <summary>
        /// 進入編輯畫面
        /// </summary>
        [UseFilter(427, 9)]
        public IActionResult Edit(string lang)
        {
            var dto = MultiLangBiz.GetByLang(lang);
            MultiLangLangVm langVm = new MultiLangLangVm
            {
                lang = dto.lang,
                title = dto.title,
                enable = dto.enable,
                admin_default = dto.admin_default,
                app_default = dto.app_default
            };
            return View(langVm);
        }

        /// <summary>
        /// 展示有錯誤發生, 如何通知 UI 使用者
        /// </summary>
        public IActionResult PostEdit(MultiLangLangVm req)
        {
            try
            {
                MultiLangBiz.Edit(req);

                if (req.isSucceed == false)
                {
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
        #endregion
    }
}
