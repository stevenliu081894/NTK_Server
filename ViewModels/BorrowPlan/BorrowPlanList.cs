using NTKServer.Internal;

namespace NTKServer.ViewModels.BorrowPlan
{
    public class BorrowPlanList
    {
        /// <summary> pk: # </summary>
        public int pk { get; set; }

        /// <summary> market: 交易市场别 </summary>
        public string market { get; set; }

        /// <summary> enable: 是否啟用 </summary>
        public bool enable { get; set; }

        /// <summary> borrow_type: 融资类型 </summary>
        public string borrow_type { get; set; }

        /// <summary> name: 融资名称 </summary>
        public string name { get; set; }

    }
}
