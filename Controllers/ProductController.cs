using Microsoft.AspNetCore.Mvc;
using BookTradingPlatform.Data;
using BookTradingPlatform.Models;
using BookTradingPlatform.Controllers.Models;

[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
	private readonly WebDatabase _context;

	public ProductController(WebDatabase context)
	{
		_context = context;
	}
	[HttpPost]
	public async Task<IActionResult> CreateProduct([FromBody] Product product)
	{
		if (product == null)
		{
			return BadRequest("Product data is null.");
		}

		if (product.price <= 0)
		{
			return BadRequest("Price must be greater than 0.");
		}

		_context.Products.Add(product);
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Product>> GetProduct(int id)
	{
		var product = await _context.Products.FindAsync(id);

		if (product == null)
		{
			return NotFound();
		}

		return product;
	}
}
