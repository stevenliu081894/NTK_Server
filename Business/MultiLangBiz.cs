using DB.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Dto;
using Newtonsoft.Json;
using NTKServer.Tool;
using NTKServer.ViewModels.MultiLang;

namespace NTKServer.Business
{
    public class MultiLangBiz
    {
        public static void MultilangSync(string key)
        {
            var mutilang = MutilangService.Find(key);
            var mutilang_subject_list = MutilangSubjectService.FindAll()
                .FindAll(x => x.enable == 1);

            // 將 mutilang.template 依照 mutilang_subject.lang 逐步合併到 mutilang_cache.value
            foreach (var mutilang_subject in mutilang_subject_list)
            {
                var template_dict = PublicTool.FromJson<Dictionary<string, string>>(mutilang.template);
                var mutilang_cache = MutilangCacheService.Find(mutilang.key, mutilang_subject.lang);

                // mutilang_cache 不存在就新增
                if (mutilang_cache == null)
                {
                    MutilangCacheService.Insert(new MutilangCacheDto
                    {
                        key = mutilang.key,
                        lang = mutilang_subject.lang,
                        value = JsonConvert.SerializeObject(template_dict)
                    });
                }
                // mutilang_cache 存在要將 mutilang_cache.value 合併 mutilang.template 後更新
                else
                {
                    var value_dict = PublicTool.FromJson<Dictionary<string, string>>(mutilang_cache.value);

                    // 重複的Key，mutilang_cache.value 覆蓋  mutilang.template 
                    var merged_dict = template_dict.Concat(value_dict)
                      .GroupBy(x => x.Key)
                      .ToDictionary(x => x.Key, x => x.Last().Value);

                    MutilangCacheService.Update(new MutilangCacheDto
                    {
                        key = mutilang.key,
                        lang = mutilang_subject.lang,
                        value = JsonConvert.SerializeObject(merged_dict)
                    });
                }
            }
        }

        public static List<SelectListItem> FindSelectList()
        {
			var list = new List<SelectListItem>();
			var langs = MutilangSubjectService.FindAll();
			foreach (var lang in langs)
			{
				list.Add(new SelectListItem()
				{
					Value = lang.lang,
					Text = $"{lang.title}({lang.lang})"
				});
			}
			return list;
        }

        public static List<MultiLangLanghList> GetSearchList()
        {
            return new List<MultiLangLanghList>();
        }

        public static MutilangSubjectDto GetByLang(string lang)
        {
            return MutilangSubjectService.Find(lang);
        }

        public static int Edit(MultiLangLangVm vm)
        {
            var dto = new MutilangSubjectDto()
            {
                lang = vm.lang,
                title = vm.title,
                enable = vm.enable,
                admin_default = vm.admin_default,
                app_default = vm.app_default
            };
            return MutilangSubjectService.UpdateFull(dto);

        }

        public static List<MultilangList> GetList(MultilangFilter filter)
        {
            string whereSql = SqlTool.Build(filter);
            return MutilangService.FindMutilangList(whereSql);
        }

        public static List<MultiLangCacheSearchList> GetMultiLangCacheList(MultiLangCacheSearchFilter filter)
        {
            string whereSql = SqlTool.Build(filter);
            var multiLang = MutilangService.Find(filter.key);

            var langCaches = MutilangCacheService.GetMultiLangCacheList(whereSql);
            Dictionary<string, string> dics = PublicTool.FromJson<Dictionary<string, string>>(multiLang.template);

            var rep = new List<MultiLangCacheSearchList>();

            foreach (var dic in dics)
            {
                foreach (var langCache in langCaches)
                {
                    Dictionary<string, string> cacheDics = PublicTool.FromJson<Dictionary<string, string>>(langCache.value);
                    rep.Add(new MultiLangCacheSearchList()
                    {
                        lang = langCache.lang,
                        key = dic.Key,
                        description = dic.Value,
                        translation = cacheDics?.FirstOrDefault(x => x.Key == dic.Key).Value ?? ""
                    });
                }
            }
            return rep;
        }

        public static MultiLangCacheEditVm GetTranslateByKey(string key, string lang, string valKey)
        {
            var langCache = MutilangCacheService.Find(key, lang);
            Dictionary<string, string> cacheDics = PublicTool.FromJson<Dictionary<string, string>>(langCache.value);
            var translation = string.Empty;
            foreach (var cacheDic in cacheDics)
            {
                if (cacheDic.Key == valKey)
                    translation= cacheDic.Value;
            }
            return new MultiLangCacheEditVm
            {
                key = key,
                lang = lang,
                translationKey = valKey,
                translation= translation
            };
        }

        public static int EditMultilangTranslate(MultiLangCacheEditVm req)
        {
            var langCache = MutilangCacheService.Find(req.key, req.lang);
            Dictionary<string, string> cacheDics = PublicTool.FromJson<Dictionary<string, string>>(langCache.value);
            Dictionary<string, string> trans = new Dictionary<string, string>();
            foreach (var cacheDic in cacheDics)
            {
                if (cacheDic.Key == req.translationKey)
                    trans.Add(cacheDic.Key, req.translation);
                else
                    trans.Add(cacheDic.Key, cacheDic.Value);
            }
            langCache.value = JsonConvert.SerializeObject(trans);
            return MutilangCacheService.Update(langCache);
        }

    }
}
