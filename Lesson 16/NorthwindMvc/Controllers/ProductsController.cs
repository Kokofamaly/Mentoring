using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindMvc.Models;

namespace NorthwindMvc.Controllers
{
    public class ProductsController : Controller
    {
        private readonly NorthwindContext _context;
        private readonly IConfiguration _configuration;
        public ProductsController(NorthwindContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            int maxShownProducts = _configuration.GetValue<int>("MaxShownProducts");

            var productsQuery = _context.Products.Include(s => s.Supplier).Include(s => s.Category);
            if (maxShownProducts > 0) { 
                var productsLimited = productsQuery.Take(maxShownProducts).ToList();
                return View(productsLimited);
            }
            var productsAll = productsQuery.ToList();
            return View(productsAll);
        }
    }
}
