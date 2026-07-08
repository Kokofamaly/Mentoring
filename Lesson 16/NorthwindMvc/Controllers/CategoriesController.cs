using Microsoft.AspNetCore.Mvc;

namespace NorthwindMvc.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
