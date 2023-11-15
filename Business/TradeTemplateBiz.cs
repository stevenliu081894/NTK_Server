using DB.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NTKServer.Business
{
    public class TradeTemplateBiz
    {
        #region ViewModel		
        public static List<SelectListItem> GetDropDownList(string lang)
        {
            var list = new List<SelectListItem>();
            var temp = TradeTemplateService.FindDropDown(lang);
            foreach (var item in temp)
            {
                list.Add(new SelectListItem()
                {
                    Value = $"{item.temp_id}",
                    Text = $"{item.name}"
                });
            }
            return list;
        }
        #endregion
    }
}
