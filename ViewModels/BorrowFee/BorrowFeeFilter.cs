using NTKServer.Internal;

namespace NTKServer.ViewModels.BorrowFee
{
    public class BorrowFeeFilter
    {
        /// <summary> begin_time: 起始时间 </summary>
        [Where(">=", "date(create_time)")]
        public DateTime? begin_time { get; set; }

        /// <summary> end_time: 结束时间 </summary>
        [Where("<", "date(create_time)")]
        public DateTime?  end_time { get; set; }

    }
}
