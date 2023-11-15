using DB.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.SysMarket;

namespace NTKServer.Business
{
    public class SysMarketBiz
    {
        #region CRUD
		public static List<SysMarketList> GetSysMarketList()
        {
            return SysMarketService.FindSysMarketList();
        }

        public static SysMarketDto Get(string code)
        {
            return SysMarketService.Find(code);
        }

        public static void PostCreate(SysMarketDto req)
        {
            if (SysMarketService.Insert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(SysMarketDto req)
        {
            if( SysMarketService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(string code)
        {
            SysMarketService.Remove(code);
        }

        #endregion

        #region ViewModel		
        public static List<SelectListItem> GetDropDownList(string lang)
        {
            var list = new List<SelectListItem>();
            var markets = SysMarketService.FindDropDown(lang);
            foreach (var market in markets)
            {
                list.Add(new SelectListItem()
                {
                    Value = market.code,
                    Text = $"{market.name}({market.code})"
                });
            }
            return list;
        }
        #endregion
    }
}
