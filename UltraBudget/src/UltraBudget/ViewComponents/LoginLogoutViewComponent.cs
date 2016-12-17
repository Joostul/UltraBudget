using Microsoft.AspNetCore.Mvc;

namespace UltraBudget.ViewComponents
{
    public class LoginLogoutViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
