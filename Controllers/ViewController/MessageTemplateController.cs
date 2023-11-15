using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.MessageTemplate;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class MessageTemplateController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(31, 14)]
		[TranslatorUIFilter("MessageTemplate")]
        public IActionResult Index(MessageTemplateFilter filter, int page = 1)
        {
            SetFilterSelect();

            MessageTemplateVm model = new()
            {
                filter = filter ?? new MessageTemplateFilter()
            };

            try
            {
                model.list = MessageTemplateBiz.GetMessageTemplateList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

        public void SetSelect()
        {
		}

		[UseFilter(31, 14)]
		[TranslatorUIFilter("MessageTemplate")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            MessageTemplateDto dto = MessageTemplateBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(MessageTemplateDto req)
        {
            SetSelect();
            try
            {
				MessageTemplateBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(31, 14)]
		[TranslatorUIFilter("MessageTemplate")]
        public IActionResult Create()
        {
            SetSelect();
            MessageTemplateDto dto = new();
            return View(dto);
        }

        public IActionResult PostCreate(MessageTemplateDto req)
        {
            SetSelect();
            try
            {
				MessageTemplateBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
        }
    }	
}
