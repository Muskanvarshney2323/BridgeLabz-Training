using Microsoft.AspNetCore.Mvc;

namespace GreetingApp.Controllers
{
    public class GreetingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Hello(string name)
        {
            ViewBag.Message = "Hello " + name + "!";
            return View();
        }
    }
}