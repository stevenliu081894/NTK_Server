using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.ViewModels.MessageRecord;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class MessageRecordController : BaseController
    {
        private void SetFilterSelect()
        {			
        }

		[MenuFilter(85, 6)]
		[TranslatorUIFilter("MessageRecord")]
        public IActionResult Index(MessageRecordFilter filter, int page = 1)
        {
            SetFilterSelect();

            MessageRecordVm model = new()
            {
                filter = filter ?? new MessageRecordFilter()
            };

            try
            {
                model.list = MessageRecordBiz.GetMessageRecordList(model.filter).ToPagedList(page, pageSize);
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
            List<SelectListItem> items = new()
            {
                new SelectListItem { Text = "無", Value = "0" },
                new SelectListItem { Text = "會員", Value = "1" },
                new SelectListItem { Text = "管理員", Value = "2" },
                new SelectListItem { Text = "營運商", Value = "3" },
            };

            ViewBag.AccountType = items;

            List<SelectListItem> items2 = new()
            {
                new SelectListItem { Text = "站内信", Value = "1" },
                new SelectListItem { Text = "郵箱", Value = "2" },
                new SelectListItem { Text = "簡訊", Value = "3" },
            };

            ViewBag.SendType = items2;
		}

		[MenuFilter(85, 6)]
		[TranslatorUIFilter("MessageRecord")]		
		public IActionResult Detail(int pk)
        {
            SetSelect();

			MessageRecordEditVm dto = MessageRecordBiz.GetEditVm(pk);
            dto.info = UploadImageLib.AddHostName(dto.info);
            return View(dto);
        }

		[UseFilter(85, 6)]
		[TranslatorUIFilter("MessageRecord")]
        public IActionResult Create(int member)
        {
            SetSelect();
            MessageRecordEditVm dto = MessageRecordBiz.GetAppendVm(member);           
            return View(dto);
        }

        public IActionResult PostCreate(MessageRecordDto req)
        {
            SetSelect();
            try
            {
                req.info = UploadImageLib.RemoveHostName(req.info);
				MessageRecordBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile upload)
        {
            try
            {
                var img_path = await UploadBiz.UploadImage(upload, FileManagementLib.Folder.message);
                var success = new UploadSuccess
                {
                    uploaded = 1,
                    url = img_path
                };

                return new JsonResult(success);
            }
            catch (AppException ex)
            {
                ShowError(ex.Message);
                return View();
            }
        }

		[UseFilter(85, 6)]
        public IActionResult Delete(int pk)
        {
            try
            {
                MessageRecordBiz.Delete(pk);
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
