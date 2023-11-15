using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class AdminConfigDto
    {
        /// <summary> 名称 </summary>
        public string name { get; set; }
        /// <summary> 标题 </summary>
        public string title { get; set; }
        /// <summary> 配置分组 </summary>
        public string group { get; set; }
        /// <summary> 类型 </summary>
        public string type { get; set; }
        /// <summary> 配置值 </summary>
        public string value { get; set; }
        /// <summary> 配置项 </summary>
        public string options { get; set; }
        /// <summary> 配置提示 </summary>
        public string tips { get; set; }
        /// <summary> 联动下拉框ajax地址 </summary>
        public string ajax_url { get; set; }
        /// <summary> 联动下拉框的下级下拉框名，多个以逗号隔开 </summary>
        public string next_items { get; set; }
        /// <summary> 联动下拉框请求参数名 </summary>
        public string param { get; set; }
        /// <summary> 格式，用于格式文本 </summary>
        public string format { get; set; }
        /// <summary> 表名，只用于快速联动类型 </summary>
        public string table { get; set; }
        /// <summary> 联动级别，只用于快速联动类型 </summary>
        public int level { get; set; }
        /// <summary> 键字段，只用于快速联动类型 </summary>
        public string key { get; set; }
        /// <summary> 值字段，只用于快速联动类型 </summary>
        public string option { get; set; }
        /// <summary> 父级id字段，只用于快速联动类型 </summary>
        public string pid { get; set; }
        /// <summary> 百度地图appkey </summary>
        public string ak { get; set; }
        /// <summary> 排序 </summary>
        public int sort { get; set; }
        /// <summary> 状态：0禁用，1启用 </summary>
        public bool status { get; set; }
    }
}
