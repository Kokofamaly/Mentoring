using Microsoft.AspNetCore.Mvc;

namespace NorthwindMvc.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
