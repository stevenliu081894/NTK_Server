using Microsoft.AspNetCore.Mvc;
using NTKServer.Business;
using NTKServer.ViewModels.Menu;

namespace NTKServer.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult MainMenu()
        {
            List<MainMenuVm> menu = AdminMenuBiz.GetMenuByRole(1);
            return View(menu);
        }
    }
}
