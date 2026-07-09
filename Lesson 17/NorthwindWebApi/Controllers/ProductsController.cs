using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindWebApi.Models;
using NorthwindWebApi.Models.DTOs;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly NorthwindContext _context;
    public ProductsController(NorthwindContext context)
    {
        _context = context;
    }

    // GET: api/Product
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProduct()
    {
        return await _context.Products.Select(p => 
            new ProductResponseDto { 
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                QuantityPerUnit = p.QuantityPerUnit,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                UnitsOnOrder = p.UnitsOnOrder,
                ReorderLevel = p.ReorderLevel,
                Discontinued = p.Discontinued,
                SupplierName = p.Supplier != null ? p.Supplier.CompanyName : "Unknown",
                CategoryName = p.Category != null ? p.Category.CategoryName : "No Category"
        }).ToListAsync();
    }

    // GET: api/Product/5
    [HttpGet("{productid:int}")]
    public async Task<ActionResult<ProductResponseDto>> GetProduct(int productid)
    {
        var productDto = await _context.Products.Where(p => p.ProductId == productid).Select(p =>
            new ProductResponseDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                QuantityPerUnit = p.QuantityPerUnit,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                UnitsOnOrder = p.UnitsOnOrder,
                ReorderLevel = p.ReorderLevel,
                Discontinued = p.Discontinued,
                SupplierName = p.Supplier != null ? p.Supplier.CompanyName : "Unknown",
                CategoryName = p.Category != null ? p.Category.CategoryName : "No Category"
            }).SingleOrDefaultAsync();

        if (productDto == null)
        {
            return NotFound();
        }

        return productDto;
    }

    // PUT: api/Product/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{productid:int}")]
    public async Task<IActionResult> PutProduct(int productid, UpdateProductDto updateProduct)
    {
        if (productid != updateProduct.ProductId) return BadRequest("Id mismatch");

        if (updateProduct.SupplierId.HasValue && !await _context.Suppliers.AnyAsync(s => s.SupplierId == updateProduct.SupplierId))
        {
            return BadRequest("Supplier does not exist");
        }
        if (updateProduct.CategoryId.HasValue && !await _context.Categories.AnyAsync(c => c.CategoryId == updateProduct.CategoryId))
        {
            return BadRequest("Category does not exist");
        }

        var product = await _context.Products.FindAsync(productid);

        if (product == null) return NotFound();

        product.ProductName = updateProduct.ProductName;
        product.SupplierId = updateProduct.SupplierId;
        product.CategoryId = updateProduct.CategoryId;
        product.UnitPrice = updateProduct.UnitPrice;
        product.UnitsInStock = updateProduct.UnitsInStock;
        product.UnitsOnOrder = updateProduct.UnitsOnOrder;
        product.QuantityPerUnit = updateProduct.QuantityPerUnit;
        product.ReorderLevel = updateProduct.ReorderLevel;
        product.Discontinued = updateProduct.Discontinued;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // POST: api/Product
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> PostProduct(CreateProductDto productDto)
    {
        if (productDto.SupplierId.HasValue && !await _context.Suppliers.AnyAsync(s => s.SupplierId == productDto.SupplierId))
        {
            return BadRequest("Supplier does not exist");
        }
        if (productDto.CategoryId.HasValue && !await _context.Categories.AnyAsync(c => c.CategoryId == productDto.CategoryId))
        {
            return BadRequest("Category does not exist");
        }

        var product = new Product
        {
            ProductName = productDto.ProductName,
            QuantityPerUnit = productDto.QuantityPerUnit,
            UnitPrice = productDto.UnitPrice,
            UnitsInStock = productDto.UnitsInStock,
            UnitsOnOrder = productDto.UnitsOnOrder,
            ReorderLevel = productDto.ReorderLevel,
            Discontinued = productDto.Discontinued,
            SupplierId = productDto.SupplierId,
            CategoryId = productDto.CategoryId
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var responseProductDto = await _context.Products
            .Where(p => p.ProductId == product.ProductId)
            .Select(p => new ProductResponseDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                QuantityPerUnit = p.QuantityPerUnit,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                UnitsOnOrder = p.UnitsOnOrder,
                ReorderLevel = p.ReorderLevel,
                Discontinued = p.Discontinued,
                SupplierName = p.Supplier != null ? p.Supplier.CompanyName : "Unknown",
                CategoryName = p.Category != null ? p.Category.CategoryName : "No Category"
            })
            .SingleOrDefaultAsync();

        return CreatedAtAction(nameof(GetProduct), new { productid = responseProductDto!.ProductId }, responseProductDto);
    }


    // DELETE: api/Product/5
    [HttpDelete("{productid:int}")]
    public async Task<IActionResult> DeleteProduct(int productid)
    {
        var product = await _context.Products.FindAsync(productid);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
