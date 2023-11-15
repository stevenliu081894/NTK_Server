using Microsoft.AspNetCore.Mvc;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.ViewModels.Home;
using NTKServer.ViewModels.Menu;

namespace NTKServer.Controllers;


public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [MenuFilter(40, 1)]
    public IActionResult Index(bool hasPermission = true)
    {
        if (!hasPermission)
        {
            ShowWarning("You don't have permission");
        }
        HomeVm model = HomeBiz.GetHomeVm();
        return View(model);
    }

    public IActionResult Verify(int id)
    {
        var user = GetUser();
        List<MainMenuVm> mainMenu = AdminMenuBiz.GetMenuByRole(user.role);
        foreach(MainMenuVm item in mainMenu)
        {
            if (item.Child != null)
            {
                foreach (MainMenuVm child in item.Child)
                {
                    if (child.Id == id)
                    {
                        return Json(new { url = child.Url, hasPermission = true });
                    }
                }
            }
        }
        return Json(new { url = "/Home/Index", hasPermission = false });
    }


}
