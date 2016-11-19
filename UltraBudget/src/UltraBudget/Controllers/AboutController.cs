using Microsoft.AspNetCore.Mvc;

namespace UltraBudget.Controllers
{
    public class AboutController: Controller
    {
        public IActionResult Phone()
        {
            return Content("Example phone number");
        }
        
        public IActionResult Address()
        {
            return Content("Example address");
        }
    }
}
