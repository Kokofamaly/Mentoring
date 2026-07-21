using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindWebApi.Models;
using NorthwindWebApi.Models.DTOs;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly NorthwindContext _context;
    public CategoriesController(NorthwindContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetCategory()
    {
        return await _context.Categories.Select(c =>
            new CategoryResponseDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                Description = c.Description
            }).ToListAsync();
    }

    [HttpGet("{categoryid:int}")]
    public async Task<ActionResult<CategoryResponseDto>> GetCategory(int categoryid)
    {
        var category = await _context.Categories.FindAsync(categoryid);

        if (category == null)
        {
            return NotFound();
        }

        return new CategoryResponseDto
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            Description = category.Description
        };
    }


    [HttpPut("{categoryid:int}")]
    public async Task<IActionResult> PutCategory(int categoryid, UpdateCategoryDto categoryUpdate)
    {
        if (categoryid != categoryUpdate.CategoryId) return BadRequest("Id mismatch");

        var category = await _context.Categories.FindAsync(categoryid);

        if (category == null) return NotFound();

        category.CategoryName = categoryUpdate.CategoryName;
        category.Description = categoryUpdate.Description;


        await _context.SaveChangesAsync();
        return NoContent();
    }


    [HttpPost]
    public async Task<ActionResult<CategoryResponseDto>> PostCategory(CreateCategoryDto categoryDto)
    {


        var category = new Category
        {
            CategoryName = categoryDto.CategoryName,
            Description = categoryDto.Description
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var responseCategoryDto = new CategoryResponseDto
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            Description = category.Description
        };

        return CreatedAtAction(nameof(GetCategory), new { categoryid = responseCategoryDto!.CategoryId }, responseCategoryDto);
    }


    [HttpDelete("{categoryid:int}")]
    public async Task<IActionResult> DeleteCategory(int categoryid)
    {
        var category = await _context.Categories.FindAsync(categoryid);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
