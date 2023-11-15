using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.Ipwhitelist;

namespace NTKServer.Business
{
    public class IpwhitelistBiz
    {
        #region CRUD
		public static List<IpwhitelistList> GetIpwhitelistList(IpwhitelistFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return AdminIpwhitelistService.FindIpwhitelistList(whereSql);
        }

        public static AdminIpwhitelistDto Get(string ip)
        {
            return AdminIpwhitelistService.Find(ip);
        }

        public static void PostCreate(AdminIpwhitelistDto req)
        {
            if (AdminIpwhitelistService.Insert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(AdminIpwhitelistDto req)
        {
            if( AdminIpwhitelistService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(string ip)
        {
            AdminIpwhitelistService.Remove(ip);
        }

        #endregion
	}
}
