using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NTKServer.Internal
{
    public class WhereAttribute : Attribute
    {
        public string comparative;
        public string field;

        /// <summary>
        /// 建立搜寻筛选条件
        /// </summary>
        /// <param name="comparative">可使用DB内建 "like", "=", ">=", "<="...</param>
        /// <param name="dbfield">资料库要比对的对应栏位</param>
        public WhereAttribute(string _comparative, string _dbfield)
        {
            comparative = _comparative.ToLower();
            field = (_dbfield.IndexOf('.') == -1) ? $"`{_dbfield}`" : _dbfield;
        }
    }
}

