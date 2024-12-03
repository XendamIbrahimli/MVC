using Microsoft.AspNetCore.Mvc;

namespace UniqloMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
