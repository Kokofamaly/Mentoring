using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindMvc.Models;

namespace NorthwindMvc.Controllers
{
    public class ProductsController : Controller
    {
        private readonly NorthwindContext _context;
        public ProductsController(NorthwindContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var products = _context.Products.Include(s => s.Supplier).Include(s => s.Category).ToList();
            return View(products);
        }
    }
}
