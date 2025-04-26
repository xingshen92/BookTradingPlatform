using BookTradingPlatform.Dtos;
using BookTradingPlatform.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
	private readonly RegisterService _registerService;

	public RegisterController(RegisterService registerService)
	{
		_registerService = registerService;
	}

	[HttpGet]
	public IActionResult GetUsers()
	{
		var users = _registerService.GetAllUsers();
		return Ok(users);
	}

	[HttpPost]
	public async Task<IActionResult> PostRegister([FromBody] RegisterRequestDto registerRequest)
	{
		var result = await _registerService.RegisterAsync(registerRequest);

		if (result == "用戶名稱已存在")
			return BadRequest(result);

		return Ok(result);
	}

	[HttpPut("update/{id}")]
	public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequestDto updateRequest)
	{
		var result = await _registerService.UpdateUserAsync(id, updateRequest);

		if (result == "使用者不存在")
			return NotFound(result);

		return Ok(result);
	}
}
