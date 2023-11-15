namespace NTKServer.ViewModels.StatBalance
{
    public class StatBalanceSummary
    {
        /// <summary> recharge_count: ��ֵ���� </summary>
        public int recharge_count { get; set; }
        /// <summary> recharge: ��ֵ�ϼ� </summary>
        public decimal recharge_total { get; set; }

        /// <summary> withdraw: ���ֱ��� </summary>
        public int withdraw_count { get; set; }

        /// <summary> withdraw: ���ֺϼ� </summary>
        public decimal withdraw_total { get; set; }
    }
}
