using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.WalletTemplate;

namespace NTKServer.Business
{
    public class WalletTemplateBiz
    {
        #region CRUD
		public static List<WalletTemplateList> GetWalletTemplateList(WalletTemplateFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return WalletTemplateService.FindWalletTemplateList(whereSql);
        }

        public static WalletTemplateDto Get(int pk)
        {
            return WalletTemplateService.Find(pk);
        }

        public static void PostCreate(WalletTemplateDto req)
        {
            if (WalletTemplateService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(WalletTemplateDto req)
        {
            if( WalletTemplateService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            WalletTemplateService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		

        public static WalletTemplateEditVm GetEditVm(int pk)
        {
            return WalletTemplateService.FindWalletTemplateEditVm(pk);
        }

		#endregion		
	}
}
