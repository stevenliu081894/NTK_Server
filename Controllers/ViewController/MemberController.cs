using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.Member;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class MemberController : BaseController
    {
        public void SetSelect()
        {
        }

        [MenuFilter(250, 4)]
		[TranslatorUIFilter("Member")]
        public IActionResult Index(MemberFilter filter, int page = 1)
        {
            SetSelect();
            MemberVm model = new()
            {
                filter = filter ?? new MemberFilter()
            };

            try
            {
                model.list = MemberBiz.GetMemberList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[UseFilter(250, 4)]
		[TranslatorUIFilter("Member")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            MemberDto dto = MemberBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(MemberDto req)
        {
            SetSelect();
            try
            {
				MemberBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }
    }	
}
