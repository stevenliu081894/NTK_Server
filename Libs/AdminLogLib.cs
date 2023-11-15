using System;
using DB.Services;
using Models.Dto;
using NTKServer.Models.Admin;

namespace NTKServer.Libs
{
	public class AdminLogLib
    {
        public static void Save(AdminLogRequest req)
        {
            AdminUserDto adminUserDto = AdminUserService.Find(req.admin_user_pk);


            AdminActionDto adminActionDto = AdminActionService.FindByAdminMenu(req.admin_menu_fk, adminUserDto.lang);

            if (adminActionDto != null)
            {
                // 取出模板
                var log = adminActionDto?.log;

                string mparams = string.Empty;
                if (req.list != null)
                {
                    // 資料套入模板
                    for (int i = 0; i < req.list.Length; i++)
                    {
                        log = log.Replace($"#{i}#", req.list[i].ToString());
                        mparams = mparams + req.list[i].ToString() + "|";
                    }
                }


                // 寫到 admin log
                AdminLogService.FindPkAfterInsert(new AdminLogDto
                {
                    admin_action = adminActionDto.pk,
                    admin_user = adminUserDto.pk,
                    param = mparams,
                    remark = log,
                    create_time = req.createtime
                });
            }
        }
    }
}

