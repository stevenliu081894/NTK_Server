using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.ReviewMember;
using NTKServer.ViewModels.RewardDetail;

namespace NTKServer.Business
{
    public class RewardDetailBiz
    {
        #region CRUD
		public static List<RewardDetailList> GetRewardDetailList(int id)
        {
            return RecommendRewardDetailService.FindRewardDetailList(id)?
                .Select(recommendReward => PublicTool.convertUtcToLocalTime(recommendReward)).ToList();
        }

        public static RecommendRewardDetailDto Get(int pk)
        {
            return RecommendRewardDetailService.Find(pk);
        }

        public static void PostCreate(RecommendRewardDetailDto req)
        {
            if (RecommendRewardDetailService.Insert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(RecommendRewardDetailDto req)
        {
            if( RecommendRewardDetailService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            RecommendRewardDetailService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
