using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.RecommendRegister;

namespace NTKServer.Business
{
    public class RecommendRegisterBiz
    {
        #region CRUD
		public static List<RecommendRegisterList> GetRecommendRegisterList(RecommendRegisterFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            var begin = string.Empty;
            var end = string.Empty;
            if (filter.year.HasValue && filter.month.HasValue)
            {
                begin = $"{filter.year}/{filter.month}/01 00:00:00";
                end = Convert.ToDateTime(begin).AddMonths(1).ToString("yyyy/MM/dd 00:00:00");
                whereSql = SqlTool.Build(filter).Must($"date('{begin}') <= create_date And create_date < date('{end}')");
            }
            else if (filter.year.HasValue)
            {
                begin = $"{filter.year}/01/01 00:00:00";
                end = $"{filter.year}/12/31 23:59:59";
                whereSql = SqlTool.Build(filter).Must($"date('{begin}') <= create_date And create_date <= date('{end}')");
            }
            else if(filter.month.HasValue)
            {
                begin = $"{DateTime.Now.Year}/{filter.month}/01 00:00:00";
                end = Convert.ToDateTime(begin).AddMonths(1).ToString("yyyy/MM/dd 00:00:00");
                whereSql = SqlTool.Build(filter).Must($"date('{begin}') <= create_date And create_date < date('{end}')");
            }

            return RecommendRegisterService.FindRecommendRegisterList(whereSql);
        }

        public static RecommendRegisterDto Get(int member_fk)
        {
            return RecommendRegisterService.Find(member_fk);
        }

        public static void PostCreate(RecommendRegisterDto req)
        {
            if (RecommendRegisterService.Insert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(RecommendRegisterDto req)
        {
            if( RecommendRegisterService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int member_fk)
        {
            RecommendRegisterService.Remove(member_fk);
        }

		#endregion

		#region ViewModel		
		public static RecommendRegisterList GetDetailVm(int beinvite)
		{
			return RecommendRegisterService.FindRecommendRegister(beinvite);
		}

		#endregion
	}
}
