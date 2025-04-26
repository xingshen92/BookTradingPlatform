using BookTradingPlatform.Dtos;
using BookTradingPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly LoginService _loginService;

    public LoginController(LoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost]
	public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
	{
		var response = await _loginService.LoginAsync(loginDto);

		if (!response.IsSuccess)
			return Unauthorized(response.Message);

		return Ok(response);
    }

    [HttpGet("refreshtoken/{id}")]
    public IActionResult RefreshTokenById(int id)
    {
        var response = _loginService.RefreshToken(id);

        if (response == null)
            return NotFound("使用者不存在");

        return Ok(response);
    }
}
