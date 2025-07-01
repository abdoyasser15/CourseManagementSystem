using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
