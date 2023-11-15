using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Dto;
using Org.BouncyCastle.Ocsp;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.ViewModels.AdminUser;
using NTKServer.ViewModels.ReviewMember;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class ReviewMemberController : BaseController
    {
        public void SetSelect()
        {
		}

        [MenuFilter(249, 4)]
        [TranslatorUIFilter("ReviewMember")]
        public IActionResult Index(ReviewMemberFilter filter, int page = 1)
        {
			SetSelect();

            ReviewMemberVm model = new()
            {
                filter = filter ?? new ReviewMemberFilter()
            };

            try
            {
                model.list = ReviewMemberBiz.GetReviewMemberList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

        [UseFilter(249, 4)]
        [TranslatorUIFilter("ReviewMember")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            MemberDto dto = ReviewMemberBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(MemberDto req)
        {
            SetSelect();
            try
            {
				ReviewMemberBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

        [UseFilter(249, 4)]
        public IActionResult Delete(int pk)
        {
            try
            {
                ReviewMemberBiz.Delete(pk);
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return RedirectToAction("Index");
            }
        }

		[MenuFilter(249, 4)]
		[TranslatorUIFilter("ReviewMember")]
        public IActionResult VerifyMember(int pk)
        {
            var filter = new AdminUserFilter();
            filter.role = 25;
			var cs = AdminUserBiz.GetAdminUserList(filter);
            List<SelectListItem> items = new List<SelectListItem>();
               
			foreach (var user in cs)
            {
                items.Add(new SelectListItem() { Text = user.nickname, Value = $"{user.pk}" });
            }

			ViewBag.AdminUser = items;

			VerifyMemberVm vm = ReviewMemberBiz.GetVerifyMember(pk);
            vm.card_pic_front = ConfigLib.Get("filesite") + vm.card_pic_front;
			return View(vm);
		}

		[MenuFilter(249, 4)]
		[TranslatorUIFilter("ReviewMember")]
		public IActionResult VerifyMemberPost(VerifyMemberVm req, int result)
        {
            try
            {
                ReviewMemberBiz.VerifyMember(req, GetUser().pk);
                return RedirectToAction("Index");
			}
			catch (AppException ex)
            {
				ShowWarning(ex.Message);
				return View(req);
			}
		}


	}	
}
