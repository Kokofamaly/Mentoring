using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            PopulateDropDowns(product);
            return View(product);
        }
        public IActionResult Create()
        {
            PopulateDropDowns();
            return View();
        }

        [HttpPost]
        public IActionResult Update(int id, Product product) {
            if(id != product.ProductId) return BadRequest();

            if (!ModelState.IsValid)
            {
                PopulateDropDowns(product);
                return View(product);
            }
            var existingProduct = _context.Products.Find(id);
            if (existingProduct == null) return NotFound();

            existingProduct.ProductName = product.ProductName;
            existingProduct.SupplierId = product.SupplierId;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.QuantityPerUnit = product.QuantityPerUnit;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.UnitsInStock = product.UnitsInStock;
            existingProduct.UnitsOnOrder = product.UnitsOnOrder;
            existingProduct.ReorderLevel = product.ReorderLevel;
            existingProduct.Discontinued = product.Discontinued;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Create(Product product) {
            if (!ModelState.IsValid)
            {
                PopulateDropDowns(product);
                return View(product);
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropDowns(Product? product = null)
        {
            ViewBag.Categories = new SelectList(_context.Categories.OrderBy(c => c.CategoryName), "CategoryId", "CategoryName", product?.CategoryId);
            ViewBag.Suppliers = new SelectList(_context.Suppliers.OrderBy(s => s.CompanyName), "SupplierId", "CompanyName", product?.SupplierId);
        }
    }
}
