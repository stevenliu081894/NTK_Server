using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.MultiLang;
using X.PagedList;


namespace NTKServer.Controllers
{
    public class MutilangController : BaseController
    {
		private void SetSelect()
		{
			ViewBag.langDropdown = MultiLangBiz.FindSelectList();
		}

		[TranslatorUIFilter("Mutilang")]
        [MenuFilter(8, 9)]
        public IActionResult MultilangIndex(MultilangFilter filter, int page = 1)
        {
			SetSelect();
            MultilangVm model = new MultilangVm()
            {
                filter = filter ?? new MultilangFilter(),
            };
            try
            {
                model.list = MultiLangBiz.GetList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

        [TranslatorUIFilter("Mutilang")]
        [MenuFilter(8, 9)]
        public IActionResult MultilangTranslate(string key, MultiLangCacheSearchFilter Filter, int page = 1)
        {
			SetSelect();
            MultiLangCacheSearchVm model = new MultiLangCacheSearchVm()
            {
                filter = Filter ?? new MultiLangCacheSearchFilter() { 
                    key= key,
                }
            };
            try
            {
                model.list = MultiLangBiz.GetMultiLangCacheList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

        [TranslatorUIFilter("Mutilang")]
        [MenuFilter(8, 9)]
        public IActionResult MultilangSync(string key, MultiLangCacheSearchFilter Filter, int page = 1)
        {
            SetSelect();
            var model = new MultiLangCacheSearchVm()
            {
                filter = Filter ?? new MultiLangCacheSearchFilter()
                {
                    key = key,
                }
            };
            try
            {
                // ����P�B
                MultiLangBiz.MultilangSync(key);

                model.list = MultiLangBiz.GetMultiLangCacheList(model.filter).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                ShowWarning(ex.Message);
            }
            return RedirectToAction("MultilangTranslate", new { key });
        }

        [TranslatorUIFilter("Mutilang")]
        [MenuFilter(8, 9)]
        public IActionResult MultilangTranslateEdit(string key,string lang, string valKey)
        {
            var dto = MultiLangBiz.GetTranslateByKey(key,lang,valKey);
            return View(dto);
        }

        [TranslatorUIFilter("Mutilang")]
        [MenuFilter(8, 9)]
        public IActionResult MultilangTranslateEditPost(MultiLangCacheEditVm req)
        {
            try
            {
                MultiLangBiz.EditMultilangTranslate(req);
                return RedirectToAction("MultilangTranslate", new { req.key });
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return RedirectToAction("MultilangTranslateEdit", new { req.key, req.lang, valKey = req.translationKey });
            }
        }

    }
}
