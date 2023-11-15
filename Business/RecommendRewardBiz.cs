using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.RecommendReward;

namespace NTKServer.Business
{
    public class RecommendRewardBiz
    {
        #region CRUD
		public static List<RecommendRewardList> GetRecommendRewardList(RecommendRewardFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return RecommendRewardService.FindRecommendRewardList(whereSql);
        }

        public static RecommendRewardDto Get(int pk)
        {
            return RecommendRewardService.Find(pk);
        }

        public static void PostCreate(RecommendRewardDto req)
        {
            if (RecommendRewardService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(RecommendRewardDto req)
        {
            if( RecommendRewardService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            RecommendRewardService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
