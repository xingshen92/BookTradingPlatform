using BookTradingPlatform.Data;
using BookTradingPlatform.Models;
using BookTradingPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    // 假設在某個 Controller 裡
private readonly WebDatabase _context;

public UserController(WebDatabase context)
{
    _context = context;
}
	// GET
	[HttpGet()]
	public async Task<ActionResult<IEnumerable<User>>> GetUsers()
	{
		return await _context.Users.ToListAsync();
	}

	// GET: api/User/5
	[HttpGet("{id}")]
	public async Task<ActionResult<User>> GetUser(int id)
	{
		var user = await _context.Users.FindAsync(id);

		if (user == null)
			return NotFound();

		return user;
	}
	[HttpPost]
	public async Task<ActionResult<User>> PostUser(User user)
	{
		user.Modified_at = DateTime.UtcNow;
		user.Role = "user"; 

		_context.Users.Add(user);
		await _context.SaveChangesAsync();

		// 返回創建用戶URL
		return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> PutUser(int id, User user)
	{
		if (id != user.Id)
			return BadRequest();

		user.Modified_at = DateTime.Now;
		_context.Entry(user).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!_context.Users.Any(e => e.Id == id))
				return NotFound();
			else
				throw;
		}
		return NoContent();
	}
	// DELETE: api/User/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteUser(int id)
	{
		var user = await _context.Users.FindAsync(id);
		if (user == null)
			return NotFound();

		_context.Users.Remove(user);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}
