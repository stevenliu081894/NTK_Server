using DB.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.BorrowPlan;

namespace NTKServer.Business
{
    public class BorrowPlanBiz
    {
        #region CRUD

        public static List<SelectListItem> GetSelectListItems()
        {
            var plans = BorrowPlanService.FindAll();
            var list=new List<SelectListItem>();
            foreach (var plan in plans)
            {
                list.Add(new SelectListItem()
                {
                    Value = $"{plan.pk}",
                    Text = $"{plan.name}({plan.market})"
                });
            }
            return list;
		}

		public static List<BorrowPlanList> GetBorrowPlanList(BorrowPlanFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return BorrowPlanService.FindBorrowPlanList(whereSql) ?? new List<BorrowPlanList>();
        }

        public static BorrowPlanDto Get(int pk)
        {
            var plan = BorrowPlanService.Find(pk);
            plan.use_time = plan.use_time.Replace("|", "\r\n");
            return plan;
		}

        public static void PostCreate(BorrowPlanDto req)
        {
            if (BorrowPlanService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(BorrowPlanDto req)
        {
            req.use_time = req.use_time.Replace("\r\n", "|");
            if( BorrowPlanService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            BorrowPlanService.Remove(pk);
        }

        #endregion
	}
}
