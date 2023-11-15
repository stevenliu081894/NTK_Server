using NTKServer.Internal;

namespace NTKServer.ViewModels.BorrowPlan
{
    public class BorrowPlanFilter
    {
        /// <summary> market: 交易市场别 </summary>
        [Where("=", "market")]
        public string? market { get; set; }

    }
}
