using BookTradingPlatform.Data;
using BookTradingPlatform.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : Controller
{
	private readonly WebDatabase _context;

	public RegisterController(WebDatabase context)
	{
		_context = context;
	}
	[HttpGet]
	public IActionResult GetUsers()
	{
		var users = _context.Users.ToList();
		return Ok(users);
	}
	[HttpPost]
	public async Task<IActionResult> PostRegister(User user)
	{
		if (_context.Users.Any(u => u.Username == user.Username))
			return BadRequest("用戶名稱已存在");

		user.Modified_at = DateTime.UtcNow;
		user.Role = "User";

		_context.Users.Add(user);
		await _context.SaveChangesAsync();

		return Ok("註冊成功");
	}
	//更新資料
	[HttpPut("update/{id}")]
	public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
	{
		var user = await _context.Users.FindAsync(id);
		if (user == null)
			return NotFound();

		user.Email = updatedUser.Email;
		user.TelePhone = updatedUser.TelePhone;
		user.Modified_at = DateTime.UtcNow;
		user.Modified_name = updatedUser.Modified_name; // 應由登入的使用者填入

		await _context.SaveChangesAsync();

		return Ok("使用者資料已更新");
	}
}