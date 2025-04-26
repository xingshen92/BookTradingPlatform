using Microsoft.AspNetCore.Mvc;
using BookTradingPlatform.Data;
using BookTradingPlatform.Models;
using BookTradingPlatform.Controllers.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
	//private readonly WebDatabase _context;
    private readonly IProductService _productService;
	private readonly IAdminLogService _adminLogService;

    public ProductController(IProductService productService, IAdminLogService adminLogService)
    {
		//_context = context;
        _productService = productService;
		_adminLogService = adminLogService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create(ProductCreateDto dto)
    {
        var product = await _productService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductUpdateDto dto)
    {
        var success = await _productService.UpdateAsync(id, dto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _productService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}

//admin log
//	[HttpPost("add")]
//	public async Task<IActionResult> AddProduct([FromBody] Product product)
//	{
//		var username = User.Identity?.Name;
//		var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
//		var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
//
//		if (user != null)
//		{
//			await _logService.LogAdminActionAsync(user, ip, $"新增商品：{product.Name}");
//		}
//
//		_context.Products.Add(product);
//		await _context.SaveChangesAsync();
//
//		return Ok(new { message = "Product added." });
//	}
//}
