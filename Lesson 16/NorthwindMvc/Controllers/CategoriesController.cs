using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindMvc.Models;

namespace NorthwindMvc.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly NorthwindContext _context;
        public CategoriesController(NorthwindContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
    }
}
