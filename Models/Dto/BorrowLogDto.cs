using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class BorrowLogDto
    {
        /// <summary> 配资FK </summary>
        public int borrow_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 添加时间 </summary>
        public DateTime add_time { get; set; }
        /// <summary> 备注 </summary>
        public string remark { get; set; }
        /// <summary> 日志类型 </summary>
        public int logtype { get; set; }
    }
}
