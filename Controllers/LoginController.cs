using BookTradingPlatform.Data;
using BookTradingPlatform.Models;
using BookTradingPlatform.Services;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]

public class LoginController : Controller
{
	private readonly WebDatabase _context;

	private readonly JwtToken _jwt;
	public LoginController(WebDatabase context, JwtToken jwt)
	{
		_context = context;
		_jwt = jwt;
	}
	[HttpGet]
	public IActionResult GetUsers()
	{
		var users = _context.Users.ToList();
		return Ok(new { users });
	}
	[HttpPost]
	public IActionResult PostLogin([FromBody] User loginUser)
	{
		var user = _context.Users.FirstOrDefault(u =>
			u.Username == loginUser.Username && u.Password == loginUser.Password);

		if (user == null)
			return Unauthorized("帳號或密碼錯誤");

		var token = _jwt.GenerateToken(user);

		return Ok(new
		{
			Message = "登入成功",
			Token = token,
			User = new
			{
				user.Id,
				user.Username,
				user.Email,
				user.Role
			}
		});
	}
	[HttpGet("refreshtoken/{id}")]
	public IActionResult RefreshTokenById(int id)
	{
		// 從資料庫找使用者
		var user = _context.Users.FirstOrDefault(u => u.Id == id);

		if (user == null)
			return NotFound("使用者不存在");

		// 產生新的 Token
		var token = _jwt.GenerateToken(user);

		return Ok(new
		{
			Message = "Token 重新產生成功",
			Token = token,
			User = new
			{
				user.Id,
				user.Username,
				user.Email,
				user.Role
			}
		});
	}

}
