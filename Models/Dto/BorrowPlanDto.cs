using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class BorrowPlanDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 是否啟用 </summary>
        public bool enable { get; set; }
        /// <summary> 融资类型 </summary>
        public string borrow_type { get; set; }
        /// <summary> 交易市场别 </summary>
        public string market { get; set; }
        /// <summary> 融资名称 </summary>
        public string name { get; set; }
        /// <summary> 多国语言档 </summary>
        public string lang { get; set; }
        /// <summary> 管理费费率 </summary>
        public string rate { get; set; }
        /// <summary> 预警线 </summary>
        public double warning_line { get; set; }
        /// <summary> 平仓线 </summary>
        public double break_line { get; set; }
        /// <summary> 单股持仓比重 </summary>
        public string max_proporting { get; set; }
        /// <summary> 是否可续期 </summary>
        public bool renewal { get; set; }
        /// <summary> 使用期限 </summary>
        public string use_time { get; set; }
        /// <summary> 保证金最小值 </summary>
        public double money_range_min { get; set; }
        /// <summary> 保证金最大值 </summary>
        public double money_range_max { get; set; }
        /// <summary> (UI使用)金钱点击增加量 </summary>
        public double money_range_increase { get; set; }
        /// <summary> (UI使用)保证金快捷按钮 </summary>
        public string fastbtn { get; set; }
        /// <summary> 宣传词 </summary>
        public string slogan { get; set; }
        /// <summary> 页面显示的注意事项 </summary>
        public string note { get; set; }
        /// <summary> 特殊设置储存处 </summary>
        public string unique_set { get; set; }
    }
}
