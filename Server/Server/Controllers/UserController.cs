using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
