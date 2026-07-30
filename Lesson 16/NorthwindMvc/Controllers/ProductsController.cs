using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NorthwindMvc.Models;

namespace NorthwindMvc.Controllers
{
    public class ProductsController : Controller
    {
        private readonly NorthwindContext _context;
        private readonly AppSettings _settings;
        public ProductsController(NorthwindContext context, IOptions<AppSettings> options)
        {
            _context = context;
            _settings = options.Value;
        }
        public IActionResult Index()
        {
            int maxShownProducts = _settings.MaxShownProducts;


            var productsQuery = _context.Products.Include(s => s.Supplier).Include(s => s.Category);
            var products = maxShownProducts > 0 ? productsQuery.Take(maxShownProducts).ToList() : productsQuery.ToList();

            return View(products);
        }
    }
}
