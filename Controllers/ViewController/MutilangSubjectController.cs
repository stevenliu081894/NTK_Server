using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using X.PagedList;
using System.Web;

namespace NTKServer.Controllers
{
    public class MutilangSubjectController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(228, 9)]
		[TranslatorUIFilter("MutilangSubject")]
        public IActionResult Index()
        {
            SetFilterSelect();

            try
            {
                List<MutilangSubjectDto> model = MutilangSubjectBiz.GetMutilangSubjectList();
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(new List<MutilangSubjectDto>());
            }
        }

        public void SetSelect()
        {
		}

		[UseFilter(228, 9)]
		[TranslatorUIFilter("MutilangSubject")]		
		public IActionResult Edit(string lang)
        {
            SetSelect();
            MutilangSubjectDto dto = MutilangSubjectBiz.Get(lang);
            return View(dto);
        }

        public IActionResult PostEdit(MutilangSubjectDto req)
        {
            SetSelect();
            try
            {
				MutilangSubjectBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(228, 9)]
		[TranslatorUIFilter("MutilangSubject")]
        public IActionResult Create()
        {
            SetSelect();
            MutilangSubjectDto dto = new();
            return View(dto);
        }

        public IActionResult PostCreate(MutilangSubjectDto req)
        {
            SetSelect();
            try
            {
				MutilangSubjectBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
        }

		[UseFilter(228, 9)]
        public IActionResult Delete(string lang)
        {
            try
            {
                MutilangSubjectBiz.Delete(lang);
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
