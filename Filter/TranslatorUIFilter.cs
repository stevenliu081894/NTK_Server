using Microsoft.AspNetCore.Mvc.Filters;
using NTKServer.Controllers;
using DB.Services;
using NTKServer.Tool;

namespace NTKServer.Filter
{
    public class TranslatorUIFilter : ActionFilterAttribute
    {
        private string _key;

        public TranslatorUIFilter(string key)
        {
            _key = key;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Dictionary<string, string> dict = new()
            {
                { "public:search", "搜尋" },
                { "public:option", "管理" },
                { "public:append", "新增" },
				{ "public:detail", "详情" },
				{ "public:edit", "編輯" },
                { "public:copy", "複製" },
                { "public:delete", "刪除" },
				{ "public:setup", "设置" },
				{ "public:total_count", "總筆數" },
                { "public:title_create", "新增" },
                { "public:title_edit", "編輯" },
                { "public:title_copy", "複製" },
                { "public:back_list", "返回列表" },
                { "public:save", "儲存" },
				{ "public:review", "审核" },
				{ "public:member_fk", "会员编号" },
				{ "public:mem_account", "会员帐号" },
				{ "public:mem_nickname", "会员姓名" },
				{ "public:approve", "同意" },
				{ "public:reject", "不同意" },
                { "public:confirm_delete", "确定要删除？" }
			};

            var ctrl = context.Controller as BaseController;
            string lang = ctrl.GetLanguage();
            string json = MutilangCacheService.FindJson(_key, lang) ?? MutilangService.FindJson(_key);
            Dictionary<string, string> mainDic = PublicTool.FromJson<Dictionary<string, string>>(json);

            mainDic = mainDic.Concat(dict).ToDictionary(k => k.Key, v => v.Value);
            ctrl.ViewData["Lang"] = mainDic;
        }
    }
}
