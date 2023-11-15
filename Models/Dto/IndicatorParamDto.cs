using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class IndicatorParamDto
    {
        /// <summary> 會員FK </summary>
        public int member_fk { get; set; }
        /// <summary> 指標參數PK </summary>
        public int pk { get; set; }
        /// <summary> 指標名稱(SMA1/SMA2/SMA3/KD/MACD/RSI) </summary>
        public string name { get; set; }
        /// <summary> 参数1(没使用的参数填0) </summary>
        public double param1 { get; set; }
        /// <summary> 参数2(没使用的参数填0) </summary>
        public double param2 { get; set; }
        /// <summary> 参数3(没使用的参数填0) </summary>
        public double param3 { get; set; }
        /// <summary> 参数4(没使用的参数填0) </summary>
        public double param4 { get; set; }
        /// <summary> 参数5(没使用的参数填0) </summary>
        public double param5 { get; set; }
    }
}
