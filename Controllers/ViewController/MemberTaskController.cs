using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.MemberTask;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class MemberTaskController : BaseController
    {
        public void SetSelect()
        {
            ViewBag.langDropdown = MultiLangBiz.FindSelectList();
        }

        [MenuFilter(67, 9)]
		[TranslatorUIFilter("MemberTask")]
        public IActionResult Index(MemberTaskFilter filter, int page = 1)
        {
            SetSelect();
            MemberTaskVm model = new()
            {
                filter = filter ?? new MemberTaskFilter()
            };

            try
            {
                model.list = MemberTaskBiz.GetMemberTaskList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[UseFilter(67, 9)]
		[TranslatorUIFilter("MemberTask")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            MemberTaskDto dto = MemberTaskBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(MemberTaskDto req)
        {
            SetSelect();
            try
            {
				MemberTaskBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(67, 9)]
		[TranslatorUIFilter("MemberTask")]
        public IActionResult Create()
        {
            SetSelect();
            MemberTaskDto dto = new();
            return View(dto);
        }

        public IActionResult PostCreate(MemberTaskDto req)
        {
            SetSelect();
            try
            {
				MemberTaskBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
        }

		[UseFilter(67, 9)]
        public IActionResult Delete(int pk)
        {
            try
            {
                MemberTaskBiz.Delete(pk);
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return RedirectToAction("Index");
            }
        }
    }	
}
